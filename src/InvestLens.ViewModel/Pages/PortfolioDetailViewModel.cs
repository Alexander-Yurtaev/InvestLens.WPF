using AutoMapper;
using InvestLens.Model;
using InvestLens.Model.Crud.Portfolio;
using InvestLens.ViewModel.Helpers;
using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Windows;
using InvestLens.ViewModel.Windows.Dialogs;
using InvestLens.ViewModel.Wrappers;
using System.ComponentModel;
using System.IO;
using System.Windows.Data;

namespace InvestLens.ViewModel.Pages;

public class PortfolioDetailViewModel : ViewModelBaseWithContentHeader, IPortfolioDetailViewModel
{
    private readonly IMapper _mapper;
    private readonly PortfolioDetail _model;
    private readonly IWindowManager _windowManager;
    private readonly IAuthManager _authManager;
    private readonly IPortfoliosManager _portfoliosManager;
    private bool _showSold;
    private int _securitiesCount;

    public PortfolioDetailViewModel(
        IMapper mapper,
        PortfolioDetail model, 
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

        PortfolioStats = _model.PortfolioStats.Select(p => new StatWrapper(p)).ToList();
        var securities = _model.Securities.Select(s => new SecurityInfoWrapper(s)).ToList();
        SecuritiesView = CollectionViewSource.GetDefaultView(securities);
        SecuritiesView.Filter = wrapper => ShowSold || ((SecurityInfoWrapper)wrapper).Count > 0; ;
        RefreshSecuritiesHeader();
    }

    public string Title => _model.Title;
    public string SecuritiesHeader => $"Активы ({_securitiesCount})";
    public List<StatWrapper> PortfolioStats { get; }
    public ICollectionView SecuritiesView { get; }
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
    public List<SecurityOperation> Operations => _model.Operations;

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

            if (viewModel?.MergeMode == true)
            {
                await _portfoliosManager.Merge(transactions);
            }
            else if (viewModel?.RecreateMode == true)
            {
                await _portfoliosManager.Recreate(transactions);
            }

            RaisePropertyChanged(nameof(SecuritiesView));
            RaisePropertyChanged(nameof(Operations));
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message ?? ex.Message;
            _windowManager.ShowErrorDialog(message);
        }
    }

    private void RefreshSecuritiesHeader()
    {
        _securitiesCount = SecuritiesView?.Cast<object>().Count() ?? 0;
        RaisePropertyChanged(nameof(SecuritiesHeader));
    }
}