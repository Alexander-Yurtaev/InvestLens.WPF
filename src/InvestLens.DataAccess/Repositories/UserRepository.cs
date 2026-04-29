using InvestLens.Model.Entities;
using InvestLens.Model.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace InvestLens.DataAccess.Repositories;

public class UserRepository(InvestLensDataContext dataContext, IAuthManager authManager) 
    : BaseRepository(dataContext, authManager), IUserRepository
{
    public async Task<bool> CheckLoginUnique(string login)
    {
        return !await DataContext.Users.AnyAsync(u => u.Login == login);
    }

    public async Task<bool> CreateUser(User user)
    {
        DataContext.Users.Add(user);
        var count = await DataContext.SaveChangesAsync();
        return count > 0;
    }

    public async Task<User?> GetUserByLogin(string login)
    {
        return await DataContext.Users.FirstOrDefaultAsync(u => u.Login == login);
    }
}