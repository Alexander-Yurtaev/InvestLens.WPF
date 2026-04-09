using System.Windows;
using InvestLens.ViewModel;

namespace InvestLens.App.Windows
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow(ILoginWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
