
using InvestLens.Model.Entities;
using InvestLens.Model.Enums;
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

    // TotalCashIn

    public async Task<decimal> GetTotalCashIn()
    {
        var total = await DataContext.Transactions
            .Where(t => t.Event == Model.Enums.TransactionEvent.Cash_In)
            .SumAsync(t => t.Quantity);

        return total;
    }

    public async Task<decimal> GetPortfolioTotalCashIn(int[] ids)
    {
        var total = await DataContext.Transactions
            .Where(t => ids.Contains(t.PortfolioId) &&
                        (t.Event == Model.Enums.TransactionEvent.Cash_In))
            .SumAsync(t => t.Quantity);

        return total;
    }

    // TotalCashOut

    public async Task<decimal> GetTotalCashOut()
    {
        var total = await DataContext.Transactions
            .Where(t => t.Event == Model.Enums.TransactionEvent.Cash_Out)
            .SumAsync(t => t.Quantity);

        return total;
    }

    public async Task<decimal> GetPortfolioTotalCashOut(int[] ids)
    {
        var total = await DataContext.Transactions
            .Where(t => ids.Contains(t.PortfolioId) &&
                        (t.Event == Model.Enums.TransactionEvent.Cash_Out))
            .SumAsync(t => t.Quantity);

        return total;
    }

    // CurrentCost

    public async Task<decimal> GetCurrentCost()
    {
        var totalCashIn = await GetTotalCashIn();
        var totlaFeeTax = await GetTotalFeeTax();

        return totalCashIn - totlaFeeTax;
    }

    public async Task<decimal> GetPortfolioCurrentCost(int[] ids)
    {
        var totalCashIn = await GetPortfolioTotalCashIn(ids);
        var totlaFeeTax = await GetPortfolioTotalFeeTax(ids);

        return totalCashIn - totlaFeeTax;
    }

    // Dividends

    public async Task<decimal> GetTotalDividends()
    {
        var total = await DataContext.Transactions
            .Where(t => t.Event == Model.Enums.TransactionEvent.Dividend)
            .SumAsync(t => t.Quantity);

        return total;
    }

    public async Task<decimal> GetPortfolioTotalDividends(int[] ids)
    {
        var total = await DataContext.Transactions
            .Where(t => ids.Contains(t.PortfolioId) &&
                        t.Event == Model.Enums.TransactionEvent.Dividend)
            .SumAsync(t => t.Quantity);

        return total;
    }

    // TotalFeeTax

    public async Task<decimal> GetTotalFeeTax()
    {
        var total = await DataContext.Transactions
            .SumAsync(t => t.FeeTax);

        return total;
    }

    public async Task<decimal> GetPortfolioTotalFeeTax(int[] ids)
    {
        var total = await DataContext.Transactions
            .Where(t => ids.Contains(t.PortfolioId))
            .SumAsync(t => t.FeeTax);

        return total;
    }

    // DynamicMetrics
    public async Task<Dictionary<DateTime, decimal>> GetDynamicMetrics(PortfolioDynamicPeriod period, CancellationToken ct)
    {
        var result = new Dictionary<DateTime, decimal>();

        if (await DataContext.Transactions.AnyAsync(ct))
        {
            var startDate = DataContext.Transactions.Min(t => t.Date);

            foreach (var item in GetDynamicMetrics(startDate, period))
            {
                ct.ThrowIfCancellationRequested();

                var total = await DataContext.Transactions
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

        if (await DataContext.Transactions.AnyAsync(ct))
        {
            var startDate = DataContext.Transactions.Min(t => t.Date);

            foreach (var item in GetDynamicMetrics(startDate, period))
            {
                ct.ThrowIfCancellationRequested();

                var total = await DataContext.Transactions
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
}