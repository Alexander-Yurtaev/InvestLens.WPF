using InvestLens.Model;
using InvestLens.ViewModel.Services;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace InvestLens.ViewModel;

public class RegistrationWindowViewModel : ValidationViewModelBase, IRegistrationWindowViewModel
{
    private readonly ISecurityService _securityService;
    private readonly IWindowManager _windowManager;
    private string _confirmPassword = string.Empty;
    private readonly RegistrationModel _model;
    private string _errorMessage;

    public RegistrationWindowViewModel(RegistrationModel model, ISecurityService securityService, IWindowManager windowManager)
    {
        _securityService = securityService;
        _windowManager = windowManager;
        _model = model;
        RegisterCommand = new DelegateCommand(OnRegister, CanRegister);
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

    public string ConfirmPassword
    {
        get => _confirmPassword;
        set => SetProperty(ref _confirmPassword, value);
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

    private void OnRegister()
    {
        if (!Validate()) return;

        var result = _securityService.Register(_model);
        if (result.Success)
        {
            _windowManager.SetMainWindow<LoginWindowViewModel>();
            _windowManager.ShowWindow<LoginWindowViewModel>();
        }

        ErrorMessage = result.ErrorMessage;
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
        ((DelegateCommand)RegisterCommand).RaiseCanExecuteChanged();
    }
}