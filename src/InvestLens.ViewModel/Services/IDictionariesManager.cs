using InvestLens.Model;

namespace InvestLens.ViewModel.Services;

public interface IDictionariesManager
{
    IDictionariesMoexManager DictionariesMoexManager { get; }
    List<Card> Cards { get; }
}