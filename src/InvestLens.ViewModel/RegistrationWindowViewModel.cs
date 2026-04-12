using InvestLens.Model;
using InvestLens.ViewModel.Services;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security;
using System.Windows.Input;
using InvestLens.Common.Helpers;

namespace InvestLens.ViewModel;

public sealed class RegistrationWindowViewModel : ValidationViewModelBase, IRegistrationWindowViewModel
{
    private readonly ISecurityService _securityService;
    private readonly IWindowManager _windowManager;
    private readonly RegistrationModel _model;
    private string _errorMessage = string.Empty;
    private SecureString? _confirmPassword;
    private string _passwordError = string.Empty;
    private string _confirmPasswordError = string.Empty;

    public RegistrationWindowViewModel(
        RegistrationModel model, 
        ISecurityService securityService, 
        IWindowManager windowManager)
    {
        _securityService = securityService;
        _windowManager = windowManager;
        _model = model;
        RegisterCommand = new AsyncDelegateCommand(OnRegister, CanRegister);
        LoginCommand = new DelegateCommand(OnLogin);
        CloseCommand = new DelegateCommand(OnClose);
        this.PropertyChanged += OnPropertyChanged;
        InvalidateCommands();
    }

    [Required(ErrorMessage = "Имя должно быть заполнено")]
    public string Name
    {
        get => _model.Name;
        set
        {
            if (string.Equals(_model.Name, value)) return;
            _model.Name = value;
            RaisePropertyChanged();
            ValidateProperty(_model.Name);
        }
    }

    [Required(ErrorMessage = "Фамилия должна быть заполнена")]
    public string Surname
    {
        get => _model.Surname;
        set
        {
            if (string.Equals(_model.Surname, value)) return;
            _model.Surname = value;
            RaisePropertyChanged();
            ValidateProperty(_model.Surname);
        }
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
            ValidateLoginAsync();
        }
    }

    public SecureString? Password
    {
        get => _model.Password;
        set
        {
            _model.Password = value;
            RaisePropertyChanged();
            ValidateProperty(Password);
            ValidatePassword();
        }
    }

    public string PasswordError
    {
        get => _passwordError;
        set => SetProperty(ref _passwordError, value);
    }

    public SecureString? ConfirmPassword
    {
        get => _confirmPassword;
        set
        {
            if (!SetProperty(ref _confirmPassword, value)) return;
            ValidateProperty(ConfirmPassword);
            ValidatePassword();
        }
    }

    public string ConfirmPasswordError
    {
        get => _confirmPasswordError;
        set => SetProperty(ref _confirmPasswordError, value);
    }

    public ICommand RegisterCommand { get; }
    public ICommand LoginCommand { get; }
    public ICommand CloseCommand { get; }

    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    public bool ShowErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

    private async Task OnRegister()
    {
        if (!Validate()) return;

        try
        {
            var result = await _securityService.RegisterAsync(_model);
            if (result.Success)
            {
                _windowManager.SetMainWindow<LoginWindowViewModel>();
                _windowManager.ShowWindow<LoginWindowViewModel>();
                _windowManager.CloseWindow<RegistrationWindowViewModel>();
            }

            ErrorMessage = result.ErrorMessage;
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
        RaisePropertyChanged(nameof(ShowErrorMessage));
    }

    private bool CanRegister()
    {
        return !HasErrors;
    }

    private void OnLogin()
    {
        _windowManager.SetMainWindow<LoginWindowViewModel>();
        _windowManager.ShowWindow<LoginWindowViewModel>();
        _windowManager.CloseWindow<RegistrationWindowViewModel>();
    }

    private void OnClose()
    {
        _windowManager.CloseWindow<RegistrationWindowViewModel>();
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(this.HasErrors))
        {
            InvalidateCommands();
        }
    }

    private void ValidatePassword()
    {
        if (PasswordHelper.GetPasswordAsString(Password) != PasswordHelper.GetPasswordAsString(ConfirmPassword))
        {
            PasswordError = "Пароли не совпадают";
            ConfirmPasswordError = "Пароли не совпадают";
        }
        else
        {
            PasswordError = "";
            ConfirmPasswordError = "";
        }
    }

    protected override void InvalidateCommands()
    {
        ((AsyncDelegateCommand)RegisterCommand).RaiseCanExecuteChanged();
    }

    #region Overrides of ValidationViewModelBase

    private async void ValidateLoginAsync()
    {
        var isUnique = await _securityService.CheckLoginUniqueAsync(Login);
        if (!isUnique)
        {
            AddError("Такой пользователь уже зарегистрирован", nameof(Login));
        }
    }

    #endregion
}