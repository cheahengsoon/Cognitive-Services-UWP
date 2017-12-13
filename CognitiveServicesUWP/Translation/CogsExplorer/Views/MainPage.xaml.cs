using CogsExplorer.ViewModels;

using Windows.UI.Xaml.Controls;

namespace CogsExplorer.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();
        public MainPage()
        {
            InitializeComponent();
        }
    }
}
