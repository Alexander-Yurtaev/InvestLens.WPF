using InvestLens.Model.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace InvestLens.DataAccess.Repositories
{
    public interface IBaseRepository
    {
        Task AddOrUpdate<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task Delete<TEntity>(int id) where TEntity : BaseEntity;
        Task<TEntity?> Get<TEntity>(int id) where TEntity : BaseEntity;
        Task RollbackTransactionAsync();
        Task<int> SaveAsync();
    }
}