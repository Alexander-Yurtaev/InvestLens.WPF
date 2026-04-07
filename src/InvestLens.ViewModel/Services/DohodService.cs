using InvestLens.Model;
using InvestLens.Model.Enums;

namespace InvestLens.ViewModel.Services;

public class DohodService : IDohodService
{
    public DohodService()
    {
        LoadCards();
    }

    public List<Card> Cards { get; } = [];

    private void LoadCards()
    {
        var result = new List<Card>
        {
            new Card("Облигации AAA"){Stats =
            {
                new Stat("Всего", 1027),
                new Stat(nameof(PeriodType.Short), 780),
                new Stat(nameof(PeriodType.Middle), 346),
                new Stat(nameof(PeriodType.Long), 123),
            }},
            new Card("Облигации AA"){Stats =
            {
                new Stat("Всего", 2054),
                new Stat(nameof(PeriodType.Short), 1560),
                new Stat(nameof(PeriodType.Middle), 692),
                new Stat(nameof(PeriodType.Long), 246),
            }},
            new Card("Облигации A+"){Stats =
            {
                new Stat("Всего", 4108),
                new Stat(nameof(PeriodType.Short), 3120),
                new Stat(nameof(PeriodType.Middle), 1304),
                new Stat(nameof(PeriodType.Long), 492),
            }},
        };

        Cards.Clear();
        Cards.AddRange(result);
    }
}