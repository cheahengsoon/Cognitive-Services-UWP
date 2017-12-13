using System;

using CogsExplorer.Helpers;
using CogsExplorer.Common;
using System.Windows.Input;

namespace CogsExplorer.ViewModels
{
    public class MainViewModel : ObservableBase
    {
        public MainViewModel()
        {
            RefreshAvailableServices();
        }

        private void RefreshAvailableServices()
        {
            this.AvailableServices.Add(new ServiceInformation() { ModuleType = typeof(Modules.ComputerVision.Views.StartPage), DisplayName = "Computer Vision", Description = "Extract rich information from images to categorize and process visual data and machine-assisted moderation of images to help curate your services.", ImageUrl = "ms-appx:///Images/Icons/ComputerVision.png", IsEnabled = true });
            this.AvailableServices.Add(new ServiceInformation() { ModuleType = typeof(Modules.Face.Views.StartPage), DisplayName = "Face", Description = "Detect human faces and compare similar ones, organize images into groups based on similarity, and identify previously tagged people in images.", ImageUrl = "ms-appx:///Images/Icons/Face.png", IsEnabled = true });
            this.AvailableServices.Add(new ServiceInformation() { ModuleType = typeof(Modules.Emotion.Views.StartPage), DisplayName = "Emotion & Text Analytics", Description = "Analyze faces and text to detect a range of feelings and personalize your app's responses.", ImageUrl = "ms-appx:///Images/Icons/Emotion.png", IsEnabled = true });
            this.AvailableServices.Add(new ServiceInformation() { ModuleType = typeof(Modules.LUIS.Views.StartPage), DisplayName = "LUIS & QnA Maker", Description = "Understand language contextually, so your app communicates with people in the way they speak.", ImageUrl = "ms-appx:///Images/Icons/Luis.png", IsEnabled = true });
            this.AvailableServices.Add(new ServiceInformation() { ModuleType = typeof(Modules.Search.Views.StartPage), DisplayName = "Search", Description = "Understand language contextually, so your app communicates with people in the way they speak.", ImageUrl = "ms-appx:///Images/Icons/Search.png", IsEnabled = true });
            this.AvailableServices.Add(new ServiceInformation() { ModuleType = typeof(Modules.Translation.Views.StartPage), DisplayName = "Translation", Description = "Easily perform real-time speech and text translation.", ImageUrl = "ms-appx:///Images/Icons/Translator.png", IsEnabled = true });
            this.AvailableServices.Add(new ServiceInformation() { ModuleType = typeof(Modules.VideoIndexer.Views.StartPage), DisplayName = "Video Indexer", Description = "Extract the insights from your videos using artificial intelligence technologies.", ImageUrl = "ms-appx:///Images/Icons/VideoIndexer.png", IsEnabled = true });
            this.AvailableServices.Add(new ServiceInformation() { DisplayName = "Custom Vision", Description = "Easily customize your own state-of-the-art computer vision models.", ImageUrl = "ms-appx:///Images/Icons/CustomVision.png", IsEnabled = false });
            this.AvailableServices.Add(new ServiceInformation() { DisplayName = "Project Cuzco", Description = "Find events associated with Wikipedia entities. Just start with an entity from Wikipedia, and the API will provide a list of related events organized by time.", ImageUrl = "ms-appx:///Images/Icons/Cuzco.png", IsEnabled = false });
            this.AvailableServices.Add(new ServiceInformation() { DisplayName = "Project Wollongong", Description = "Score the attractiveness of a location, based on how many of a particular amenity are within a specific distance.", ImageUrl = "ms-appx:///Images/Icons/Wollongong.png", IsEnabled = false });
            
        }
        
        private ServiceCollection _availableServices = new ServiceCollection();
        public ServiceCollection AvailableServices
        {
            get => _availableServices;
            set => Set(ref _availableServices, value);
        }

         
    }
}
