using InvestLens.Model.Entities.Settings;

namespace InvestLens.DataAccess.Repositories;

public interface ISecurityTypeRepository : IBaseRepository
{
    Task<List<SecurityType>> GetAllAsync();
}