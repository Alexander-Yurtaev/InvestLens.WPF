namespace InvestLens.ViewModel;

public interface IConfirmPasswordSupport : IPasswordSupport
{
    string ConfirmPassword { get; set; }
}