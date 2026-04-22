using InvestLens.Model.Enums;

namespace InvestLens.Model;

public class SecurityOperation
{
    public string SecId { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public TransactionEvent OperationType { get; set; }
    public decimal Count { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
    public string Description { get; set; } = string.Empty;
}