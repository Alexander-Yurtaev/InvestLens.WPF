using InvestLens.ViewModel.Windows;
using InvestLens.ViewModel.Wrappers;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace InvestLens.ViewModel.Design;

public class DesignSplashInitializationWindowViewModel : BindableBase, ISplashInitializationWindowViewModel
{
    private float _progressMax;
    private float _progressValue;

    public DesignSplashInitializationWindowViewModel()
    {
        CancelCommand = new DelegateCommand(() => { });

        ProgressValue = 0.47f;
        ProgressMax = 1.0f;

        var ct = new CancellationTokenSource().Token;

        var waitWrapper = new InitStepWrapper("Wait", "Wait Task", new AsyncDelegateCommand(() => Task.Delay(0)), ct);
        InitSteps.Add(waitWrapper);

        var runWrapper = new InitStepWrapper("Run", "Run Task", new AsyncDelegateCommand(async () => await Task.Delay(Timeout.Infinite)), ct);
        _ = runWrapper.ExecuteAsync();
        InitSteps.Add(runWrapper);

        var faileWrapper = new InitStepWrapper("Fail", "Fail Task", new AsyncDelegateCommand(async () => await Task.FromException(new Exception())), ct);
        _ = faileWrapper.ExecuteAsync();
        InitSteps.Add(faileWrapper);

        var completeWrapper = new InitStepWrapper("Complete", "Complete Task", new AsyncDelegateCommand(async () => await Task.CompletedTask), ct);
        _ = completeWrapper.ExecuteAsync();
        InitSteps.Add(completeWrapper);

        var cts = new CancellationTokenSource();
        cts.Cancel();
        var canceledWrapper = new InitStepWrapper("Cancel", "Cancel Task", new AsyncDelegateCommand(async () => await Task.FromCanceled(cts.Token)), ct);
        _ = canceledWrapper.ExecuteAsync();
        InitSteps.Add(canceledWrapper);
    }

    public ICommand CancelCommand { get; }

    public string Header => "Инициализация";

    public float ProgressMax 
    { 
        get => _progressMax; 
        set => SetProperty(ref _progressMax, value); 
    }

    public float ProgressValue
    {
        get => _progressValue;
        set => SetProperty(ref _progressValue, value);
    }

    public ObservableCollection<InitStepWrapper> InitSteps { get; set; } = [];
    
    public bool ShowCancelButton 
    { 
        get => true; 
        set { }
    }

    public async Task Load(bool? force = false)
    {
        await Task.CompletedTask;
    }
}