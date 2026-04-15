using InvestLens.Model;

namespace InvestLens.ViewModel.Wrappers;

public class CardWrapper : BindableBase
{
    private readonly Card _model;

#if DEBUG
    public CardWrapper() : this(new Card("Design"))
    {

    }
#endif

    public CardWrapper(Card model)
    {
        _model = model;
        Stats.AddRange(_model.Stats.Select(s => new StatWrapper(s)));
    }

    public string Title => _model.Title;
    public string CardType => _model.CardType;
    public string CardTypeForeground => _model.CardTypeForeground;
    public string CardTypeBackground => _model.CardTypeBackground;

    public List<StatWrapper> Stats { get; } = [];

    public string LastDateUpdate => $"Обновлено: {_model.LastDateUpdate}";
}