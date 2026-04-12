using InvestLens.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestLens.DataAccess;

public class UserRepository(InvestLensDataContext db) : IUserRepository
{
    public async Task<bool> CheckLoginUnique(string login)
    {
        return !await db.Users.AnyAsync(u => u.Login == login);
    }

    public async Task<bool> CreateUser(User user)
    {
        db.Users.Add(user);
        var count = await db.SaveChangesAsync();
        return count > 0;
    }

    public async Task<User?> GetUserByLogin(string login)
    {
        return await db.Users.FirstOrDefaultAsync(u => u.Login == login);
    }
}