using InvestLens.Model;
using InvestLens.ViewModel.Events;

namespace InvestLens.App.Services;

public class AuthManager : IAuthManager
{
    public AuthManager(IEventAggregator eventAggregator)
    {
        eventAggregator.GetEvent<LoginEvent>().Subscribe(OnLogin);
    }

    public UserInfo? CurrentUser { get; set; }

    private void OnLogin(UserInfo info)
    {
        CurrentUser = info;
    }
}