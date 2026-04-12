using InvestLens.Model;
using InvestLens.ViewModel.Events;
using InvestLens.ViewModel.Services;
using System.ComponentModel.DataAnnotations;
using System.Security;
using System.Windows.Input;

namespace InvestLens.ViewModel;

public sealed class LoginWindowViewModel : ValidationViewModelBase, ILoginWindowViewModel
{
    private readonly ISecurityService _securityService;
    private readonly IWindowManager _windowManager;
    private readonly IEventAggregator _eventAggregator;
    private readonly LoginModel _model;
    private string _errorMessage = string.Empty;

    public LoginWindowViewModel(
        LoginModel model, 
        ISecurityService securityService, 
        IWindowManager windowManager,
        IEventAggregator eventAggregator)
    {
        _securityService = securityService;
        _windowManager = windowManager;
        _eventAggregator = eventAggregator;
        _model = model;
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
        set => SetProperty(ref _errorMessage, value);
    }

    public bool ShowErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

    private async Task OnLogin()
    {
        if (!Validate()) return;

        var result = await _securityService.LoginAsync(_model);
        if (result.Success)
        {
            _windowManager.SetMainWindow<MainWindowViewModel>();
            _windowManager.ShowWindow<MainWindowViewModel>();
            _windowManager.CloseWindow<LoginWindowViewModel>();

            var userInfo = new UserInfo
            {
                UserAvatar = $"{result.User!.FirstName[0]}{result.User!.LastName[0]}",
                UserName = result.User!.FirstName,
                UserFullNameInShortFormat = $"{result.User!.FirstName} {result.User!.LastName[0]}"
            };

            _eventAggregator.GetEvent<LoginEvent>().Publish(userInfo);
        }

        ErrorMessage = result.ErrorMessage;
        RaisePropertyChanged(nameof(ShowErrorMessage));
    }

    private bool CanLogin()
    {
        return !HasErrors;
    }

    private void OnRegistration()
    {
        _windowManager.SetMainWindow<RegistrationWindowViewModel>();
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