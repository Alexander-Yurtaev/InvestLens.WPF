using InvestLens.Model.Enums;

namespace InvestLens.Model;

public class SecurityOperation
{
    public SecurityOperation(string secId, SecurityOperationType operationType)
    {
        SecId = secId;
        OperationType = operationType;
    }

    public string SecId { get; }
    public DateTime Date { get; init; }
    public SecurityOperationType OperationType { get; }
    public decimal Count { get; set; }
    public double Price { get; init; }
    public decimal TotalPrice { get; init; }
}