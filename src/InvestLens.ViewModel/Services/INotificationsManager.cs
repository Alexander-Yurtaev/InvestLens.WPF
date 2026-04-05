namespace InvestLens.ViewModel.Services;

public interface INotificationsManager
{
    int NotificationsCount { get; }
    string NotificationsCountDisplay { get; }
    bool HasNotifications { get; }
}