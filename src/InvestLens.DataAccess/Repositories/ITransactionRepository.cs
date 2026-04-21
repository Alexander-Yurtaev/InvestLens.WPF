namespace InvestLens.DataAccess.Repositories;

public interface ITransactionRepository
{
    Task<decimal> GetTotalCost();
    Task<decimal> GetYield();
    Task<decimal> GetDividends();
    Task<decimal> GetProfitYTD();
}