using InvestLens.DataAccess.Services;
using InvestLens.Model.Services;

namespace InvestLens.DataAccess.Repositories;

public class BoardRepository : BaseRepository, IBoardRepository
{
    public BoardRepository(IDatabaseService databaseService, IAuthManager authManager) 
        : base(databaseService, authManager)
    {
    }
}