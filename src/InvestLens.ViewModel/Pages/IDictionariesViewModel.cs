using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public interface IDictionariesViewModel : IBaseViewModel
{
    List<CardWrapper> Cards { get; }
}