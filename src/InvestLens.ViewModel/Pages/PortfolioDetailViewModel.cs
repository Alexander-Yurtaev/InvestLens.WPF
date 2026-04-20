using AutoMapper;
using InvestLens.Model;
using InvestLens.Model.Crud.Portfolio;
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

namespace InvestLens.ViewModel.Pages;

public class PortfolioDetailViewModel : ViewModelBaseWithContentHeader, IPortfolioDetailViewModel
{
    private readonly IMapper _mapper;
    private readonly PortfolioDetails _model;
    private readonly IWindowManager _windowManager;
    private readonly IAuthManager _authManager;
    private readonly IPortfoliosManager _portfoliosManager;
    private bool _showSold;
    private int _securitiesCount;

    public PortfolioDetailViewModel(
        IMapper mapper,
        PortfolioDetails model, 
        IWindowManager windowManager, 
        IAuthManager authManager,
        IPortfoliosManager portfoliosManager) : base(model.Title, model.Description)
    {
        _mapper = mapper;
        _model = model;
        _windowManager = windowManager;
        _authManager = authManager;
        _portfoliosManager = portfoliosManager;
        
        var buttonModels = new List<ButtonModel>
        {
            new ButtonModel("Редактировать", OnEditPortfolio),
            new ButtonModel("Удалить", OnDeletePortfolio),
            new ButtonModel("Импортировать", OnImportPortfolio),
        };
        ContentHeaderVm.Buttons.Clear();
        ContentHeaderVm.AddButtons(buttonModels);

        var stats = _model.PortfolioStats.Select(p => new StatWrapper(p)).ToList();
        PortfolioStats = new ObservableCollection<StatWrapper>(stats);
        var securities = _model.Securities.Select(s => new SecurityInfoWrapper(s)).ToList();
        SecuritiesView = CollectionViewSource.GetDefaultView(securities);
        SecuritiesView.Filter = wrapper => ShowSold || ((SecurityInfoWrapper)wrapper).Count > 0;

        Operations = new ObservableCollection<SecurityOperation>(_model.Operations);

        RefreshSecuritiesHeader();
    }

    public string Title => _model.Title;
    public string SecuritiesHeader => $"Активы ({_securitiesCount})";
    public ObservableCollection<StatWrapper> PortfolioStats { get; }
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
            await _portfoliosManager.Delete(_model.Id);
        }
    }

    private async Task OnImportPortfolio()
    {
        var viewModel = _windowManager.ShowModalDialog<PortfolioImportDialogViewModel>();

        if (viewModel?.IsConfirmed != true) return;

        var fileFullName = viewModel?.FileFullName ?? "";

        try
        {
            using var reader = File.OpenText(fileFullName);
            var transactionModels = TransactionHelper.Convert(reader);
            var transactions = _mapper.Map<List<Model.Entities.Transaction>>(transactionModels);

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

            await RefreshModel();
            _windowManager.ShowSuccessDialog($"Было импортированно {acceptedCount} записей");
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            _windowManager.ShowErrorDialog(message);
        }
    }

    private async Task RefreshModel()
    {
        var model = await _portfoliosManager.GetPortfolioDetiails(_model.Id);
        if (model is null) return;

        _model.Securities.Clear();
        _model.Securities.AddRange(model.Securities);

        _model.PortfolioStats.Clear();
        _model.PortfolioStats.AddRange(model.PortfolioStats);

        _model.Operations.Clear();
        _model.Operations.AddRange(model.Operations);

        var portfolioStats = _model.PortfolioStats.Select(p => new StatWrapper(p)).ToList();
        PortfolioStats.Clear();
        foreach (var stat in portfolioStats)
        {
            PortfolioStats.Add(stat);
        }
        
        var securities = _model.Securities.Select(s => new SecurityInfoWrapper(s)).ToList();
        SecuritiesView = CollectionViewSource.GetDefaultView(securities);

        RaisePropertyChanged(nameof(SecuritiesView));
        RaisePropertyChanged(nameof(PortfolioStats));
        RaisePropertyChanged(nameof(Operations));
    }

    private void RefreshSecuritiesHeader()
    {
        _securitiesCount = SecuritiesView?.Cast<object>().Count() ?? 0;
        RaisePropertyChanged(nameof(SecuritiesHeader));
    }
}