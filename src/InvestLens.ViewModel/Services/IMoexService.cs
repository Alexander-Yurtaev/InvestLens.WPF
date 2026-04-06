using InvestLens.Model;

namespace InvestLens.ViewModel.Services;

public interface IMoexService
{
    List<Card> Cards { get; }
}