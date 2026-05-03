using InvestLens.ViewModel.Wrappers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace InvestLens.ViewModel.Windows
{
    public interface ISplashInitializationWindowViewModel : ILoadableViewModel
    {
        ICommand CancelCommand { get; }
        string Header { get; }
        ObservableCollection<InitStepWrapper> InitSteps { get; set; }
        bool ShowCancelButton { get; set; }

        float ProgressMax { get; }
        float ProgressValue { get; set; }
    }
}