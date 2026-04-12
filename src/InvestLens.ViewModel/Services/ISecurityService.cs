using InvestLens.Model;
using InvestLens.Model.Entities;

namespace InvestLens.ViewModel.Services;

public interface ISecurityService
{
    Task<bool> CheckLoginUniqueAsync(string login);
    Task<(bool Success, string ErrorMessage)> RegisterAsync(RegistrationModel model);
    Task<(bool Success, User? User, string ErrorMessage)> LoginAsync(LoginModel model);
}