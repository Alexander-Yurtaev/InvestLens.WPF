using InvestLens.Model;
using InvestLens.Model.Crud.User;

namespace InvestLens.DataAccess.Services;

public interface ISecurityService
{
    Task<bool> CheckLoginUniqueAsync(string login);
    Task<(bool Success, string ErrorMessage)> RegisterAsync(RegistrationModel model);
    Task<(bool Success, UserModel? User, string ErrorMessage)> LoginAsync(LoginModel model);
}