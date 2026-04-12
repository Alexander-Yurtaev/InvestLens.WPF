using System.Security;

namespace InvestLens.ViewModel;

public interface IPasswordSupport
{
    SecureString? Password { get; set; }
}