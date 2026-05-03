using InvestLens.DataAccess.Services;
using InvestLens.Model.Services;

namespace InvestLens.DataAccess.Repositories;

public class SecurityCollectionRepository : BaseRepository, ISecurityCollectionRepository
{
    public SecurityCollectionRepository(IDatabaseService databaseService, IAuthManager authManager) 
        : base(databaseService, authManager)
    {
    }
}