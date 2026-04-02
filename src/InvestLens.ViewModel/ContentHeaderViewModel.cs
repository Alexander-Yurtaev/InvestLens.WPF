using System.Windows.Input;

namespace InvestLens.ViewModel;

public class ContentHeaderViewModel : IContentHeaderViewModel
{
    public ContentHeaderViewModel(string welcomeTitle, string welcomeDescription, string buttonContent, Action onButtonAction)
    {
        WelcomeTitle = welcomeTitle;
        WelcomeDescription = welcomeDescription;
        ButtonContent = buttonContent;
        ButtonCommand = new DelegateCommand(onButtonAction);
    }

    public string WelcomeTitle { get; }
    public string WelcomeDescription { get; }
    public string ButtonContent { get; }
    public ICommand ButtonCommand { get; }
}