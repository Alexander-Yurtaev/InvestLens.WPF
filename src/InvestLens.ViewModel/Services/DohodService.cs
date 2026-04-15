using InvestLens.Model;
using InvestLens.Model.Enums;
using InvestLens.Model.NavigationTree;
using InvestLens.ViewModel.NavigationTree;

namespace InvestLens.ViewModel.Services;

public class DohodService : IDohodService
{
    private readonly IEventAggregator _eventAggregator;

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

    private readonly Dictionary<PeriodType, List<Bond>> _bonds = new Dictionary<PeriodType, List<Bond>>
    {
        [PeriodType.Short] =
        [
            new Bond("RU000A10XXX1", "Краткосрочная облигация 1", "IssuerAAA", PeriodType.Short),
            new Bond("RU000A10XXX2", "Краткосрочная облигация 2", "IssuerAAA", PeriodType.Short)
        ],
        [PeriodType.Middle] =
        [
            new Bond("RU000A10YYY1", "Среднесрочная облигация 1", "IssuerAA", PeriodType.Middle),
            new Bond("RU000A10YYY2", "Среднесрочная облигация 2", "IssuerAA", PeriodType.Middle)
        ],
        [PeriodType.Long] =
        [
            new Bond("RU000A10ZZZ1", "Долгосрочная облигация 1", "IssuerA+", PeriodType.Long),
            new Bond("RU000A10ZZZ2", "Долгосрочная облигация 2", "IssuerA+", PeriodType.Long)
        ]
    };

    public DohodService(IEventAggregator eventAggregator)
    {
        _eventAggregator = eventAggregator;
        LoadCards();
    }

    public List<Card> Cards { get; } = [];

    public List<INavigationTreeItem> GetDohodBondsMenuItems()
    {
        var result = new List<INavigationTreeItem>
        {
            new NavigationTreeItem(new DictionariesDohodBondNavigationTreeModel("", "AAA", PeriodType.Short){Description = "Самые надежные облигации"}, _eventAggregator),
            new NavigationTreeItem(new DictionariesDohodBondNavigationTreeModel("", "AA", PeriodType.Middle){Description = "Самые надежные облигации"}, _eventAggregator),
            new NavigationTreeItem(new DictionariesDohodBondNavigationTreeModel("", "A+", PeriodType.Long){Description = "Самые надежные облигации"}, _eventAggregator)
        };

        return result;
    }

    public DohodBonds GetBonds(PeriodType periodType)
    {
        var bonds = !_bonds.TryGetValue(periodType, out var bondList) ? [] : bondList.ToList();
        var model = new DohodBonds(periodType.ToString());
        model.Bonds.AddRange(bonds);
        return model;
    }

    private void LoadCards()
    {
        Cards.Clear();
        Cards.AddRange(_cards);
    }
}