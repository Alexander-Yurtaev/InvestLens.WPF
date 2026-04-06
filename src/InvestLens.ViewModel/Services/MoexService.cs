using InvestLens.Model;

namespace InvestLens.ViewModel.Services;

public class MoexService : IMoexService
{
    public MoexService()
    {
        LoadCards();
    }

    public List<Card> Cards { get; } = [];

    private void LoadCards()
    {
        var result = new List<Card>
        {
            new Card("Ценные бумаги"){Stats =
            {
                new Stat("Всего", 584), 
                new Stat("Торгуется", 236),
                new Stat("Акции", 106),
                new Stat("ETF", 84),
            }},
            new Card("Облигации"){Stats =
            {
                new Stat("Всего", 2347), 
                new Stat("Торгуется", 1284)
            }},
        };

        Cards.Clear();
        Cards.AddRange(result);
    }
}