using InvestLens.Model;

namespace InvestLens.Model.Services;

public interface IAuthManager
{
    UserInfo? CurrentUser { get; }
    void SetCurrentUser(UserInfo info);
}