using InvestLens.Model.Crud.Portfolio;
using InvestLens.ViewModel.Services;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace InvestLens.ViewModel.Windows.Dialogs;

public class PortfolioImportDialogViewModel : BaseDialogViewModel, IPortfolioImportDialogViewModel
{
    private string _fileFullName;
    private string _fileName;
    private bool _mergeMode;
    private bool _recreateMode;
    private string _errorMessage;

    public PortfolioImportDialogViewModel(IWindowManager windowManager) : base(windowManager)
    {
        SelectFileCommand = new DelegateCommand(OnSelectFile);
        CancelSelectFileCommand = new DelegateCommand(OnCancelSelectFile);

        FileFullName = "";
        MergeMode = true;
    }

    public override string Header => "Импорт";

    public override string ActionContext => "Импортировать";

    public override bool ShowCancelButton => true;

    [Required]
    public string FileFullName
    {
        get => string.IsNullOrEmpty(_fileFullName) ? "" : _fileFullName;
        set
        {
            SetProperty(ref _fileFullName, string.IsNullOrEmpty(value) ? "" : value);
            ValidateProperty(FileFullName);
        }
    }

    public string FileName => IsSelected ? Path.GetFileName(FileFullName) : "";

    public bool IsSelected => !string.IsNullOrEmpty(FileFullName);

    public bool MergeMode
    {
        get => _mergeMode;
        set
        {
            SetProperty(ref _mergeMode, value);
            ValidateProperty(MergeMode);
        }
    }

    public bool RecreateMode
    {
        get => _recreateMode;
        set
        {
            SetProperty(ref _recreateMode, value);
            ValidateProperty(MergeMode);
        }
    }

    public string ErrorMessage
    {
        get
        {
            return ((List<string>)GetErrors(nameof(MergeMode)))?.FirstOrDefault() ?? "";
        }
    }

    public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

    public ICommand SelectFileCommand { get; set; }
    public ICommand CancelSelectFileCommand { get; set; }

    public bool IsConfirmed { get; private set; }

    protected override void OnAccept()
    {
        if (!Validate()) return;

        IsConfirmed = true;
        base.OnAccept();
    }

    protected override void OnCancel()
    {
        FileFullName = "";
        IsConfirmed = false;
        base.OnCancel();
    }

    protected override void CloseWindow()
    {
        WindowManager.CloseWindow<PortfolioImportDialogViewModel>();
    }

    protected override bool CanAccept() => !HasErrors;

    protected override bool Validate()
    {
        base.Validate();
        ValidateProperty(MergeMode, nameof(MergeMode));
        return !HasErrors;
    }

    protected override void ValidateProperty(object? newValue, [CallerMemberName] string? propertyName = null)
    {
        base.ValidateProperty(newValue, propertyName);

        if (propertyName == nameof(MergeMode) || propertyName == nameof(RecreateMode))
        {
            if (MergeMode == false && RecreateMode == false)
            {
                AddError("Выберите режим импорта", nameof(MergeMode));
            } else if (MergeMode == true && RecreateMode == true)
            {
                AddError("Выберите только один режим импорта", nameof(MergeMode));
            }
            else
            {
                ClearErrors(nameof(MergeMode));
            }

            RaisePropertyChanged(nameof(HasErrorMessage));
            RaisePropertyChanged(nameof(ErrorMessage));
        }
    }

    protected override void InvalidateCommands()
    {
        ((DelegateCommand)AcceptCommand).RaiseCanExecuteChanged();
    }

    private void OnSelectFile()
    {
        FileFullName = WindowManager.ShowSelectFileDialog("Импорт сделок", "CSV|*.csv");
        RaisePropertyChanged(nameof(FileName));
        RaisePropertyChanged(nameof(IsSelected));
    }

    private void OnCancelSelectFile()
    {
        FileFullName = "";
        RaisePropertyChanged(nameof(FileName));
        RaisePropertyChanged(nameof(IsSelected));
    }
}