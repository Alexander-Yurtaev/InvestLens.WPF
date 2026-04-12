using InvestLens.DataAccess;
using InvestLens.Model;
using InvestLens.Model.Entities;

namespace InvestLens.ViewModel.Services;

public class SecurityService : ISecurityService
{
    private readonly IUserRepository _userRepository;

    public SecurityService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> CheckLoginUniqueAsync(string login)
    {
        return await _userRepository.CheckLoginUnique(login);
    }

    public async Task<(bool Success, string ErrorMessage)> RegisterAsync(RegistrationModel model)
    {
        try
        {
            User user = new User(model.Name, model.Surname, model.Login, model.Password);
            var success = await _userRepository.CreateUser(user);
            return success ? (true, "") : (false, "Данные не сохранились");
        }
        catch (Exception e)
        {
            return (false, "Ошибка при регистрации");
        }
    }

    public Task<(bool Success, string ErrorMessage)> LoginAsync(LoginModel model)
    {
        return Task.FromResult((true, ""));
        // return Task.FromResult((false, "Ошибка при авторизации"));
    }
}