using InvestLens.Model.Enums;

namespace InvestLens.ViewModel.Wrappers;

public class InitStepWrapper : BindableBase
{
    protected readonly IAsyncCommand _command;
    private CancellationToken _ct;
    protected List<InitStepWrapper> _nextSteps = [];
    protected InitStepState _state;

    public InitStepWrapper(
        string title,
        string description,
        IAsyncCommand command,
        CancellationToken ct)
    {
        Title = title;
        Description = description;
        _command = command;
        _ct = ct;
        State = InitStepState.Wait;
    }

    public string Icon
    {
        get => State switch
        {
            InitStepState.Wait => "⏳",
            InitStepState.Run => "",
            InitStepState.CompletedSuccessfully => "✅",
            InitStepState.Fail => "❌",
            InitStepState.Cancel => "⏹️",
            _ => throw new NotImplementedException(),
        };
    }

    public string Status
    {
        get => State switch
        {
            InitStepState.Wait => "Ожидание",
            InitStepState.Run => "Выплняется",
            InitStepState.CompletedSuccessfully => "Готово",
            InitStepState.Fail => "Ошибка",
            InitStepState.Cancel => "Отмена",
            _ => throw new NotImplementedException(),
        };
    }

    public InitStepState State
    {
        get => _state;
        set
        {
            if (!SetProperty(ref _state, value)) return;
            RaisePropertyChanged(nameof(Icon));
            RaisePropertyChanged(nameof(Status));
        }
    }

    public string Title { get; } = string.Empty;
    public string Description { get; } = string.Empty;

    public Exception? Exception { get; private set; }

    public void AddNextStep(InitStepWrapper nextStep)
    {
        if (_nextSteps.Contains(nextStep)) return;
        _nextSteps.Add(nextStep);
    }

    public void CalcelIsWait()
    {
        if (State == InitStepState.Wait)
        {
            State = InitStepState.Cancel;
        }

        foreach (var step in _nextSteps)
        {
            step.CalcelIsWait();
        }
    }

    public async Task ExecuteAsync()
    {
        State = InitStepState.Run;
        try
        {
            await _command.ExecuteAsync(null, _ct);
            State = InitStepState.CompletedSuccessfully;
            foreach (var nextStep in _nextSteps)
            {
                if (_ct.IsCancellationRequested) break;
                _ = nextStep.ExecuteAsync();
            }
        }
        catch(TaskCanceledException)
        {
            State = InitStepState.Cancel;
        }
        catch (Exception ex)
        {
            Exception = ex;
            State = InitStepState.Fail;
        }
    }
}