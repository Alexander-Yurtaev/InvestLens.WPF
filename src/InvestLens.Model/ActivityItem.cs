using InvestLens.Model.Entities;
using InvestLens.Model.Enums;
using InvestLens.Model.Helpers;

namespace InvestLens.Model;

public class ActivityItem
{
    public ActivityItem(string icon, Transaction transaction)
    {
        Icon = icon;
        IconColor = TransactionEventHelper.EventToColor(transaction.Event);
        Title = $"{TransactionEventHelper.EventToString(transaction.Event)} {transaction.Symbol}";
        Description = GetDescription(transaction);
        Amount = GetTotalCost(transaction).ToString("C2");
        Date = transaction.Date.Date.ToString("dd-MM-yyyy");
    }

    public string Icon { get; set; }
    public string IconColor { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Amount { get; set; }
    public string Date { get; set; }

    private string GetDescription(Transaction transaction)
    {
        if (transaction.Event == Model.Enums.TransactionEvent.Buy)
        {
            return $"+{transaction.Quantity.ToString("N2")} шт";
        }

        if (transaction.Event == Model.Enums.TransactionEvent.Sell)
        {
            return $"-{transaction.Quantity.ToString("N2")} шт";
        }

        return transaction.Portfolio?.Name ?? "-";
    }

    private decimal GetTotalCost(Transaction transaction)
    {
        var totalCost = transaction.FeeTax;

        if (transaction.Event == Model.Enums.TransactionEvent.Buy ||
            transaction.Event == Model.Enums.TransactionEvent.Sell)
        {
            totalCost += transaction.Price * transaction.Quantity;
        }
        else if (transaction.Event == Model.Enums.TransactionEvent.Dividend)
        {
            totalCost += transaction.Quantity;
        }
        else if (transaction.Event == Model.Enums.TransactionEvent.Amortisation ||
                 transaction.Event == Model.Enums.TransactionEvent.Repayment)
        {
            totalCost += transaction.Quantity;
        }
        else if (transaction.Event == Model.Enums.TransactionEvent.Cash_Convert)
        {
            totalCost += transaction.Quantity;
        }

        return totalCost;
    }
}