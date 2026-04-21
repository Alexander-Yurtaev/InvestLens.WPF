namespace InvestLens.DataAccess.Repositories;

public interface ITransactionRepository
{
    Task<decimal> GetTotalCost();
    Task<decimal> GetPortfolioTotalCost(int id);
    Task<decimal> GetYield();
    Task<decimal> GetPortfolioYield(int id);
    Task<decimal> GetDividends();
    Task<decimal> GetPortfolioDividends(int id);
    Task<decimal> GetProfitYTD();
    Task<decimal> GetPortfolioProfitYTD(int id);
}