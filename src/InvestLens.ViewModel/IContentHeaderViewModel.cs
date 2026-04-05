using System.Windows.Input;

namespace InvestLens.ViewModel;

public interface IContentHeaderViewModel
{
    string WelcomeTitle { get; }
    string WelcomeDescription { get; }
    List<ButtonInfo> Buttons { get; }
}