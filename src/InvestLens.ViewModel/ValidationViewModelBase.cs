using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace InvestLens.ViewModel;

public abstract class ValidationViewModelBase : BindableBase, INotifyDataErrorInfo
{
    private readonly Dictionary<string, List<string>> _errorsByPropertyName = [];

    protected ValidationViewModelBase()
    {
        this.PropertyChanged += OnPropertyChanged;
    }

    public bool HasErrors => _errorsByPropertyName.Any();

    public IEnumerable GetErrors(string? propertyName)
    {
        return propertyName is not null && _errorsByPropertyName.TryGetValue(propertyName, out var errors)
            ? errors
            : [];
    }
    
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e)
    {
        ErrorsChanged?.Invoke(this, e);
    }

    protected void AddError(string errorMessage, string? propertyName)
    {
        if (propertyName is null) return;

        if (!_errorsByPropertyName.ContainsKey(propertyName))
        {
            _errorsByPropertyName[propertyName] = [];
        }

        if (!_errorsByPropertyName[propertyName].Contains(errorMessage))
        {
            _errorsByPropertyName[propertyName].Add(errorMessage);
        }

        OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
        RaisePropertyChanged(nameof(HasErrors));
    }

    protected void ClearErrors(string? propertyName)
    {
        if (propertyName is null)
        {
            _errorsByPropertyName.Clear();
        } else if (!_errorsByPropertyName.Remove(propertyName)) return;

        OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
        RaisePropertyChanged(nameof(HasErrors));
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(this.HasErrors))
        {
            InvalidateCommands();
        }
    }

    protected virtual bool Validate()
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

    protected virtual void ValidateProperty(object? newValue, [CallerMemberName] string? propertyName = null)
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

    protected virtual void InvalidateCommands()
    {

    }
}