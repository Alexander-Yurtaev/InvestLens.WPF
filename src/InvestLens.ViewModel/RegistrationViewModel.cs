using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using InvestLens.Model;

namespace InvestLens.ViewModel;

public class RegistrationViewModel : ValidationViewModelBase, IRegistrationViewModel
{
    private string _confirmPassword = string.Empty;

    public RegistrationViewModel(RegistrationModel model)
    {
        Model = model;
        RegisterCommand = new DelegateCommand(OnRegister, CanRegister);
        this.PropertyChanged += OnPropertyChanged;
        InvalidateCommands();
    }

    public RegistrationModel Model { get; }

    [Required(ErrorMessage = "Имя должно быть заполнено")]
    public string Name
    {
        get => Model.Name;
        set
        {
            if (string.Equals(Model.Name, value)) return;
            Model.Name = value;
            RaisePropertyChanged();
            ValidateProperty(Model.Name);
        }
    }

    [Required(ErrorMessage = "Фамилия должна быть заполнена")]
    public string Surname
    {
        get => Model.Surname;
        set
        {
            if (string.Equals(Model.Surname, value)) return;
            Model.Surname = value;
            RaisePropertyChanged();
            ValidateProperty(Model.Surname);
        }
    }

    [Required(ErrorMessage = "Адрес должн быть заполнен")]
    [EmailAddress(ErrorMessage = "Неверный формат")]
    public string Email
    {
        get => Model.Email;
        set
        {
            if (string.Equals(Model.Email, value)) return;
            Model.Email = value;
            RaisePropertyChanged();
            ValidateProperty(Model.Email);
        }
    }

    public string Password
    {
        get => Model.Password;
        set
        {
            if (string.Equals(Model.Password, value)) return;
            Model.Password = value;
            RaisePropertyChanged();
        }
    }

    public string ConfirmPassword
    {
        get => _confirmPassword;
        set => SetProperty(ref _confirmPassword, value);
    }

    public ICommand RegisterCommand { get; }

    private void OnRegister()
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
    }

    private bool CanRegister()
    {
        return !HasErrors;
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(this.HasErrors))
        {
            InvalidateCommands();
        }
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