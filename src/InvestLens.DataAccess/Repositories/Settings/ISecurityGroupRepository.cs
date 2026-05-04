using InvestLens.Model.Entities.Settings;

namespace InvestLens.DataAccess.Repositories;

public interface ISecurityGroupRepository : IBaseRepository
{
    List<SecurityGroup> GetAll();
}