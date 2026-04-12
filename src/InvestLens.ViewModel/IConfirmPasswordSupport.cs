using System.Security;

namespace InvestLens.ViewModel;

public interface IConfirmPasswordSupport : IPasswordSupport
{
    SecureString? ConfirmPassword { get; set; }
}