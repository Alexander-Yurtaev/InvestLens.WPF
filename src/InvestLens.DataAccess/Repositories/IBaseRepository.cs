using Microsoft.EntityFrameworkCore.Storage;

namespace InvestLens.DataAccess.Repositories
{
    public interface IBaseRepository
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<int> SaveAsync();
    }
}