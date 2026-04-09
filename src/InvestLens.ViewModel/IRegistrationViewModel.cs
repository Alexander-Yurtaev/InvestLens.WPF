using System.Collections;
using System.ComponentModel;
using System.Windows.Input;

namespace InvestLens.ViewModel;

public interface IRegistrationViewModel
{
    string Name { get; set; }
    string Surname { get; set; }
    string Email { get; set; }
    string Password { get; set; }
    string ConfirmPassword { get; set; }
    ICommand RegisterCommand { get; }
    ICommand HasAccountCommand { get; }
    string ErrorMessage { get; set; }
    bool ShowErrorMessage { get; }
    bool HasErrors { get; }
    event PropertyChangedEventHandler? PropertyChanged;
    IEnumerable GetErrors(string? propertyName);
    event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
}