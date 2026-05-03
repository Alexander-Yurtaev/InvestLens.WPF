using InvestLens.DataAccess.Services;
using InvestLens.Model.Entities;
using InvestLens.Model.Entities.Settings;
using InvestLens.Model.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace InvestLens.DataAccess.Repositories;

public abstract class BaseRepository : IBaseRepository
{
    protected BaseRepository(
        IDatabaseService databaseService,
        IAuthManager authManager)
    {
        DatabaseService = databaseService;
        AuthManager = authManager;
    }

    protected IDatabaseService DatabaseService { get; }
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
        var dbEntry = await Get<TEntity>(entity.Id);
        if (dbEntry is null)
        {
            DatabaseService.DataContext.Set<TEntity>().Add(entity);
        }
        else
        {
            DatabaseService.DataContext.Entry(dbEntry).State = EntityState.Detached;
            DatabaseService.DataContext.Attach(entity);
        }
    }

    // Read
    public virtual async Task<TEntity?> Get<TEntity>(int id) where TEntity : BaseEntity
    {
        var entity = await DatabaseService.DataContext.FindAsync<TEntity>(id);
        return entity;
    }

    // Delete
    public virtual async Task Delete<TEntity>(int id) where TEntity : BaseEntity
    {
        var entity = await DatabaseService.DataContext.FindAsync<TEntity>(id);
        if (entity is null)
        {
            throw new InvalidOperationException($"{this.GetType().Name}: Сущность не найдена.");
        }

        DatabaseService.DataContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
    }
}