using InvestLens.Model.Entities;

namespace InvestLens.DataAccess.Repositories
{
    public interface IBaseRepository
    {
        Task AddOrUpdate<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task Delete<TEntity>(int id) where TEntity : BaseEntity;
        Task<TEntity?> Get<TEntity>(int id) where TEntity : BaseEntity;
    }
}