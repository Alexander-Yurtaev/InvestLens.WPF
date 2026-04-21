namespace InvestLens.DataAccess.Repositories;

public interface ITransactionRepository
{
    Task<decimal> GetTotalCost();
    Task<decimal> GetPortfolioTotalCost(int id);
    Task<decimal> GetYield();
    Task<decimal> GetPortfolioYield(int id);
    Task<decimal> GetDividends();
    Task<decimal> GetProfitYTD();
}