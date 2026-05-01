using InvestLens.Model.Services;

namespace InvestLens.DataAccess.Repositories;

public class SecurityGroupRepository : BaseRepository, ISecurityGroupRepository
{
    public SecurityGroupRepository(InvestLensDataContext dataContext, IAuthManager authManager) : base(dataContext, authManager)
    {
    }
}