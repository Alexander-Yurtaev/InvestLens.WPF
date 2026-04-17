using System.Windows.Input;

namespace InvestLens.ViewModel.Wrappers;

public class ButtonWrapper
{
    private readonly ButtonModel _model;

    public ButtonWrapper(ButtonModel model)
    {
        _model = model;
        Command = new AsyncDelegateCommand(_model.Action);
    }

    public string Content => _model.Content;
    public ICommand Command { get; }
}