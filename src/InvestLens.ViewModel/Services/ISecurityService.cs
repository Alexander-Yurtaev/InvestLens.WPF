using InvestLens.Model;

namespace InvestLens.ViewModel.Services;

public interface ISecurityService
{
    Task<bool> CheckLoginUniqueAsync(string login);
    Task<(bool Success, string ErrorMessage)> RegisterAsync(RegistrationModel model);
    Task<(bool Success, string ErrorMessage)> LoginAsync(LoginModel model);
}