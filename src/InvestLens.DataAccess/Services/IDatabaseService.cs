using Microsoft.EntityFrameworkCore.Storage;

namespace InvestLens.DataAccess.Services
{
    public interface IDatabaseService
    {
        InvestLensDataContext DataContext { get; }
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<int> SaveAsync();
    }
}