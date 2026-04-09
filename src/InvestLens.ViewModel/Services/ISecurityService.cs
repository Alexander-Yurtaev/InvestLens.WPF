using InvestLens.Model;

namespace InvestLens.ViewModel.Services;

public interface ISecurityService
{
    Task<bool> CheckEmailAsync(string email);
    (bool Success, string ErrorMessage) Register(RegistrationModel model);
    Task<(bool Success, string ErrorMessage)> LoginAsync(LoginModel model);
}