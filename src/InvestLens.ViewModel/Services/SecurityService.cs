using InvestLens.Model;

namespace InvestLens.ViewModel.Services;

public class SecurityService : ISecurityService
{
    public async Task<bool> CheckEmailAsync(string email)
    {
        return await Task.FromResult(true);
    }

    public (bool Success, string ErrorMessage) Register(RegistrationModel model)
    {
        return (true, "");
        //return (false, "Ошибка при регистрации");
    }

    public Task<(bool Success, string ErrorMessage)> LoginAsync(LoginModel model)
    {
        return Task.FromResult((true, ""));
        // return Task.FromResult((false, "Ошибка при авторизации"));
    }
}