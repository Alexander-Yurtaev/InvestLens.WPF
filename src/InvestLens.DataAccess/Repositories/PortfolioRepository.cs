using AutoMapper;
using InvestLens.DataAccess.Services;
using InvestLens.Model.Entities;
using InvestLens.Model.Enums;
using InvestLens.Model.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace InvestLens.DataAccess.Repositories;

public class PortfolioRepository : BaseRepository, IPortfolioRepository
{
    private readonly IMapper _mapper;

    public PortfolioRepository(
                    IDatabaseService databaseService,
                    IMapper mapper,
                    IAuthManager authManager) : base(databaseService, authManager)
    {
        _mapper = mapper;
    }

    public async Task<List<Portfolio>> GetAllPortfolios()
    {
        var ownerId = GetOwnerId();
        return await DatabaseService.DataContext.Portfolios
            .Where(p => p.OwnerId == ownerId)
            .ToListAsync();
    }

    public async Task<List<Portfolio>> GetAllPortfolios(PortfolioType portfolioType)
    {
        var ownerId = GetOwnerId();
        return await DatabaseService.DataContext.Portfolios
            .Include(p => p.ChildrenPortfolios)
            .Where(p => p.OwnerId == ownerId && p.PortfolioType == portfolioType)
            .ToListAsync();
    }

    public async Task<Portfolio> CreatePortfolio(Portfolio portfolio)
    {
        DatabaseService.DataContext.Portfolios.Add(portfolio);
        return await Task.FromResult(portfolio);
    }

    public async Task Update(Portfolio portfolio, List<int> portfolios)
    {
        var portfolioOriginal = await GetPortfolioById(portfolio.Id);
        if (portfolioOriginal is null)
        {
            await Task.FromException(new KeyNotFoundException($"Портфель с {portfolio.Id} не найден."));
            return;
        }

        foreach (var id in portfolios.ToArray())
        {
            if (portfolioOriginal.ChildrenPortfolios.Any(c => c.Id == id)) continue;
            var childPortfolio = await GetPortfolioById(id);
            portfolioOriginal.ChildrenPortfolios.Add(childPortfolio!);
        }

        foreach (var childPortfolio in portfolioOriginal.ChildrenPortfolios.ToArray())
        {
            if (portfolios.Contains(childPortfolio.Id)) continue;
            portfolioOriginal.ChildrenPortfolios.Remove(childPortfolio);
        }
    }

    public async Task<bool> Delete(int id)
    {
        await DatabaseService.BeginTransactionAsync();

        try
        {
            var portfolio = await DatabaseService.DataContext.Portfolios
            .FirstOrDefaultAsync(p => p.Id == id);

            if (portfolio is null)
            {
                throw new InvalidOperationException($"Портфель с Id = {id} не найден.");
            }

            var count = await DatabaseService.DataContext.Portfolios.Where(p => p.Id == id).ExecuteDeleteAsync();
            DatabaseService.DataContext.Entry(portfolio).State = EntityState.Detached;

            await DatabaseService.CommitTransactionAsync();

            return count > 0;
        }
        catch
        {
            await DatabaseService.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<int> Merge(List<Transaction> transactions)
    {
        return await MergeInMemoty(transactions);
    }

    public async Task<int> Recreate(List<Transaction> transactions)
    {
        if (!transactions.Any()) return 0;

        var task = Task.Run(async () =>
        {
            await DatabaseService.BeginTransactionAsync();

            try
            {
                var portfolioId = transactions.Select(t => t.PortfolioId).Distinct().Single();
                DatabaseService.DataContext.Transactions
                    .Where(t => t.PortfolioId == portfolioId)
                    .ExecuteDelete();

                DatabaseService.DataContext.Transactions.AddRange(transactions);
                var count = DatabaseService.DataContext.SaveChanges();
                await DatabaseService.CommitTransactionAsync();
                return count;
            }
            catch (Exception)
            {
                await DatabaseService.RollbackTransactionAsync();
                throw;
            }
        });

        var count = await task;
        return count;
    }

    public async Task<Portfolio?> GetPortfolioById(int id)
    {
        var portfolio = await DatabaseService.DataContext.Portfolios
            .Include(p => p.ChildrenPortfolios)
            .FirstOrDefaultAsync(p => p.Id == id);

        return portfolio;
    }

    private async Task<int> MergeInMemoty(List<Transaction> transactions)
    {
        await DatabaseService.BeginTransactionAsync();

        try
        {
            var dbTransactions = await DatabaseService.DataContext.Transactions.ToListAsync();
            foreach (var external in transactions)
            {
                // Event,Date,Symbol,Price,Quantity,Currency,FeeTax,Exchange,NKD,FeeCurrency,DoNotAdjustCash,Note
                var existing = dbTransactions.SingleOrDefault(
                    t => t.Event == external.Event &&
                         t.Date == external.Date &&
                         t.Symbol == external.Symbol &&
                         t.Price == external.Price &&
                         t.Quantity == external.Quantity &&
                         t.Currency == external.Currency
                    );

                if (existing != null)
                {
                    external.Id = existing.Id;
                    var entity = DatabaseService.DataContext.Entry(existing);
                    entity.CurrentValues.SetValues(external);
                }
                else
                {
                    DatabaseService.DataContext.Transactions.Add(external);
                }
            }

            var count = await DatabaseService.DataContext.SaveChangesAsync();
            await DatabaseService.CommitTransactionAsync();
            return count;
        }
        catch
        {
            await DatabaseService.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<List<Transaction>> GetTransactions(int portfolioId)
    {
        var transactions = await DatabaseService.DataContext.Transactions
            .Where(t => t.PortfolioId == portfolioId)
            .ToListAsync();

        return transactions;
    }

    public async Task<List<Transaction>> GetAllTransactions()
    {
        var ownerId = GetOwnerId();

        var transactions = await DatabaseService.DataContext.Transactions
            .AsNoTracking()
            .Include(t => t.Portfolio)
            .Where(t => t.Portfolio != null &&
                        t.Portfolio.PortfolioType == PortfolioType.Invest &&
                        t.Portfolio!.OwnerId == ownerId)
            .ToListAsync();

        return transactions;
    }

    public async Task<List<Transaction>> GetLastTtransactions(int count)
    {
        var ownerId = GetOwnerId();

        var transactions = await DatabaseService.DataContext.Transactions
            .AsNoTracking()
            .Include(t => t.Portfolio)
            .Where(t => t.Portfolio != null &&
                        t.Portfolio.PortfolioType == PortfolioType.Invest &&
                        t.Portfolio!.OwnerId == ownerId)
            .OrderByDescending(t => t.Date)
            .Take(count)
            .ToListAsync();

        return transactions;
    }
}