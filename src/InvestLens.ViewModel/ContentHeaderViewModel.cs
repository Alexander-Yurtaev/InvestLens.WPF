using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel;

public class ContentHeaderViewModel : BindableBase, IContentHeaderViewModel
{
    public ContentHeaderViewModel(string welcomeTitle, string welcomeDescription, List<ButtonModel>? buttonModels = null)
    {
        WelcomeTitle = welcomeTitle;
        WelcomeDescription = welcomeDescription;
        Buttons = [];

        AddButtons(buttonModels);
    }

    public string WelcomeTitle { get; private set; }
    public string WelcomeDescription { get; }
    public List<ButtonWrapper> Buttons { get; }

    public void AddButtons(List<ButtonModel>? buttonModels)
    {
        if (buttonModels is null) return;

        foreach (var onButtonModel in buttonModels)
        {
            Buttons.Add(new ButtonWrapper(onButtonModel));
        }
    }

    public void SetWelcomeTitle(string title)
    {
        WelcomeTitle = title;
        RaisePropertyChanged(nameof(WelcomeTitle));
    }
}