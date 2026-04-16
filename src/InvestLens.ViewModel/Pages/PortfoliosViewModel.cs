using InvestLens.DataAccess.Repositories;
using InvestLens.ViewModel.Dialogs;
using InvestLens.ViewModel.Events;
using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Wrappers;
using System.Collections.ObjectModel;

namespace InvestLens.ViewModel.Pages;

public class PortfoliosViewModel : ViewModelBaseWithContentHeader, IPortfoliosViewModel
{
    private readonly IPortfoliosManager _portfoliosManager;
    private readonly IWindowManager _windowManager;
    private readonly IEventAggregator _eventAggregator;

    public PortfoliosViewModel(
        IPortfoliosManager portfoliosManager,
        IWindowManager windowManager,
        IEventAggregator eventAggregator)
        : base("Мои портфели", "Управляйте своими инвестиционными портфелями")
    {
        _portfoliosManager = portfoliosManager;
        _windowManager = windowManager;
        _eventAggregator = eventAggregator;
        var buttonModels = new List<ButtonModel>
        {
            new ButtonModel("+ Создать портфель", OnCreatePortfolio)
        };
        ContentHeaderVm.AddButtons(buttonModels);

        _eventAggregator.GetEvent<PortfoliosRefreshedEvent>().Subscribe(OnPortfoliosRefreshed);
    }

    public ObservableCollection<CardWrapper> Cards { get; } = [];

    private void OnCreatePortfolio()
    {
        _windowManager.ShowDialogWindow<CreatePortfolioWindowViewModel>();
    }

    private void OnPortfoliosRefreshed()
    {
        Cards.Clear();
        foreach (var card in _portfoliosManager.Cards.Select(c => new CardWrapper(c, OnDeleteCommand)))
        {
            Cards.Add(card);
        }
    }

    private async Task OnDeleteCommand(CardWrapper wrapper)
    {
        var viewModel = new ConfirmDeleteDialogViewModel(_windowManager)
        {
            PortfolioName = wrapper.Title
        };

        var confirmed = _windowManager.ShowDialogWindow<ConfirmDeleteDialogViewModel>(viewModel);
        if (confirmed != true) return;
        await _portfoliosManager.Delete(wrapper.Id);
    }

    public async Task Load(bool? force = false)
    {
        OnPortfoliosRefreshed();
        await Task.Delay(0);
    }
}