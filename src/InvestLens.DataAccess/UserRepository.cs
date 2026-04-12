using InvestLens.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvestLens.DataAccess;

public class UserRepository : IUserRepository
{
    private readonly InvestLensDataContext _db;

    public UserRepository(InvestLensDataContext db)
    {
        _db = db;
    }

    public async Task<bool> CheckLoginUnique(string login)
    {
        return !await _db.Users.AnyAsync(u => u.Login == login);
    }

    public async Task<bool> CreateUser(User user)
    {
        _db.Users.Add(user);
        var count = await _db.SaveChangesAsync();
        return count > 0;
    }
}