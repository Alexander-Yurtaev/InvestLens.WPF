using InvestLens.Model.Entities;
using InvestLens.Model.Entities.Settings;
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

    // CRUD
    // Create
    // Update
    public virtual async Task AddOrUpdate<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
        if (entity.Id == default)
        {
            DataContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        }
        else
        {
            DataContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        await Task.Delay(0);
    }

    // Read
    public virtual async Task<TEntity?> Get<TEntity>(int id) where TEntity : BaseEntity
    {
        var entity = await DataContext.FindAsync<TEntity>(id);
        return entity;
    }

    // Delete
    public virtual async Task Delete<TEntity>(int id) where TEntity : BaseEntity
    {
        var entity = await DataContext.FindAsync<TEntity>(id);
        if (entity is null)
        {
            throw new InvalidOperationException($"{this.GetType().Name}: Сущность не найдена.");
        }

        DataContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
    }

    // Thatsactions

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