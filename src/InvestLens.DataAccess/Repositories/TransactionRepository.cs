
using InvestLens.Model.Services;
using Microsoft.EntityFrameworkCore;

namespace InvestLens.DataAccess.Repositories;

public class TransactionRepository : BaseRepository, ITransactionRepository
{
    public TransactionRepository(
        InvestLensDataContext db,
        IAuthManager authManager) : base(db, authManager)
    {
    }

    public async Task<decimal> GetDividends()
    {
        var dividends = await DataContext.Transactions
            .Where(t => t.Event == Model.Enums.TransactionEvents.Dividend)
            .SumAsync(t => t.Quantity);

        return dividends;
    }

    public async Task<decimal> GetTotalCost()
    {
        var totalCost = await DataContext.Transactions
            .Where(t => t.Event == Model.Enums.TransactionEvents.Buy ||
                        t.Event == Model.Enums.TransactionEvents.Sell)
            .SumAsync(t => t.Event == Model.Enums.TransactionEvents.Buy
                        ? t.Price
                        : -t.Price);

        return totalCost;
    }

    public async Task<decimal> GetYield()
    {
        return await Task.FromResult(0m);
    }
}