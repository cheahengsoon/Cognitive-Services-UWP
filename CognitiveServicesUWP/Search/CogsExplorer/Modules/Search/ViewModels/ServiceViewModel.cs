using CogsExplorer.Common;
using CogsExplorer.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace CogsExplorer.Modules.Search
{
    public class ServiceViewModel : ObservableBase
    {
        public ServiceViewModel()
        {
            InitializeLocationCommand = new RelayCommand(async () => { await InitializeLocationAsync(); });
        }

        public ICommand InitializeLocationCommand { get; private set; }

        public Services.LocationService LocationService { get; set; }

        private Windows.Devices.Geolocation.BasicGeoposition _currentLocation;
        public Windows.Devices.Geolocation.BasicGeoposition CurrentLocation
        {
            get { return _currentLocation; }
            set { Set(ref _currentLocation, value); }
        }



        private string _suggestionQuery;
        public string SuggestionQuery
        {
            get { return _suggestionQuery; }
            set { Set(ref _suggestionQuery, value); }
        }

        private ObservableCollection<string> _suggestions = new ObservableCollection<string>();
        public ObservableCollection<string> Suggestions
        {
            get { return _suggestions; }
            set { Set(ref _suggestions, value); }
        }

        private ObservableCollection<WebInformation> _webResults = new ObservableCollection<WebInformation>();
        public ObservableCollection<WebInformation> WebResults
        {
            get { return _webResults; }
            set { Set(ref _webResults, value); }
        }

        private ObservableCollection<ImageInformation> _imageResults = new ObservableCollection<ImageInformation>();
        public ObservableCollection<ImageInformation> ImageResults
        {
            get { return _imageResults; }
            set { Set(ref _imageResults, value); }
        }

        private ObservableCollection<VideoInformation> _videoResults = new ObservableCollection<VideoInformation>();
        public ObservableCollection<VideoInformation> VideoResults
        {
            get { return _videoResults; }
            set { Set(ref _videoResults, value); }
        }

        private ObservableCollection<NewsInformation> _newsResults = new ObservableCollection<NewsInformation>();
        public ObservableCollection<NewsInformation> NewsResults
        {
            get { return _newsResults; }
            set { Set(ref _newsResults, value); }
        }

        private ObservableCollection<EntityInformation> _entityResults = new ObservableCollection<EntityInformation>();
        public ObservableCollection<EntityInformation> EntityResults
        {
            get { return _entityResults; }
            set { Set(ref _entityResults, value); }
        }

        private ObservableCollection<PlaceInformation> _placeResults = new ObservableCollection<PlaceInformation>();
        public ObservableCollection<PlaceInformation> PlaceResults
        {
            get { return _placeResults; }
            set { Set(ref _placeResults, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(ref _isBusy, value); }
        }

        
        public async void StartSearch()
        {
            this.IsBusy = true;

            string searchQuery = this.SuggestionQuery;
            
            //WEB RESULTS
            this.WebResults.Clear();
            var webResults = await Helpers.SearchHelper.SearchWebAsync(searchQuery);
            foreach (var result in webResults)
            {
                this.WebResults.Add(result);
            }

            //IMAGE RESULTS
            this.ImageResults.Clear();
            var imageResults = await Helpers.SearchHelper.SearchImagesAsync(searchQuery);
            foreach (var result in imageResults)
            {
                this.ImageResults.Add(result);
            }

            //VIDEO RESULTS
            this.VideoResults.Clear();
            var videoResults = await Helpers.SearchHelper.SearchVideosAsync(searchQuery);
            foreach (var result in videoResults)
            {
                this.VideoResults.Add(result);
            }

            //NEWS RESULTS
            this.NewsResults.Clear();
            var newsResults = await Helpers.SearchHelper.SearchNewsAsync(searchQuery);
            foreach (var result in newsResults)
            {
                this.NewsResults.Add(result);
            }

            //ENTITY RESULTS
            this.EntityResults.Clear();
            this.PlaceResults.Clear();
            var entityResults = await Helpers.SearchHelper.SearchEntitiesAsync(searchQuery, this.CurrentLocation);

            foreach (var result in entityResults.Entities)
            {
                this.EntityResults.Add(result);
            }

            foreach (var result in entityResults.Places)
            {
                this.PlaceResults.Add(result);
            }

            this.IsBusy = false;
        }
        public async Task PopulateSuggestionsAsync()
        {
            if (this.SuggestionQuery.Length > 2)
            {
                this.Suggestions.Clear();

                var suggestions = await Helpers.SearchHelper.GetSuggestionsAsync(this.SuggestionQuery, this.CurrentLocation);

                foreach (var suggestion in suggestions)
                {
                    this.Suggestions.Add(suggestion);
                }
            }

            return;
        }


        public async Task InitializeLocationAsync()
        {
            this.LocationService = new Services.LocationService();

            await this.LocationService.InitializeAsync();

            await this.LocationService.StartListeningAsync();

            this.CurrentLocation = this.LocationService.CurrentPosition.Coordinate.Point.Position;

            return;
        }

    }
}
