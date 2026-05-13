using InvestLens.Model.Entities.Moex;

namespace InvestLens.DataAccess.Repositories;

public interface IHistoryRepository : IBaseRepository
{
    Task<Dictionary<string, History>> GetAllLastHistory();
}
