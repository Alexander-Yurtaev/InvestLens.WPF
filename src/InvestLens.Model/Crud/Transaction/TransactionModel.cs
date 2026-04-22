using InvestLens.Model.Crud.Portfolio;
using InvestLens.Model.Enums;

namespace InvestLens.Model.Crud.Transaction;

public class TransactionModel
{
    public TransactionEvent Event { get; set; }
    public DateTime Date { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public string Currency { get; set; } = string.Empty;
    public decimal FeeTax { get; set; }
    public string Exchange { get; set; } = string.Empty;
    public decimal? NKD { get; set; }
    public string FeeCurrency { get; set; } = string.Empty;
    public bool? DoNotAdjustCash { get; set; }
    public string Note { get; set; } = string.Empty;
    public int PortfolioId { get; set; }
    public PortfolioModel? Portfolio { get; set; }
}