using InvestLens.Model;

namespace InvestLens.ViewModel.Services;

public interface IAuthManager
{
    UserInfo? CurrentUser { get; }
    void SetCurrentUser(UserInfo info);
}