using InvestLens.Model.Services;

namespace InvestLens.DataAccess.Repositories;

public class BoardRepository : BaseRepository, IBoardRepository
{
    public BoardRepository(InvestLensDataContext dataContext, IAuthManager authManager) : base(dataContext, authManager)
    {
    }
}