using InvestLens.Model;
using System.Windows.Input;

namespace InvestLens.ViewModel.Wrappers;

public class CardWrapper : BindableBase
{
    private readonly Card _model;

#if DEBUG
    public CardWrapper() : this(new Card(0, "Design", true), async (wrapper) => await Task.Delay(0))
    {

    }
#endif

    public CardWrapper(Card model, Func<CardWrapper, Task>? deleteAction=null)
    {
        _model = model;
        Stats.AddRange(_model.Stats.Select(s => new StatWrapper(s)));

        if (deleteAction is not null)
        {
            DeleteCommand = new AsyncDelegateCommand<CardWrapper>(deleteAction);
        }
    }

    public int Id => _model.Id;
    public string Title => _model.Title;
    public string CardType => _model.CardType;
    public string CardTypeForeground => _model.CardTypeForeground;
    public string CardTypeBackground => _model.CardTypeBackground;

    public List<StatWrapper> Stats { get; } = [];

    public string LastDateUpdate => $"Обновлено: {_model.LastDateUpdate}";
    public ICommand? DeleteCommand { get; set; }
    public bool CanDelete => _model.CanDelete;
}