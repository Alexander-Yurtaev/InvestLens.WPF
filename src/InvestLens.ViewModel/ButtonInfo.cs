using System.Windows.Input;

namespace InvestLens.ViewModel;

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