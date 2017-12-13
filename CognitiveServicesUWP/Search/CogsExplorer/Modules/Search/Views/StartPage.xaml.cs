using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CogsExplorer.Modules.Search.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartPage : Page
    {  
        public ServiceViewModel ViewModel { get; } = new ServiceViewModel();
        public StartPage()
        {
            this.InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this.ViewModel;            
        }
   
        private void OnSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            this.ViewModel.SuggestionQuery = (string)args.SelectedItem;
            this.ViewModel.StartSearch();
        }

        private async void OnSuggestionTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                await this.ViewModel.PopulateSuggestionsAsync();
            }
        }

        private async void OnVideoSelected(object sender, ItemClickEventArgs e)
        {
            var selectedVideo = e.ClickedItem as VideoInformation;

            await Windows.System.Launcher.LaunchUriAsync(new Uri(selectedVideo.ContentUrl));
        }

        private async void OnNewsSelected(object sender, ItemClickEventArgs e)
        {
            var selectedNews = e.ClickedItem as NewsInformation;

            await Windows.System.Launcher.LaunchUriAsync(new Uri(selectedNews.ArticleUrl));
        }
    }
}
