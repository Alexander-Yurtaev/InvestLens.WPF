using InvestLens.DataAccess.Services;
using InvestLens.Model.Entities;
using InvestLens.Model.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace InvestLens.DataAccess.Repositories;

public class UserRepository(IDatabaseService databaseService, IAuthManager authManager) 
    : BaseRepository(databaseService, authManager), IUserRepository
{
    public async Task<bool> CheckLoginUnique(string login)
    {
        return !await DatabaseService.DataContext.Users.AnyAsync(u => u.Login == login);
    }

    public async Task<bool> CreateUser(User user)
    {
        DatabaseService.DataContext.Users.Add(user);
        var count = await DatabaseService.DataContext.SaveChangesAsync();
        return count > 0;
    }

    public async Task<User?> GetUserByLogin(string login)
    {
        return await DatabaseService.DataContext.Users.FirstOrDefaultAsync(u => u.Login == login);
    }
}