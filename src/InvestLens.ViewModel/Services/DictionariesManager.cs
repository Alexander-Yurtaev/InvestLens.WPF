using InvestLens.Model;

namespace InvestLens.ViewModel.Services;

public class DictionariesManager : IDictionariesManager
{
    public List<Card> Cards { get; } = [];

    public DictionariesManager()
    {
        LoadDictionaries();
    }

    private void LoadDictionaries()
    {
        var results = new List<Card>
        {
            new Card("Московская Биржа (MOEX)")
            {
                CardType = "Официальные данные",
                Stats =
                {
                    new Stat("Ценные бумаги", 1647),
                    new Stat("Облигации", 1284)
                },
                CardTypeForeground = "#FFC8102E",
                CardTypeBackground = "#1AC8102E",
                LastDateUpdate = "10:23"
            },
            new Card("Dohod.ru")
            {
                CardType = "Агрегатор",
                Stats =
                {
                    new Stat("Облигации", 2156),
                    new Stat("Эмитентов", 342),
                    new Stat("Доходность", 24, "%"),
                },
                CardTypeForeground = "#FF2C8C6E",
                CardTypeBackground = "#1A2C8C6E",
                LastDateUpdate = "23:23"
            }
        };

        Cards.Clear();
        Cards.AddRange(results);
    }
}