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
}