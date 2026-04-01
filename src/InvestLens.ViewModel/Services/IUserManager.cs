namespace InvestLens.ViewModel.Services;

public interface IUserManager
{
    string UserAvatar { get; set; }
    string UserName { get; set; }
    Task LoadAsync();
}