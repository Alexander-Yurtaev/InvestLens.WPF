using InvestLens.Model;
using InvestLens.Model.Enums;
using InvestLens.Model.Menu;

namespace InvestLens.ViewModel.Services;

public class DohodService : IDohodService
{
    private readonly List<Card> _cards =
        [
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
        ];

    private readonly Dictionary<NodeType, List<Bond>> _bonds = new Dictionary<NodeType, List<Bond>>
    {
        [NodeType.DictionariesDohodBondsAAA] =
        [
            new Bond("RU000A10XXX1", "Краткосрочная облигация 1", "IssuerAAA", PeriodType.Short),
            new Bond("RU000A10XXX2", "Краткосрочная облигация 2", "IssuerAAA", PeriodType.Long)
        ],
        [NodeType.DictionariesDohodBondsAA] =
        [
            new Bond("RU000A10YYY1", "Среднесрочная облигация 1", "IssuerAA", PeriodType.Short),
            new Bond("RU000A10YYY2", "Среднесрочная облигация 2", "IssuerAA", PeriodType.Middle)
        ],
        [NodeType.DictionariesDohodBondsAplus] =
        [
            new Bond("RU000A10ZZZ1", "Долгосрочная облигация 1", "IssuerA+", PeriodType.Long),
            new Bond("RU000A10ZZZ2", "Долгосрочная облигация 2", "IssuerA+", PeriodType.Long)
        ]
    };

    public DohodService()
    {
        LoadCards();
    }

    public List<Card> Cards { get; } = [];

    public List<MenuItemModel> GetDohodBondsMenuItems()
    {
        var result = new List<MenuItemModel>
        {
            new MenuItemModel(NodeType.DictionariesDohodBondsAAA, "", "AAA"){Title = "AAA", Description = "Самые надежные облигации"},
            new MenuItemModel(NodeType.DictionariesDohodBondsAA, "", "AA"){Title = "AA", Description = "Самые надежные облигации"},
            new MenuItemModel(NodeType.DictionariesDohodBondsAplus, "", "A+"){Title = "A+", Description = "Самые надежные облигации"},
        };

        return result;
    }

    public DohodBonds GetBonds(NodeType nodeType)
    {
        var bonds = !_bonds.TryGetValue(nodeType, out var bondList) ? [] : bondList.ToList();
        var model = new DohodBonds(nodeType.ToString());
        model.Bonds.AddRange(bonds);
        return model;
    }

    private void LoadCards()
    {
        Cards.Clear();
        Cards.AddRange(_cards);
    }
}