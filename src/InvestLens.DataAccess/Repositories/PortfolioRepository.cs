using AutoMapper;
using InvestLens.Model.Entities;
using InvestLens.Model.Enums;
using InvestLens.Model.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace InvestLens.DataAccess.Repositories;

public class PortfolioRepository(InvestLensDataContext db, 
    IMapper mapper,
    IAuthManager authManager) : IPortfolioRepository
{
    private readonly InvestLensDataContext _db = db;
    private readonly IMapper _mapper = mapper;
    private readonly IAuthManager _authManager = authManager;

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _db.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _db.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _db.Database.RollbackTransactionAsync();
    }

    public async Task<List<Portfolio>> GetAllPortfolios()
    {
        var ownerId = GetOwnerId();
        return await _db.Portfolios
            .Where(p => p.OwnerId == ownerId)
            .ToListAsync();
    }

    public async Task<List<Portfolio>> GetAllPortfolios(PortfolioType portfolioType)
    {
        var ownerId = GetOwnerId();
        return await _db.Portfolios
            .Include(p => p.ChildrenPortfolios)
            .Where(p => p.OwnerId == ownerId && p.PortfolioType == portfolioType)
            .ToListAsync();
    }

    public async Task<Portfolio> CreatePortfolio(InvestLens.Model.Crud.Portfolio.CreateModel model)
    {
        var portfolio = _mapper.Map<Portfolio>(model);
        _db.Portfolios.Add(portfolio);
        return await Task.FromResult(portfolio);
    }

    public async Task Update(InvestLens.Model.Crud.Portfolio.UpdateModel model)
    {
        var portfolio = await GetPortfolioById(model.Id);
        if (portfolio is null)
        {
            await Task.FromException(new KeyNotFoundException($"Портфель с {model.Id} не найден."));
            return;
        }

        _mapper.Map(model, portfolio);
        foreach(var id in model.Portfolios.ToArray())
        {
            if (portfolio.ChildrenPortfolios.Any(c => c.Id == id)) continue;
            var childPortfolio = await GetPortfolioById(id);
            portfolio.ChildrenPortfolios.Add(childPortfolio!);
        }

        foreach(var childPortfolio in portfolio.ChildrenPortfolios.ToArray())
        {
            var deletedChildPortfolioId = model.Portfolios.FirstOrDefault(p => p == childPortfolio.Id);
            if (deletedChildPortfolioId > 0) continue;
            portfolio.ChildrenPortfolios.Remove(childPortfolio);
        }
    }

    public async Task Delete(int id)
    {
        var portfolio = await _db.Portfolios
            .FirstOrDefaultAsync(p => p.Id == id);
        if (portfolio is null) return;

        _db.Portfolios.Remove(portfolio);
    }

    public async Task<int> Save()
    {
        return await _db.SaveChangesAsync();
    }

    public async Task<int> Merge(List<Transaction> transactions)
    {
        return await MergeInMemoty(transactions);
    }

    public async Task<int> Recreate(List<Transaction> transactions)
    {
        if (!transactions.Any()) return 0;

        var task = Task.Run(() =>
        {
            using var transaction = _db.Database.BeginTransaction();

            try
            {
                var portfolioId = transactions.Select(t => t.PortfolioId).Distinct().Single();
                _db.Transactions.Where(t => t.PortfolioId == portfolioId).ExecuteDelete();
                _db.Transactions.AddRange(transactions);
                var count = _db.SaveChanges();
                transaction.Commit();
                return count;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        });

        var count = await task;
        return count;
    }

    public async Task<Portfolio?> GetPortfolioById(int id)
    {
        var portfolio = await _db.Portfolios
            .Include(p => p.ChildrenPortfolios)
            .FirstOrDefaultAsync(p => p.Id == id);

        return portfolio;
    }

    private int GetOwnerId()
    {
        if (_authManager.CurrentUser is null)
        {
            throw new InvalidOperationException("Вы не авторизованы!");
        }

        return _authManager.CurrentUser.Id;
    }

    private async Task<int> MergeInMemoty(List<Transaction> transactions)
    {
        var dbTransactions = await _db.Transactions.ToListAsync();
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
                var entity = _db.Entry(existing);
                entity.CurrentValues.SetValues(external);
            }
            else
            {
                _db.Transactions.Add(external);
            }
        }

        return await _db.SaveChangesAsync();
    }

    public async Task<List<Transaction>> GetTransactions(int portfolioId)
    {
        var transactions = await _db.Transactions
            .Where(t => t.PortfolioId == portfolioId)
            .ToListAsync();

        return transactions;
    }

    public async Task<List<Transaction>> GetAllTransactions()
    {
        var ownerId = GetOwnerId();

        var transactions = await _db.Transactions
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

        var transactions = await _db.Transactions
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