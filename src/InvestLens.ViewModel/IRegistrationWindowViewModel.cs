using System.Collections;
using System.ComponentModel;
using System.Windows.Input;

namespace InvestLens.ViewModel;

public interface IRegistrationWindowViewModel : IConfirmPasswordSupport
{
    string Name { get; set; }
    string Surname { get; set; }
    string Login { get; set; }
    ICommand RegisterCommand { get; }
    ICommand LoginCommand { get; }
    string ErrorMessage { get; set; }
    bool HasErrorMessage { get; }
    bool HasErrors { get; }
    event PropertyChangedEventHandler? PropertyChanged;
    IEnumerable GetErrors(string? propertyName);
    event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
}