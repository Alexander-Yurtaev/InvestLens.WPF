using Microsoft.EntityFrameworkCore.Storage;

namespace InvestLens.DataAccess.Services;

public class DatabaseService : IDatabaseService
{
    private IDbContextTransaction? _currentTransaction;

    public DatabaseService(InvestLensDataContext dataContext)
    {
        DataContext = dataContext;
    }

    public InvestLensDataContext DataContext { get; }

    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction is not null) return;
        _currentTransaction = await DataContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_currentTransaction is null) return;
        await _currentTransaction.CommitAsync();
        _currentTransaction.Dispose();
    }

    public async Task RollbackTransactionAsync()
    {
        if (_currentTransaction is null) return;
        await _currentTransaction.RollbackAsync();
        _currentTransaction.Dispose();
    }

    public async Task<int> SaveAsync()
    {
        return await DataContext.SaveChangesAsync();
    }
}