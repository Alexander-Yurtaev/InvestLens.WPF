using InvestLens.ViewModel.Services;
using System.Windows.Input;

namespace InvestLens.ViewModel.Dialogs;

public class ConfirmDeleteDialogViewModel : BindableBase, IConfirmDeleteDialogViewModel
{
    private readonly IWindowManager _windowManager;
    private bool _isAgree;
    private bool? _dialogResult;
    private string _portfolioName;

    public ConfirmDeleteDialogViewModel(IWindowManager windowManager)
    {
        CloseCommand = new DelegateCommand(OnClose);
        DeleteCommand = new DelegateCommand(OnDelete, () => IsAgree);
        _windowManager = windowManager;
    }

    public string PortfolioName
    {
        get => _portfolioName;
        set => SetProperty(ref _portfolioName, value);
    }

    public bool IsAgree
    {
        get => _isAgree;
        set
        {
            SetProperty(ref _isAgree, value);
            ((DelegateCommand)DeleteCommand).RaiseCanExecuteChanged();
        }
    }

    public bool IsConfermed { get; private set; }

    public ICommand CloseCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

    private void OnClose()
    {
        IsConfermed = false;
        _windowManager.CloseWindow<ConfirmDeleteDialogViewModel>();
    }

    private void OnDelete()
    {
        IsConfermed = true;
        _windowManager.CloseWindow<ConfirmDeleteDialogViewModel>();
    }
}