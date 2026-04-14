using InvestLens.Model.Entities;

namespace InvestLens.DataAccess.Repositories;

public interface IUserRepository
{
    Task<bool> CheckLoginUnique(string login);
    Task<bool> CreateUser(User user);
    Task<User?> GetUserByLogin(string login);
}