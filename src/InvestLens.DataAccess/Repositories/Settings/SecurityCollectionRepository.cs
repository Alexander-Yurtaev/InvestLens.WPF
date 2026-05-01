using InvestLens.Model.Services;

namespace InvestLens.DataAccess.Repositories;

public class SecurityCollectionRepository : BaseRepository, ISecurityCollectionRepository
{
    public SecurityCollectionRepository(InvestLensDataContext dataContext, IAuthManager authManager) : base(dataContext, authManager)
    {
    }
}