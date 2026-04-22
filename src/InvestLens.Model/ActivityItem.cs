using InvestLens.Model.Crud.Transaction;
using InvestLens.Model.Enums;
using InvestLens.Model.Helpers;

namespace InvestLens.Model;

public class ActivityItem
{
    public ActivityItem(string icon, TransactionModel model)
    {
        Icon = icon;
        IconColor = TransactionEventHelper.EventToColor(model.Event);
        Title = $"{TransactionEventHelper.EventToString(model.Event)} {model.Symbol}";
        Description = GetDescription(model);
        Amount = TransactionEventHelper.GetTotalCost(model).ToString("C2");
        Date = model.Date.Date.ToString("dd-MM-yyyy");
    }

    public string Icon { get; set; }
    public string IconColor { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Amount { get; set; }
    public string Date { get; set; }

    private string GetDescription(TransactionModel model)
    {
        if (model.Event == TransactionEvent.Buy)
        {
            return $"+{model.Quantity.ToString("N2")} шт";
        }

        if (model.Event == TransactionEvent.Sell)
        {
            return $"-{model.Quantity.ToString("N2")} шт";
        }

        return model.Portfolio?.Name ?? "-";
    }
}