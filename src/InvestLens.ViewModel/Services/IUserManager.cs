namespace InvestLens.ViewModel.Services;

public interface IUserManager
{
    string UserAvatar { get; }
    string UserName { get; }
    string UserFullNameInShortFormat { get; }
    Task LoadAsync();
}