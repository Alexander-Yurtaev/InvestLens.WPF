using System.Windows.Input;

namespace InvestLens.ViewModel;

public class ContentHeaderViewModel : IContentHeaderViewModel
{
    public ContentHeaderViewModel(string welcomeTitle, string welcomeDescription, List<ButtonModel>? buttonModels = null)
    {
        WelcomeTitle = welcomeTitle;
        WelcomeDescription = welcomeDescription;
        Buttons = [];

        if (buttonModels is null) return;

        foreach (var onButtonModel in buttonModels)
        {
            Buttons.Add(new ButtonInfo(onButtonModel));
        }
    }

    public string WelcomeTitle { get; }
    public string WelcomeDescription { get; }
    public List<ButtonInfo> Buttons { get; }
}

public class ButtonModel
{
    public ButtonModel(string content, Action action)
    {
        Content = content;
        Action = action;
    }

    public string Content { get; }
    public Action Action { get; }
}

public class ButtonInfo
{
    private readonly ButtonModel _model;

    public ButtonInfo(ButtonModel model)
    {
        _model = model;
        Command = new DelegateCommand(_model.Action);
    }

    public string Content => _model.Content;
    public ICommand Command { get; }
}