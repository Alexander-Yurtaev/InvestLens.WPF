namespace InvestLens.ViewModel;

public class ButtonModel
{
    public ButtonModel(string content, Func<Task> action)
    {
        Content = content;
        Action = action;
    }

    public string Content { get; }
    public Func<Task> Action { get; }
}