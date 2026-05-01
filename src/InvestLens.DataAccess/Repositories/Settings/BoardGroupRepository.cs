using InvestLens.Model.Services;

namespace InvestLens.DataAccess.Repositories;

public class BoardGroupRepository : BaseRepository, IBoardGroupRepository
{
    public BoardGroupRepository(InvestLensDataContext dataContext, IAuthManager authManager) : base(dataContext, authManager)
    {
    }
}