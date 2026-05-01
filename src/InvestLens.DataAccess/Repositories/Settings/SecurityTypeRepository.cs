using InvestLens.Model.Services;

namespace InvestLens.DataAccess.Repositories;

public class SecurityTypeRepository : BaseRepository, ISecurityTypeRepository
{
    public SecurityTypeRepository(InvestLensDataContext dataContext, IAuthManager authManager) : base(dataContext, authManager)
    {
    }
}