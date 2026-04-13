using InvestLens.Model;

namespace InvestLens.App.Services;

public interface IAuthManager
{
    UserInfo? CurrentUser { get; set; }
}