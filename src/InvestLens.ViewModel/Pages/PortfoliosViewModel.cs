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

        OnPortfoliosLoaded();

        _eventAggregator.GetEvent<PortfolioCreatedEvent>().Subscribe(OnPortfolioCreated);
        _eventAggregator.GetEvent<PortfoliosLoadedEvent>().Subscribe(OnPortfoliosLoaded);
    }

    public ObservableCollection<CardWrapper> Cards { get; } = [];

    public async Task Load(bool? force = false)
    {
        await Task.Delay(0);
    }

    private async Task OnCreatePortfolio()
    {
        _windowManager.ShowDialogWindow<CreatePortfolioWindowViewModel>();
        await Task.Delay(0);
    }

    private void OnPortfolioCreated(int id)
    {
        Cards.Clear();
        foreach (var card in _portfoliosManager.Cards.Select(c => new CardWrapper(c, OnDeleteCommand)))
        {
            Cards.Add(card);
        }
    }

    private async Task OnDeleteCommand(CardWrapper wrapper)
    {
        var confirmed = _windowManager.ShowConfirmDialog($"Вы собираетесь удалить портфель \"{wrapper.Title}\". " +
            $"Это действие нельзя отменить. Все данные портфеля будут потеряны.", "Удалить");

        if (confirmed == true)
        {
            var result = await _portfoliosManager.Delete(wrapper.Id);
            if (result == true)
            {
                var card = Cards.First(c => c.Id == wrapper.Id);
                Cards.Remove(card);
            }
        }
    }

    private void OnPortfoliosLoaded()
    {
        Cards.Clear();
        foreach (var card in _portfoliosManager.Cards.Select(c => new CardWrapper(c, OnDeleteCommand)))
        {
            Cards.Add(card);
        }
    }
}