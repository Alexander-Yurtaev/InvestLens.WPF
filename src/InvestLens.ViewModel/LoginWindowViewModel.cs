using InvestLens.Model;
using InvestLens.ViewModel.Services;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace InvestLens.ViewModel;

public class LoginWindowViewModel : ValidationViewModelBase, ILoginWindowViewModel
{
    private readonly ISecurityService _securityService;
    private readonly IWindowManager _windowManager;
    private readonly LoginModel _model;
    private string _errorMessage;

    public LoginWindowViewModel(LoginModel model, ISecurityService securityService, IWindowManager windowManager)
    {
        _securityService = securityService;
        _windowManager = windowManager;
        _model = model;
        LoginCommand = new AsyncDelegateCommand(OnLogin, CanLogin);
        RegistrationCommand = new DelegateCommand(OnRegistration);
        CloseCommand = new DelegateCommand(OnClose);
        this.PropertyChanged += OnPropertyChanged;
        InvalidateCommands();
    }

    [Required(ErrorMessage = "Адрес должн быть заполнен")]
    [EmailAddress(ErrorMessage = "Неверный формат")]
    public string Email
    {
        get => _model.Email;
        set
        {
            if (string.Equals(_model.Email, value)) return;
            _model.Email = value;
            RaisePropertyChanged();
            ValidateProperty(_model.Email);
        }
    }

    public string Password
    {
        get => _model.Password;
        set
        {
            if (string.Equals(_model.Password, value)) return;
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

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(this.HasErrors))
        {
            InvalidateCommands();
        }
    }

    private bool Validate()
    {
        var result = new List<ValidationResult>();
        var content = new ValidationContext(this);
        Validator.TryValidateObject(this, content, result);

        if (result.Any())
        {
            foreach (ValidationResult res in result)
            {
                foreach (string memberName in res.MemberNames)
                {
                    AddError(res.ErrorMessage ?? "Неизвестная ошибка", memberName);
                }
            }
        }
        else
        {
            ClearErrors(null);
        }

        return !HasErrors;
    }

    private void ValidateProperty(object? newValue, [CallerMemberName] string? propertyName = null)
    {
        var result = new List<ValidationResult>();
        var content = new ValidationContext(this) { MemberName = propertyName };
        Validator.TryValidateProperty(newValue, content, result);

        if (result.Any())
        {
            ClearErrors(propertyName);
            foreach (ValidationResult res in result)
            {
                AddError(res.ErrorMessage ?? "Неизвестная ошибка", propertyName);
            }
        }
        else
        {
            ClearErrors(propertyName);
        }
    }

    private void InvalidateCommands()
    {
        ((AsyncDelegateCommand)LoginCommand).RaiseCanExecuteChanged();
    }
}