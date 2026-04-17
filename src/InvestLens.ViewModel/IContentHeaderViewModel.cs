using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel;

public interface IContentHeaderViewModel
{
    string WelcomeTitle { get;  }
    string WelcomeDescription { get; }
    List<ButtonWrapper> Buttons { get; }

    void AddButtons(List<ButtonModel>? buttonModels);
    void SetWelcomeTitle(string title);
}