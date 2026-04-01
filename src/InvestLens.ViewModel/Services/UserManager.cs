namespace InvestLens.ViewModel.Services;

public class UserManager : IUserManager
{
    public string UserAvatar { get; private set; } = string.Empty;
    public string UserName { get; private set; } = string.Empty;
    public string UserFullNameInShortFormat { get; private set; } = string.Empty;

    public async Task LoadAsync()
    {
        UserAvatar = "АЮ";
        UserName = "Александр";
        UserFullNameInShortFormat = "Александр Ю.";
        await Task.CompletedTask;
    }
}