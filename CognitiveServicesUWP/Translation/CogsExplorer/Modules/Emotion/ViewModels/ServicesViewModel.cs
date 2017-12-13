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

namespace CogsExplorer.Modules.Emotion
{
    public class ServiceViewModel : ObservableBase
    {
        public ServiceViewModel()
        {
            RefreshNewsCommand = new RelayCommand(async () => { await RefreshNewsAsync(); });
            IdentifyLanguageCommand = new RelayCommand(async () => { await IdentifyLanguageAsync(); });
            ExtractKeyPhrasesCommand = new RelayCommand(async () => { await ExtractKeyPhrasesAsync(); });
            DetectTopicsCommand = new RelayCommand(async () => { await DetectTopicsAsync(); });

            this.NewsSearchQuery = "Microsoft";
        }
        
        public ICommand RefreshNewsCommand { get; private set; }
        public ICommand IdentifyLanguageCommand { get; private set; }
        public ICommand ExtractKeyPhrasesCommand { get; private set; }
        public ICommand DetectTopicsCommand { get; private set; }

        private ObservableCollection<NewsInformation> _currentNews = new ObservableCollection<NewsInformation>();
        public ObservableCollection<NewsInformation> CurrentNews
        {
            get { return _currentNews; }
            set { Set(ref _currentNews, value); }
        }              

        private ImageInformation _currentImage;
        public ImageInformation CurrentImage
        {
            get { return _currentImage; }
            set { Set(ref _currentImage, value); }
        }

        private string _newsSearchQuery;
        public string NewsSearchQuery
        {
            get { return _newsSearchQuery; }
            set { Set(ref _newsSearchQuery, value); }
        }

        private string _analysisContent;
        public string AnalysisContent
        {
            get { return _analysisContent; }
            set { Set(ref _analysisContent, value); }
        }

        private LanguageInformation _detectedLanguage;
        public LanguageInformation DetectedLanguage
        {
            get { return _detectedLanguage; }
            set { Set(ref _detectedLanguage, value); }
        }

        private ObservableCollection<string> _extractedKeyPhrases = new ObservableCollection<string>();
        public ObservableCollection<string> ExtractedKeyPhrases
        {
            get { return _extractedKeyPhrases; }
            set { Set(ref _extractedKeyPhrases, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(ref _isBusy, value); }
        }

        public void Initialize(Canvas detectionCanvas)
        {
            this.InitializeDetectionCanvas(detectionCanvas);
        }

        public void InitializeDetectionCanvas(Canvas detectionCanvas)
        {
            this.DetectionCanvas = detectionCanvas;
        }

        public Canvas DetectionCanvas { get; set; }

        public async Task<bool> RefreshNewsAsync()
        {
            this.IsBusy = true;

            this.CurrentNews.Clear();

            var searchResults = await Helpers.NewsHelper.GetNewsAsync(this.NewsSearchQuery, 100);

            foreach(var result in searchResults)
            {
                this.CurrentNews.Add(result);
            }

            this.IsBusy = false;

            return searchResults.Count > 0;
        }

        public async Task<bool> BrowseAndDetectFacesAsync()
        {
            this.CurrentImage = null;

            this.DetectionCanvas.Children.OfType<Windows.UI.Xaml.Shapes.Rectangle>().ToList().ForEach(b => this.DetectionCanvas.Children.Remove(b));

            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            var file = await picker.PickSingleFileAsync();

            this.IsBusy = true;

            var fileProperties = await file.Properties.GetImagePropertiesAsync();

            byte[] imageBytes = await file.AsByteArrayAsync();

            var image = new ImageInformation(this.DetectionCanvas)
            {    
                FileBytes = imageBytes,
                Url = file.Path,
                ImageHeight = (int)fileProperties.Height,
                ImageWidth = (int)fileProperties.Width,

            };

            image.Url = await Helpers.StorageHelper.SaveToTemporaryFileAsync("Emotion", file.Name, imageBytes);

            this.CurrentImage = image;

            this.IsBusy = false;

            return this.CurrentImage != null;

        }

        private async Task<bool> DetectTopicsAsync()
        {
            this.IsBusy = true;

            var documents = this.CurrentNews.Select(s => s.Title).ToList();

            documents.AddRange(documents.Take(100 - documents.Count));

            var extractedTopics = await Helpers.TextAnalyticsHelper.DetectTopicsAsync(documents, new List<string>(), new List<string>());

            //foreach (var keyPhrase in extractedKeyPhrases.OrderBy(o => o))
            //{
            //    this.ExtractedKeyPhrases.Add(keyPhrase);
            //}

            this.IsBusy = false;

            return this.ExtractedKeyPhrases.Count > 0;
        }

        private async Task<bool> ExtractKeyPhrasesAsync()
        {
            this.IsBusy = true;

            this.DetectedLanguage = await Helpers.TextAnalyticsHelper.DetectLanguagesAsync(new List<string>() { this.AnalysisContent });
            
            var extractedKeyPhrases = await Helpers.TextAnalyticsHelper.ExtractKeyPhrasesAsync(new List<string>() { this.AnalysisContent }, this.DetectedLanguage.Abbreviation);

            foreach(var keyPhrase in extractedKeyPhrases.OrderBy(o => o))
            {
                this.ExtractedKeyPhrases.Add(keyPhrase);
            }

            this.IsBusy = false;

            return this.ExtractedKeyPhrases.Count > 0;
        }

        private async Task<bool> IdentifyLanguageAsync()
        {
            this.IsBusy = true;

            this.DetectedLanguage = await Helpers.TextAnalyticsHelper.DetectLanguagesAsync(new List<string>() { this.AnalysisContent });

            this.IsBusy = false;

            return this.DetectedLanguage != null;
           
        }

    }
}
