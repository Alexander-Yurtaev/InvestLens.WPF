using InvestLens.Model;
using InvestLens.Model.Enums;

namespace InvestLens.ViewModel.Wrappers;

public class SecurityWrapper
{
    private readonly SecurityModel _model;

    public SecurityWrapper(SecurityModel model)
    {
        _model = model;
    }

    public string SecId => _model.SecId;
    public string Name => _model.Name;
    public string SecTypeDisplay => GetSecTypeDisplay(_model.SecType);

    private string GetSecTypeDisplay(SecurityType secType)
    {
        return secType switch
        {
            SecurityType.None => "Нет данных",
            SecurityType.common_share => "Акции",
            SecurityType.exchange_bond => "Облигации",
            SecurityType.etf_ppif => "ETF",
            _ => throw new ArgumentOutOfRangeException(secType.ToString()),
        };
    }
}