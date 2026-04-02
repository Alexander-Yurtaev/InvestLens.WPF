using System.Windows.Input;

namespace InvestLens.ViewModel;

public class ContentHeaderViewModel : IContentHeaderViewModel
{
    public ContentHeaderViewModel(string welcomeTitle, string welcomeDescription, Action? onButtonAction = null, string ? buttonContent = "")
    {
        WelcomeTitle = welcomeTitle;
        WelcomeDescription = welcomeDescription;
        if (onButtonAction is not null)
        {
            ButtonContent = buttonContent;
            ButtonCommand = new DelegateCommand(onButtonAction);
        }
    }

    public string WelcomeTitle { get; }
    public string WelcomeDescription { get; }
    public string? ButtonContent { get; }
    public ICommand? ButtonCommand { get; }
    public bool ShowButton => ButtonCommand is not null;
}