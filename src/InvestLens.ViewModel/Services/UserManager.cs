namespace InvestLens.ViewModel.Services;

public class UserManager : IUserManager
{
    public string UserAvatar { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;

    public async Task LoadAsync()
    {
        UserAvatar = "АЮ";
        UserName = "Александр Ю.";
        await Task.CompletedTask;
    }
}