using InvestLens.Model.Services;

namespace InvestLens.DataAccess.Repositories;

public class MarketRepository : BaseRepository, IMarketRepository
{
    public MarketRepository(InvestLensDataContext dataContext, IAuthManager authManager) : base(dataContext, authManager)
    {
    }
}