using System.Windows.Input;

namespace InvestLens.ViewModel;

public interface IContentHeaderViewModel
{
    string WelcomeTitle { get; }
    string WelcomeDescription { get; }
    string? ButtonContent { get; }
    ICommand? ButtonCommand { get; }
    bool ShowButton { get; }
}