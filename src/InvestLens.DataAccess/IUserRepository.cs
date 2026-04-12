using InvestLens.Model.Entities;

namespace InvestLens.DataAccess;

public interface IUserRepository
{
    Task<bool> CheckLoginUnique(string login);
    Task<bool> CreateUser(User user);
    Task<User?> GetUserByLogin(string login);
}