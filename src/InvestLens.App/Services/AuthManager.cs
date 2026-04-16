using InvestLens.Model;
using InvestLens.ViewModel.Events;
using InvestLens.ViewModel.Services;

namespace InvestLens.App.Services;

public class AuthManager : IAuthManager
{
    private readonly IEventAggregator _eventAggregator;

    public AuthManager(IEventAggregator eventAggregator)
    {
        _eventAggregator = eventAggregator;
    }

    public UserInfo? CurrentUser { get; private set; }

    public void SetCurrentUser(UserInfo info)
    {
        CurrentUser = info;
        _eventAggregator.GetEvent<LoginEvent>().Publish(CurrentUser);
    }
}