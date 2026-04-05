using System.ComponentModel;
using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public interface IDictionariesViewModel : INotifyPropertyChanged
{
    IContentHeaderViewModel ContentHeaderVm { get; }
    List<CardWrapper> Cards { get; }
}