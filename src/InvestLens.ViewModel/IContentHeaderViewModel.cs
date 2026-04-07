namespace InvestLens.ViewModel;

public interface IContentHeaderViewModel
{
    string WelcomeTitle { get; }
    string WelcomeDescription { get; }
    List<ButtonInfo> Buttons { get; }

    void AddButtons(List<ButtonModel>? buttonModels);
}