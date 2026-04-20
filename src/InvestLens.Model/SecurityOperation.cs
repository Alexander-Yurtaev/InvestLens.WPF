using InvestLens.Model.Enums;

namespace InvestLens.Model;

public class SecurityOperation
{
    public string SecId { get; } = string.Empty;
    public DateTime Date { get; init; }
    public TransactionEvents OperationType { get; }
    public decimal Count { get; set; }
    public decimal Price { get; init; }
    public decimal TotalPrice { get; init; }
    public string Description { get; init; } = string.Empty;
}