using InvestLens.DataAccess.Services;
using InvestLens.Model.Entities;
using InvestLens.Model.Enums;
using InvestLens.Model.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace InvestLens.DataAccess.Repositories;

public class TransactionRepository : BaseRepository, ITransactionRepository
{
    public TransactionRepository(
        IDatabaseService databaseService,
        IAuthManager authManager) : base(databaseService, authManager)
    {
    }

    // TotalCashIn

    public async Task<decimal> GetTotalCashIn()
    {
        var total = await DatabaseService.DataContext.Transactions
            .Where(t => t.Event == Model.Enums.TransactionEvent.Cash_In)
            .SumAsync(t => t.Quantity);

        return total;
    }

    public async Task<decimal> GetPortfolioTotalCashIn(int[] ids)
    {
        var total = await DatabaseService.DataContext.Transactions
            .Where(t => ids.Contains(t.PortfolioId) &&
                        (t.Event == Model.Enums.TransactionEvent.Cash_In))
            .SumAsync(t => t.Quantity);

        return total;
    }

    // TotalCashOut

    public async Task<decimal> GetTotalCashOut()
    {
        var total = await DatabaseService.DataContext.Transactions
            .Where(t => t.Event == Model.Enums.TransactionEvent.Cash_Out)
            .SumAsync(t => t.Quantity);

        return total;
    }

    public async Task<decimal> GetPortfolioTotalCashOut(int[] ids)
    {
        var total = await DatabaseService.DataContext.Transactions
            .Where(t => ids.Contains(t.PortfolioId) &&
                        (t.Event == Model.Enums.TransactionEvent.Cash_Out))
            .SumAsync(t => t.Quantity);

        return total;
    }

    // CurrentCost

    public async Task<decimal> GetCurrentCost(CancellationToken ct)
    {
        return await GetPortfolioCurrentCost([], ct);
    }

    public async Task<decimal> GetPortfolioCurrentCost(int[] ids, CancellationToken ct)
    {
        IQueryable<Transaction> query = DatabaseService.DataContext.Transactions;

        if (ids.Any())
        {
            query = query.Where(t => ids.Contains(t.PortfolioId));
        }

        var transactionList = await query.OrderBy(t => t.Date)
                                         .Select(t => new {
                                            PortfolioId = t.PortfolioId,
                                            Symbol = t.Symbol,
                                            Date = t.Date,
                                            Event = t.Event,
                                            Quantity = t.Quantity,
                                            Price = t.Price
                                         })
                                         .ToListAsync(ct);

        var total = await Task.Run(() =>
        {
            return transactionList
            .GroupBy(t => new { PortfolioId = t.PortfolioId, Symbol = t.Symbol })
            .ToDictionary(k => k.Key, g => g
                .OrderBy(t => t.Date)
                .Aggregate(0.0m, (acc, t) => t.Event == TransactionEvent.Buy ? acc + t.Quantity :
                                             t.Event == TransactionEvent.Sell ? acc - t.Quantity :
                                             t.Event == TransactionEvent.Split ? acc * t.Price : acc)
            )
            .GroupBy(k => k.Key.Symbol)
            .ToDictionary(g => g.Key, g =>
            {
                var quantity = g.Sum(v => v.Value);
                var price = DatabaseService.DataContext.History
                            .Where(h => h.SecId == g.Key)
                            .OrderByDescending(h => h.Date)
                            .Select(h => h.Close)
                            .FirstOrDefault();
                return quantity * price;
            });
        }, ct);

        return total.Sum(d => d.Value);
    }

    // Dividends

    public async Task<decimal> GetTotalDividends()
    {
        var total = await DatabaseService.DataContext.Transactions
            .Where(t => t.Event == Model.Enums.TransactionEvent.Dividend)
            .SumAsync(t => t.Quantity);

        return total;
    }

    public async Task<decimal> GetPortfolioTotalDividends(int[] ids)
    {
        var total = await DatabaseService.DataContext.Transactions
            .Where(t => ids.Contains(t.PortfolioId) &&
                        t.Event == Model.Enums.TransactionEvent.Dividend)
            .SumAsync(t => t.Quantity);

        return total;
    }

    // TotalFeeTax

    public async Task<decimal> GetTotalFeeTax()
    {
        var total = await DatabaseService.DataContext.Transactions
            .SumAsync(t => t.FeeTax);

        return total;
    }

