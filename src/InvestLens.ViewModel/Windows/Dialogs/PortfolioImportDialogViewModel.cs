using InvestLens.ViewModel.Services;
using System.Windows.Input;

namespace InvestLens.ViewModel.Windows.Dialogs;

public class PortfolioImportDialogViewModel : BaseDialogViewModel, IPortfolioImportDialogViewModel
{
    private string _fileFullName;
    private string _fileName;

    public PortfolioImportDialogViewModel(IWindowManager windowManager) : base(windowManager)
    {
        SelectFileCommand = new DelegateCommand(OnSelectFile);
        CancelSelectFileCommand = new DelegateCommand(OnCancelSelectFile);
    }

    public override string Header => "Импорт";

    public override string ActionContext => "Импортировать";

    public override bool ShowCancelButton => true;

    public string FileName => IsSelected ? Path.GetFileName(_fileFullName) : "";

    public bool IsSelected => !string.IsNullOrEmpty(_fileFullName);

    public ICommand SelectFileCommand { get; set; }
    public ICommand CancelSelectFileCommand { get; set; }

    protected override void CloseWindow()
    {
        WindowManager.CloseWindow<PortfolioImportDialogViewModel>();
    }

    protected override bool CanAccept() => IsSelected;

    private void OnSelectFile()
    {
        _fileFullName = WindowManager.ShowSelectFileDialog("Импорт сделок", "CSV|*.csv");
        RaisePropertyChanged(nameof(FileName));
        RaisePropertyChanged(nameof(IsSelected));
        ((DelegateCommand)AcceptCommand).RaiseCanExecuteChanged();
    }

    private void OnCancelSelectFile()
    {
        _fileFullName = "";
        RaisePropertyChanged(nameof(FileName));
        RaisePropertyChanged(nameof(IsSelected));
        ((DelegateCommand)AcceptCommand).RaiseCanExecuteChanged();
    }
}