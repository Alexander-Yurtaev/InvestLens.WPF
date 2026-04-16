namespace InvestLens.Model;

public class Card
{
    public Card(int id, string title, bool canDelete=false)
    {
        Id = id;
        Title = title;
        CanDelete = canDelete;
    }

    public int Id { get; init; }
    public string Title { get; }
    public string CardType { get; init; } = string.Empty;
    public string CardTypeForeground { get; init; } = "#00000000";
    public string CardTypeBackground { get; init; } = "#00000000";

    public List<Stat> Stats { get; } = [];

    public string LastDateUpdate { get; init; } = "-";
    public bool CanDelete { get; }
}

