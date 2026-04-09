namespace InvestLens.ViewModel;

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