using InvestLens.DataAccess.Services;
using InvestLens.Model.Services;

namespace InvestLens.DataAccess.Repositories.Settings;

public class EngineRepository : BaseRepository, IEngineRepository
{
    public EngineRepository(IDatabaseService databaseService, IAuthManager authManager) 
        : base(databaseService, authManager)
    {
    }
}