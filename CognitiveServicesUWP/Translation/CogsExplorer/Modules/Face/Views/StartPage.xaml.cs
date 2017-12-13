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

namespace CogsExplorer.Modules.Face.Views
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
            this.ViewModel.Initialize(this.detectionCanvas);
 
        }

        private async void Goo()
        {
            await Helpers.UtilitiesHelper.ClearAllFaceInformationAsync();            
        }
        
        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void OnCreatePersonGroupOpening(object sender, object e)
        {
            this.ViewModel.CreateEmptyPersonGroup();
        }

        private void OnCreatePersonOpening(object sender, object e)
        {
            this.ViewModel.CreateEmptyPerson();
        }

        private async void OnDetectFacesClick(object sender, RoutedEventArgs e)
        {
            var imageAdded = await ViewModel.BrowseAndDetectFacesAsync();
        }
        
        private async void OnIndentifyFaceClick(object sender, RoutedEventArgs e)
        {
            var imageAdded = await ViewModel.BrowseAndIdentifyFaceAsync();
        }

        private async void OnAddFaceClick(object sender, RoutedEventArgs e)
        {
            var faceAdded = await ViewModel.BrowseDetectAndAddFaceAsync();
        }

        private async void OnTrainClick(object sender, RoutedEventArgs e)
        {
            await ViewModel.TrainPeopleGroupAsync();
        }

        
    }
}
