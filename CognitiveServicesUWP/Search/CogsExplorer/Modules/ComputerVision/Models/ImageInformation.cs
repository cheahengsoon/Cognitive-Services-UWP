using CogsExplorer.Common;
using CogsExplorer.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace CogsExplorer.Modules.ComputerVision
{
    public class ImageInformation : ObservableBase
    {
        public ImageInformation()
        {
            AnalyzeImageCommand = new RelayCommand(async () => { await AnalyzeImageAsync(); });
            AnalyzeOcrCommand = new RelayCommand(async () => { await AnalyzeOcrAsync(); });
            AnalyzeHandwritingCommand = new RelayCommand(async () => { await AnalyzeHandwritingAsync(); });
            ViewImageInfoCommand = new RelayCommand(async () => { await AnalyzeImageAsync(); });
        }

        public ICommand AnalyzeImageCommand { get; private set; }
        public ICommand AnalyzeOcrCommand { get; private set; }
        public ICommand AnalyzeHandwritingCommand { get; private set; }
        public ICommand ViewImageInfoCommand { get; private set; }

        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { Set(ref _displayName, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { Set(ref _description, value); }
        }

        private string _url;
        public string Url
        {
            get { return _url; }
            set { Set(ref _url, value); }
        }

        private ObservableCollection<string> _tags;
        public ObservableCollection<string> Tags
        {
            get { return _tags; }
            set { Set(ref _tags, value); }
        }

        private byte[] _fileBytes;
        public byte[] FileBytes
        {
            get { return _fileBytes; }
            set { Set(ref _fileBytes, value); }
        }

        private double _adultScore;
        public double AdultScore
        {
            get { return _adultScore; }
            set { Set(ref _adultScore, value); }
        }

        private double _racyScore;
        public double RacyScore
        {
            get { return _racyScore; }
            set { Set(ref _racyScore, value); }
        }

        private List<string> _dominantColors;
        public List<string> DominantColors
        {
            get { return _dominantColors; }
            set { Set(ref _dominantColors, value); }
        }

        private SolidColorBrush _accentColor;
        public SolidColorBrush AccentColor
        {
            get { return _accentColor; }
            set { Set(ref _accentColor, value); }
        }

        private bool _isClipart;
        public bool IsClipart
        {
            get { return _isClipart; }
            set { Set(ref _isClipart, value); }
        }

        private bool _isLineDrawing;
        public bool IsLineDrawing
        {
            get { return _isLineDrawing; }
            set { Set(ref _isLineDrawing, value); }
        }

        private int _imageWidth;
        public int ImageWidth
        {
            get { return _imageWidth; }
            set { Set(ref _imageWidth, value); }
        }

        private int _imageHeight;
        public int ImageHeight
        {
            get { return _imageHeight; }
            set { Set(ref _imageHeight, value); }
        }

        private string _imageFormat;
        public string ImageFormat
        {
            get { return _imageFormat; }
            set { Set(ref _imageFormat, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(ref _isBusy, value); }
        }

        public async Task<bool> AnalyzeOcrAsync()
        {
            bool successful = false;

            this.IsBusy = true;

            try
            {
                List<string> tags = new List<string>();

                var analysis = await Helpers.ImageHelper.GetOcrAnalysisAsync(Guid.NewGuid(), this.FileBytes);

                foreach (var region in analysis.regions)
                {
                    foreach (var line in region.lines)
                    {
                        tags.AddRange(line.words.Select(s => s.text));
                    }
                }

                this.Description = $"Image with content in {analysis.language.ToUpper()} containing {tags.Count} words";
                this.Tags = new ObservableCollection<string>(tags);

                successful = true;

            }
            catch (Exception ex)
            {
            }

            this.IsBusy = false;

            return successful;
        }

        public async Task<bool> AnalyzeHandwritingAsync()
        {
            bool successful = false;

            this.IsBusy = true;

            try
            {
                string description = "";
                List<string> tags = new List<string>();

                var analysis = await Helpers.ImageHelper.GetHandwritingAnalysisAsync(Guid.NewGuid(), this.FileBytes);

                foreach (var line in analysis.recognitionResult.lines)
                {
                    description += line.text + " ";
                    tags.AddRange(line.words.Select(s => s.text));
                }

                this.Description = description.Substring(0, Math.Min(50, description.Length)) + "...";
                this.Tags = new ObservableCollection<string>(tags);

                successful = true;

            }
            catch (Exception ex)
            {
            }

            this.IsBusy = false;

            return successful;
        }

        public async Task<bool> AnalyzeImageAsync()
        {
            bool successful = false;

            this.IsBusy = true;

            try
            {    
                var analysis = await Helpers.ImageHelper.GetImageAnalysisAsync(Guid.NewGuid(), this.FileBytes);

                this.Description = analysis.caption.ToFirstCharUpper();
                this.Tags = new ObservableCollection<string>(analysis.tags);
                this.IsClipart = Convert.ToBoolean(analysis.details.imageType.clipArtType);
                this.IsLineDrawing = Convert.ToBoolean(analysis.details.imageType.lineDrawingType);
                this.DominantColors = analysis.details.color.dominantColors.ToList();
                this.AccentColor = new SolidColorBrush(($"#FF{analysis.details.color.accentColor}").GetColorFromHex());
                this.AdultScore = analysis.details.adult.adultScore;
                this.RacyScore = analysis.details.adult.racyScore;
                this.ImageFormat = analysis.details.metadata.format;
                this.ImageHeight = analysis.details.metadata.height;
                this.ImageWidth = analysis.details.metadata.width;
                
                successful = true;

            }
            catch (Exception ex)
            {
            }

            this.IsBusy = false;

            return successful;
        }

    }
}
