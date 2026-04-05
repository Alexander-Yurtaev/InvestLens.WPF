using InvestLens.Model.Enums;
using System.ComponentModel;

namespace InvestLens.ViewModel.Services;

public interface IViewModelFactory
{
    INotifyPropertyChanged CreateViewModel(NodeType nodeType);
}