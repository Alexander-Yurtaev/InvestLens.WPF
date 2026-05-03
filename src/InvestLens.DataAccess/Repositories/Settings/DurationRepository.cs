using InvestLens.DataAccess.Services;
using InvestLens.Model.Services;

namespace InvestLens.DataAccess.Repositories;

public class DurationRepository : BaseRepository, IDurationRepository
{
    public DurationRepository(IDatabaseService databaseService, IAuthManager authManager) 
        : base(databaseService, authManager)
    {
    }
}