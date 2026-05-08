using InvestLens.DataAccess.Services;
using InvestLens.Model.Services;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InvestLens.DataAccess.Repositories;

public class HistoryRepository : BaseRepository, IHistoryRepository
{
    public HistoryRepository(IDatabaseService databaseService, IAuthManager authManager) : base(databaseService, authManager)
    {
    }
}