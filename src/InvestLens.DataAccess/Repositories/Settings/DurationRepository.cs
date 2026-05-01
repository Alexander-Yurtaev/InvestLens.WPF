using InvestLens.Model.Services;

namespace InvestLens.DataAccess.Repositories;

public class DurationRepository : BaseRepository, IDurationRepository
{
    public DurationRepository(InvestLensDataContext dataContext, IAuthManager authManager) : base(dataContext, authManager)
    {
    }
}