using InvestLens.Model;

namespace InvestLens.ViewModel.Services;

public interface IDictionariesManager
{
    List<Card> Cards { get; }

    List<string> GetSecurityTypes();
    List<Security> GetSecurities(string type);
}