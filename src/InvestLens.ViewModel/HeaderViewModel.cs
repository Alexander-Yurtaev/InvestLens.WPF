using InvestLens.Model;
using InvestLens.Model.Menu;
using InvestLens.ViewModel.Events;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel;

public class HeaderViewModel : BindableBase, IHeaderViewModel
{
    public INotificationsManager NotificationsManager { get; }
    private MenuItemModel? _model;
    private UserInfo _userInfo = new UserInfo();

    public HeaderViewModel(INotificationsManager notificationsManager, IEventAggregator eventAggregator)
    {
        NotificationsManager = notificationsManager;
        eventAggregator.GetEvent<LoginEvent>().Subscribe(OnLogin);
        eventAggregator.GetEvent<SelectMenuNodeEvent>().Subscribe(OnSelectMenuNode);
    }

    public string Title => _model?.Title ?? string.Empty;

    public string Description => _model?.Description ?? string.Empty;

    public string UserAvatar => _userInfo.UserAvatar;
    public string UserName => _userInfo.UserName;
    public string UserFullNameInShortFormat => _userInfo.UserFullNameInShortFormat;

    private void OnLogin(UserInfo userInfo)
    {
        _userInfo = userInfo;
        RaisePropertyChanged(nameof(UserAvatar));
        RaisePropertyChanged(nameof(UserName));
        RaisePropertyChanged(nameof(UserFullNameInShortFormat));
    }

    private void OnSelectMenuNode(MenuItemModel model)
    {
        _model = model;
        RaisePropertyChanged(nameof(Title));
        RaisePropertyChanged(nameof(Description));
    }
}
