using AutoMapper;
using InvestLens.DataAccess.Services;
using InvestLens.Model;
using InvestLens.Model.Crud.Portfolio;
using InvestLens.Model.Entities;
using InvestLens.Model.Services;
using InvestLens.ViewModel.Helpers;
using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Windows;
using InvestLens.ViewModel.Windows.Dialogs;
using InvestLens.ViewModel.Wrappers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Data;
using System.Windows.Threading;

namespace InvestLens.ViewModel.Pages;

public class PortfolioDetailViewModel : ViewModelBaseWithContentHeader, IPortfolioDetailViewModel, ILoadableViewModel
{
    private readonly IMapper _mapper;
    private readonly PortfolioDetails _model;
    private readonly IWindowManager _windowManager;
    private readonly IAuthManager _authManager;
    private readonly IMetricsService _metricsService;
    private readonly IPortfoliosManager _portfoliosManager;
    private readonly ISecurityService _securityService;
    private bool _showSold;
    private int _securitiesCount;

    public PortfolioDetailViewModel(
        IMapper mapper,
        PortfolioDetails model, 
        IWindowManager windowManager, 
        IAuthManager authManager,
        IMetricsService metricsService,
        IPortfoliosManager portfoliosManager,
        ISecurityService securityService) : base(model.Title, model.Description)
    {
        _mapper = mapper;
        _model = model;
        _windowManager = windowManager;
        _authManager = authManager;
        _metricsService = metricsService;
        _portfoliosManager = portfoliosManager;
        _securityService = securityService;
        var buttonModels = new List<ButtonModel>
        {
            new ButtonModel("Редактировать", OnEditPortfolio),
            new ButtonModel("Удалить", OnDeletePortfolio),
        };

        if (_model.PortfolioType == Model.Enums.PortfolioType.Invest)
        {
            buttonModels.Add(new ButtonModel("Импортировать", OnImportPortfolio));
        }

        ContentHeaderVm.Buttons.Clear();
        ContentHeaderVm.AddButtons(buttonModels);

        MetricCards = [];

        var securities = _model.Securities.Select(s => new SecurityInfoWrapper(s)).ToList();
        SecuritiesView = CollectionViewSource.GetDefaultView(securities);
        SecuritiesView.Filter = wrapper => ShowSold || ((SecurityInfoWrapper)wrapper).Count > 0;

        Operations = new ObservableCollection<SecurityOperation>(_model.Operations.OrderByDescending(o => o.Date));

        RefreshSecuritiesHeader();
    }

    public string Title => _model.Title;
    public string SecuritiesHeader => $"Активы ({_securitiesCount})";
    public ObservableCollection<MetricCard> MetricCards { get; }
    public ICollectionView SecuritiesView { get; private set; }
    public bool ShowSold 
    { 
        get => _showSold;
        set
        {
            if (!SetProperty(ref _showSold, value)) return;
            SecuritiesView.Refresh();
            RefreshSecuritiesHeader();
        }
    }
    public ObservableCollection<SecurityOperation> Operations { get; }

    private async Task OnEditPortfolio()
    {
        var editModel = new UpdateModel(_model.Id, _model.Title, _model.PortfolioType)
        {
            Description = _model.Description
        };
        editModel.Portfolios.AddRange(_model.Portfolios);

        var viewModel = new UpdatePortfolioWindowViewModel(editModel, _windowManager, _authManager, _portfoliosManager);
        _windowManager.ShowDialogWindow(viewModel);
        await Task.Delay(0);
    }

    private async Task OnDeletePortfolio()
    {
        var confirmed = _windowManager.ShowConfirmDialog($"Вы собираетесь удалить портфель \"{_model.Title}\". " +
            $"Это действие нельзя отменить. Все данные портфеля будут потеряны.", "Удалить");

        if (confirmed == true)
        {
            _windowManager.ShowIsBusy();
            try
            {
                await _portfoliosManager.Delete(_model.Id);
            }
            catch (Exception ex)
            {
                ShowErrorDialog(ex);
            }
            finally
            {
                _windowManager.HideIsBusy();
            }
        }
    }

    private async Task OnImportPortfolio()
    {
        var viewModel = _windowManager.ShowModalDialog<PortfolioImportDialogViewModel>();

        if (viewModel?.IsConfirmed != true) return;

        var fileFullName = viewModel?.FileFullName ?? "";

        try
        {
            _windowManager.ShowIsBusy();
            
            var transactions = await Task.Run(() => {
                using var reader = File.OpenText(fileFullName);
                var transactionModels = TransactionHelper.Convert(reader);
                return transactionModels;
            });

            foreach ( var transaction in transactions)
            {
                transaction.PortfolioId = _model.Id;
            }

            int acceptedCount = 0;
            if (viewModel?.MergeMode == true)
            {
                acceptedCount = await _portfoliosManager.Merge(transactions);
            }
            else if (viewModel?.RecreateMode == true)
            {
                acceptedCount = await _portfoliosManager.Recreate(transactions);
            }

            await _securityService.UpdateSecurities(transactions.Select(t => t.Symbol).Distinct().ToList());

            await RefreshModel();
            
            _windowManager.ShowSuccessDialog($"Было импортированно {acceptedCount} записей");
        }
        catch (Exception ex)
        {
            ShowErrorDialog(ex);
        }
        finally
        {
            _windowManager.HideIsBusy();
        }
    }

    private async Task RefreshModel()
    {
        _windowManager.ShowIsBusy();
        
        try
        {
            var model = await _portfoliosManager.GetPortfolioDetiails(_model.Id);
            if (model is null) return;

            _model.Securities.Clear();
            _model.Securities.AddRange(model.Securities);

            _model.Operations.Clear();
            _model.Operations.AddRange(model.Operations.OrderByDescending(o => o.Date));

            var ids = _model.PortfolioType == Model.Enums.PortfolioType.Invest
                ? [_model.Id]
                : _model.Portfolios.ToArray();

            var metrics = await _metricsService.GetPortfolioMetricCards(ids);
            MetricCards.Clear();
            foreach (var metric in metrics)
            {
                MetricCards.Add(metric);
            }

            var securities = _model.Securities.Select(s => new SecurityInfoWrapper(s)).ToList();
            SecuritiesView = CollectionViewSource.GetDefaultView(securities);

            RaisePropertyChanged(nameof(SecuritiesView));
            RaisePropertyChanged(nameof(MetricCards));
            RaisePropertyChanged(nameof(Operations));
        }
        catch(Exception ex)
        {
            ShowErrorDialog(ex);
        }
        finally
        {
            _windowManager.HideIsBusy();
        }
    }

    private void RefreshSecuritiesHeader()
    {
        _securitiesCount = SecuritiesView?.Cast<object>().Count() ?? 0;
        RaisePropertyChanged(nameof(SecuritiesHeader));
    }

    public async Task Load(bool? force = false)
    {
        await RefreshModel();
    }

    private void ShowErrorDialog(Exception ex)
    {
        var message = ex.InnerException?.Message ?? ex.Message;
        _windowManager.ShowErrorDialog(message);
    }
}