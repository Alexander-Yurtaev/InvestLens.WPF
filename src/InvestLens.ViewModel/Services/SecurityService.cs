using InvestLens.Common.Helpers;
using InvestLens.DataAccess;
using InvestLens.Model;
using InvestLens.Model.Entities;

namespace InvestLens.ViewModel.Services;

public class SecurityService(IUserRepository userRepository) : ISecurityService
{
    public async Task<bool> CheckLoginUniqueAsync(string login)
    {
        return await userRepository.CheckLoginUnique(login);
    }

    public async Task<(bool Success, string ErrorMessage)> RegisterAsync(RegistrationModel model)
    {
        try
        {
            var password = PasswordHelper.HashPassword(PasswordHelper.GetPasswordAsString(model.Password));
            var user = new User(model.Name, model.Surname, model.Login, password);
            var success = await userRepository.CreateUser(user);
            return success ? (true, "") : (false, "Данные не сохранились");
        }
        catch (Exception)
        {
            return (false, "Ошибка при регистрации");
        }
    }

    public async Task<(bool Success, User? User, string ErrorMessage)> LoginAsync(LoginModel model)
    {
        var user = await userRepository.GetUserByLogin(model.Login);
        if (user is null)
        {
            return (false, null, "Ошибка при авторизации");
        }

        var isAuth = PasswordHelper.VerifyPassword(PasswordHelper.GetPasswordAsString(model.Password), user.Password);

        return isAuth ? (true, user, "") : (false, null, "Ошибка при авторизации");
    }
}