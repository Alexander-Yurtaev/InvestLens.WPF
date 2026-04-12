namespace InvestLens.ViewModel;

public interface IConfirmPasswordSupport : IPasswordSupport
{
    string ConfirmPasswordHash { get; set; }
}