namespace InvestLens.Model;

public class Card
{
    public Card(string title)
    {
        Title = title;
    }

    public string Title { get; }
    public string CardType { get; init; } = string.Empty;
    public string CardTypeForeground { get; init; } = "#00000000";
    public string CardTypeBackground { get; init; } = "#00000000";

    public List<Stat> Stats { get; } = [];

    public string LastDateUpdate { get; init; } = "-";
}

