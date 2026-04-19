using InvestLens.ViewModel.Services;
using System.Windows.Input;

namespace InvestLens.ViewModel.Windows.Dialogs;

public class ConfirmDeleteDialogViewModel : BindableBase, IConfirmDeleteDialogViewModel
{
    private readonly IWindowManager _windowManager;
    private bool _isAgree;
    private string _portfolioName = string.Empty;

    public ConfirmDeleteDialogViewModel(IWindowManager windowManager, string portfolioName="")
    {
        PortfolioName = portfolioName;
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

    public bool IsConfirmed { get; private set; }

    public ICommand CloseCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

    private void OnClose()
    {
        IsConfirmed = false;
        _windowManager.CloseWindow<ConfirmDeleteDialogViewModel>();
    }

    private void OnDelete()
    {
        IsConfirmed = true;
        _windowManager.CloseWindow<ConfirmDeleteDialogViewModel>();
    }
}