    public async Task<decimal> GetPortfolioTotalFeeTax(int[] ids)
    {
        var total = await DatabaseService.DataContext.Transactions
            .Where(t => ids.Contains(t.PortfolioId))
            .SumAsync(t => t.FeeTax);

        return total;
    }

    // DynamicMetrics
    public async Task<Dictionary<DateTime, decimal>> GetDynamicMetrics(PortfolioDynamicPeriod period, CancellationToken ct)
    {
        var result = new Dictionary<DateTime, decimal>();

        if (await DatabaseService.DataContext.Transactions.AnyAsync(ct))
        {
            var startDate = DatabaseService.DataContext.Transactions.Min(t => t.Date);

            foreach (var item in GetDynamicMetrics(startDate, period))
            {
                ct.ThrowIfCancellationRequested();

                var total = await DatabaseService.DataContext.Transactions
                    .Where(t => t.Event == Model.Enums.TransactionEvent.Cash_In &&
                                t.Date <= item.Key)
                    .SumAsync(t => t.Quantity, ct);
                result.Add(item.Key, total);
            }
        }
        else
        {
            result = GetDateRange(DateTime.Now.AddMonths(-(int)period), period);
        }

        return result;
    }

    public async Task<Dictionary<DateTime, decimal>> GetPortfolioDynamicMetrics(PortfolioDynamicPeriod period, int[] ids, CancellationToken ct)
    {
        var result = new Dictionary<DateTime, decimal>();

        if (await DatabaseService.DataContext.Transactions.AnyAsync(ct))
        {
            var startDate = DatabaseService.DataContext.Transactions.Min(t => t.Date);

            foreach (var item in GetDynamicMetrics(startDate, period))
            {
                ct.ThrowIfCancellationRequested();

                var total = await DatabaseService.DataContext.Transactions
                    .Where(t => ids.Contains(t.PortfolioId) &&
                                t.Event == Model.Enums.TransactionEvent.Cash_In &&
                                t.Date <= item.Key)
                    .SumAsync(t => t.Quantity, ct);
                result.Add(item.Key, total);
            }
        }
        else
        {
            result = GetDateRange(DateTime.Now.AddMonths(-(int)period), period);
        }

        return result;
    }

    private Dictionary<DateTime, decimal> GetDynamicMetrics(
        DateTime startDate,
        PortfolioDynamicPeriod period)
    {
        return period switch
        {
            PortfolioDynamicPeriod.Period1M => GetDateRange(startDate, period),
            PortfolioDynamicPeriod.Period3M => GetDateRange(startDate, period),
            PortfolioDynamicPeriod.Period6M => GetDateRange(startDate, period),
            PortfolioDynamicPeriod.Period1Y => GetDateRange(startDate, period),
            _ => throw new NotImplementedException()
        };
    }

    private Dictionary<DateTime, decimal> GetDateRange(DateTime startDate, PortfolioDynamicPeriod period)
    {
        var result = new Dictionary<DateTime, decimal>();

        startDate = GetFirstDate(startDate, period);
        var lastDate = GetLastDate(DateTime.Now, period);
        var cursorDate = startDate;
        while (cursorDate <= lastDate)
        {
            result.Add(cursorDate, 0);
            cursorDate = cursorDate.AddMonths((int)period);
        }

        return result;
    }

    private DateTime GetFirstDate(DateTime date, PortfolioDynamicPeriod period)
    {
        return GetLastDate(date, period).AddMonths(-1).AddDays(1);
    }

    private DateTime GetLastDate(DateTime date, PortfolioDynamicPeriod period)
    {
        switch (period)
        {
            case PortfolioDynamicPeriod.Period1M:
            case PortfolioDynamicPeriod.Period3M:
            case PortfolioDynamicPeriod.Period6M:
                var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
                return new DateTime(date.Year, date.Month, daysInMonth);
            case PortfolioDynamicPeriod.Period1Y:
                return new DateTime(date.Year, 12, 31);
            default:
                throw new ArgumentOutOfRangeException(period.ToString());
        }
    }

    public async Task<DateTime> GetFirstDate(string secId)
    {
        return await DatabaseService.DataContext.Transactions
            .Where(t => t.Symbol == secId)
            .MinAsync(t => t.Date);
    }
}