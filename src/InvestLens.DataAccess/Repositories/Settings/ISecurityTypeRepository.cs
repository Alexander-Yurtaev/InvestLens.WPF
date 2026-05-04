using InvestLens.Model.Entities.Settings;

namespace InvestLens.DataAccess.Repositories;

public interface ISecurityTypeRepository : IBaseRepository
{
    List<SecurityType> GetAll();
}