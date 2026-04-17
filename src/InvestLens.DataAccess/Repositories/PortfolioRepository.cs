using AutoMapper;
using InvestLens.Model.Entities;
using InvestLens.Model.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;

namespace InvestLens.DataAccess.Repositories;

public class PortfolioRepository(InvestLensDataContext db, IMapper mapper) : IPortfolioRepository
{
    private readonly InvestLensDataContext _db = db;
    private readonly IMapper _mapper = mapper;

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

    public async Task<Portfolio?> GetPortfolioById(int id)
    {
        var portfolio = await _db.Portfolios
            .FirstOrDefaultAsync(p => p.Id == id);

        return portfolio;
    }

    public async Task<List<Portfolio>> GetAllPortfolios(int ownerId)
    {
        return await _db.Portfolios
            .Where(p => p.OwnerId == ownerId)
            .ToListAsync();
    }

    public async Task<List<Portfolio>> GetAllPortfolios(int ownerId, PortfolioType portfolioType)
    {
        return await _db.Portfolios
            .Where(p => p.OwnerId == ownerId && p.PortfolioType == portfolioType)
            .ToListAsync();
    }

    public async Task<bool> CheckNameUniqueAsync(int portfolioId, int ownerId, string name)
    {
        var isExists = await _db.Portfolios
            .AnyAsync(p => p.OwnerId == ownerId 
                           && (portfolioId == 0 || p.Id != portfolioId) 
                           && p.Name == name);

        return !isExists;
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
}