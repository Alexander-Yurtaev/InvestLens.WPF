namespace InvestLens.Model;

public class DohodBonds
{
    public DohodBonds(string title)
    {
        Title = title;
    }

    public string Title { get; }
    public List<Bond> Bonds { get; } = [];
}