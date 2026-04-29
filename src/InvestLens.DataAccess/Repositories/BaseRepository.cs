using InvestLens.Model.Services;
using Microsoft.EntityFrameworkCore.Storage;

namespace InvestLens.DataAccess.Repositories;

public abstract class BaseRepository : IBaseRepository
{
    protected BaseRepository(
        InvestLensDataContext dataContext,
        IAuthManager authManager)
    {
        DataContext = dataContext;
        AuthManager = authManager;
    }

    protected InvestLensDataContext DataContext { get; }
    protected IAuthManager AuthManager { get; }

    protected int GetOwnerId()
    {
        if (AuthManager.CurrentUser is null)
        {
            throw new InvalidOperationException("Вы не авторизованы!");
        }

        return AuthManager.CurrentUser.Id;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await DataContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await DataContext.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await DataContext.Database.RollbackTransactionAsync();
    }

    public async Task<int> SaveAsync()
    {
        return await DataContext.SaveChangesAsync();
    }
}