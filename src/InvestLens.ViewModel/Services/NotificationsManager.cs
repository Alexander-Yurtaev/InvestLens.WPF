namespace InvestLens.ViewModel.Services;

public class NotificationsManager : INotificationsManager
{
    public NotificationsManager()
    {
        NotificationsCount = new Random(DateTime.Now.Second).Next(10);
    }

    public int NotificationsCount { get; }
    public string NotificationsCountDisplay => NotificationsCount <= 9 ? NotificationsCount.ToString() : "9+";
    public bool HasNotifications => NotificationsCount > 0;
}