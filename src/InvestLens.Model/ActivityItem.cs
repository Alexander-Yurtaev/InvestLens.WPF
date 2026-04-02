namespace InvestLens.Model;

public class ActivityItem
{
    public ActivityItem(string icon, string title, string description, string amount, string date)
    {
        Icon = icon;
        Title = title;
        Description = description;
        Amount = amount;
        Date = date;
    }

    public string Icon { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Amount { get; set; }
    public string Date { get; set; }
}