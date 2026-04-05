using InvestLens.Common.Helpers;
using InvestLens.Model;

namespace InvestLens.ViewModel.Wrappers;

public class SecurityInfoWrapper
{
    private readonly SecurityInfo _model;

    public SecurityInfoWrapper(SecurityInfo model)
    {
        _model = model;
    }

    public string SecId => _model.SecId;
    public string Name => _model.Name;
    public int Count => _model.Count;
    public double AveragePrice => _model.AveragePrice;
    public double CurrentPrice => _model.CurrentPrice;
    public double TotalPrice => _model.TotalPrice;

    public string ProfitDisplay
    {
        get
        {
            var result = NumberHelpers.ConvertValueToString(_model.Profit);
            if (_model.Profit > 0) result = $"+{result}";
            return result;
        }
    }
}