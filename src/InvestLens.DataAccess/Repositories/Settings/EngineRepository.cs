using InvestLens.Model.Entities.Settings;
using InvestLens.Model.Services;

namespace InvestLens.DataAccess.Repositories.Settings;

public class EngineRepository : BaseRepository, IEngineRepository
{
    public EngineRepository(InvestLensDataContext dataContext, IAuthManager authManager) : base(dataContext, authManager)
    {
    }
}