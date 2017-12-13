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
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace CogsExplorer.Modules.Face
{
    public class ImageInformation : ObservableBase
    {
        public ImageInformation(Canvas detectionCanvas)
        {
            DetectionCanvas = detectionCanvas;
            AnalyzeFaceCommand = new RelayCommand(async () => { await AnalyzeFaceAsync(); });
            DetectFacesCommand = new RelayCommand(async () => { await DetectFacesAsync(); });
        }

        public ICommand AnalyzeFaceCommand { get; private set; }
        public ICommand DetectFacesCommand { get; private set; }
        
        public Canvas DetectionCanvas { get; set; }

        private FaceInformation _selectedFace;
        public FaceInformation SelectedFace
        {
            get { return _selectedFace; }
            set { Set(ref _selectedFace, value); }
        }

        private ObservableCollection<FaceInformation> _faces = new ObservableCollection<FaceInformation>();
        public ObservableCollection<FaceInformation> Faces
        {
            get { return _faces; }
            set { Set(ref _faces, value); }
        }

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
        
        private byte[] _fileBytes;
        public byte[] FileBytes
        {
            get { return _fileBytes; }
            set { Set(ref _fileBytes, value); }
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

        public async Task<bool> DetectFacesAsync()
        {
            bool successful = false;

            this.IsBusy = true;

            try
            {
                var analysis = await Helpers.DetectionHelper.DetectFacesAsync(this.FileBytes);

                foreach (var region in analysis)
                {
                    Rectangle rectangle = new Rectangle()
                    {
                        Stroke = new SolidColorBrush(Colors.CornflowerBlue),
                        StrokeThickness = 3,
                        Fill = new SolidColorBrush(Color.FromArgb(60, 0, 0, 0)),
                    };

                    rectangle.SetValue(Canvas.LeftProperty, region.faceRectangle.left);
                    rectangle.SetValue(Canvas.TopProperty, region.faceRectangle.top);
                    rectangle.Width = region.faceRectangle.width;
                    rectangle.Height = region.faceRectangle.height;
                    rectangle.Tapped += OnFaceSelected;

                    FaceInformation faceInformation = new FaceInformation()
                    {
                        Gender = region.faceAttributes.gender,
                        Age = region.faceAttributes.age,
                        HairColor = region.faceAttributes.hair.hairColor[0].color,
                        IsSmiling = region.faceAttributes.smile > 0.5,
                        IsWearingMakeup = region.faceAttributes.makeup.eyeMakeup || region.faceAttributes.makeup.lipMakeup,
                        IsWearingGlasses = !region.faceAttributes.glasses.Equals("NoGlasses"),
                        HasFacialHair = (region.faceAttributes.facialHair.beard + region.faceAttributes.facialHair.moustache > 0.0)
                    };

                    this.Faces.Add(faceInformation);

                    rectangle.DataContext = faceInformation;

                    this.DetectionCanvas.Children.Add(rectangle);
                }
                
                successful = true;

            }
            catch (Exception ex)
            {
            }

            this.IsBusy = false;

            return successful;
        }

        private void OnFaceSelected(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            this.SelectedFace = (sender as Rectangle).DataContext as FaceInformation;
        }

        public async Task<bool> AnalyzeFaceAsync()
        {
            bool successful = false;

            this.IsBusy = true;

            try
            {
                List<string> tags = new List<string>();

                var analysis = await Helpers.DetectionHelper.DetectFacesAsync(this.FileBytes);
                
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
