using InvestLens.Model;
using InvestLens.ViewModel.Services;
using System.ComponentModel.DataAnnotations;
using System.Security;
using System.Windows.Input;
using InvestLens.DataAccess.Services;
using InvestLens.Model.Services;

namespace InvestLens.ViewModel.Windows;

public sealed class LoginWindowViewModel : ValidationViewModelBase, ILoginWindowViewModel
{
    private readonly IAuthService _authService;
    private readonly IWindowManager _windowManager;
    private readonly IEventAggregator _eventAggregator;
    private readonly LoginModel _model;
    private readonly IAuthManager _authManager;
    private string _errorMessage = string.Empty;

    public LoginWindowViewModel(
        LoginModel model,
        IAuthManager authManager,
        IAuthService authService, 
        IWindowManager windowManager,
        IEventAggregator eventAggregator)
    {
        _authService = authService;
        _windowManager = windowManager;
        _eventAggregator = eventAggregator;
        _model = model;
        _authManager = authManager;
        LoginCommand = new AsyncDelegateCommand(OnLogin, CanLogin);
        RegistrationCommand = new DelegateCommand(OnRegistration);
        CloseCommand = new DelegateCommand(OnClose);
        InvalidateCommands();
    }

    [Required(ErrorMessage = "Логин должн быть заполнен")]
    public string Login
    {
        get => _model.Login;
        set
        {
            if (string.Equals(_model.Login, value)) return;
            _model.Login = value;
            RaisePropertyChanged();
            ValidateProperty(_model.Login);
        }
    }

    public SecureString? Password
    {
        get => _model.Password;
        set
        {
            if (Equals(_model.Password, value)) return;
            _model.Password = value;
            RaisePropertyChanged();
        }
    }
    
    public ICommand LoginCommand { get; }
    public ICommand RegistrationCommand { get; }
    public ICommand CloseCommand { get; }

    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            if (SetProperty(ref _errorMessage, value))
            {
                RaisePropertyChanged(nameof(HasErrorMessage));
            }
        }
    }

    public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

    private async Task OnLogin()
    {
        if (!Validate()) return;

        var result = await _authService.LoginAsync(_model);
        if (result.Success)
        {
            _windowManager.ShowWindow<MainWindowViewModel>();
            _windowManager.CloseWindow<LoginWindowViewModel>();

            var userInfo = new UserInfo(result.User!);

            _authManager.SetCurrentUser(userInfo);
        }

        ErrorMessage = result.ErrorMessage;
    }

    private bool CanLogin() => !HasErrors;

    private void OnRegistration()
    {
        _windowManager.ShowWindow<RegistrationWindowViewModel>();
        _windowManager.CloseWindow<LoginWindowViewModel>();
    }

    private void OnClose()
    {
        _windowManager.CloseWindow<LoginWindowViewModel>();
    }

    protected override void InvalidateCommands()
    {
        ((AsyncDelegateCommand)LoginCommand).RaiseCanExecuteChanged();
    }
}