using InvestLens.Common.Helpers;
using InvestLens.Model;

namespace InvestLens.ViewModel.Wrappers;

public class StatWrapper : BindableBase
{
    private readonly Stat _model;

    public StatWrapper(Stat model)
    {
        _model = model;
    }

    public string Title => _model.Title.ToUpper();

    public string ValueDisplay
    {
        get
        {
            var result = NumberHelpers.ConvertValueToString(_model.Value);
            if (string.IsNullOrEmpty(_model.Unit)) return result;

            if (!_model.UnitIsSuffix) return $"{_model.Unit}{result}";
            
            result = $"{result}{_model.Unit}";
            if (_model.Value > 0) result = $"+{result}";

            return result;
        }
    }
}