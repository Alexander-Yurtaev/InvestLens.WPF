namespace InvestLens.DataAccess.Repositories;

public interface ITransactionRepository
{
    Task<decimal> GetTotalCashIn();
    Task<decimal> GetPortfolioTotalCashIn(int[] ids);

    Task<decimal> GetTotalCashOut();
    Task<decimal> GetPortfolioTotalCashOut(int[] ids);

    Task<decimal> GetCurrentCost();
    Task<decimal> GetPortfolioCurrentCost(int[] ids);

    Task<decimal> GetTotalDividends();
    Task<decimal> GetPortfolioTotalDividends(int[] ids);

    Task<decimal> GetTotalFeeTax();
    Task<decimal> GetPortfolioTotalFeeTax(int[] ids);
}