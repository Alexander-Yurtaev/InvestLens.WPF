namespace InvestLens.ViewModel;

public interface ISupportPortfolioType
{
    bool IsPortfolioSimpleType { get; }

    bool IsPortfolioComplexType { get; }
}