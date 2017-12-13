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

namespace CogsExplorer.Modules.Emotion
{
    public class ImageInformation : ObservableBase
    {
        public ImageInformation(Canvas detectionCanvas)
        {
            DetectionCanvas = detectionCanvas;
            AnalyzeEmotionCommand = new RelayCommand(async () => { await AnalyzeEmotionAsync(); });
          
        }

        public ICommand AnalyzeEmotionCommand { get; private set; }
       
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

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(ref _isBusy, value); }
        }
 
        public async Task<bool> AnalyzeEmotionAsync()
        {
            bool successful = false;

            this.IsBusy = true;

            try
            {
                var analysis = await Helpers.EmotionHelper.GetEmotionAnalysisAsync(Guid.NewGuid(), this.FileBytes);

                foreach (var face in analysis)
                {
                    Rectangle rectangle = new Rectangle()
                    {
                        Stroke = new SolidColorBrush(Colors.CornflowerBlue),
                        StrokeThickness = 3,
                        Fill = new SolidColorBrush(Color.FromArgb(60, 0, 0, 0)),
                    };

                    rectangle.SetValue(Canvas.LeftProperty, face.faceRectangle.left);
                    rectangle.SetValue(Canvas.TopProperty, face.faceRectangle.top);
                    rectangle.Width = face.faceRectangle.width;
                    rectangle.Height = face.faceRectangle.height;
                    rectangle.Tapped += OnFaceSelected;
 
                    var faceInformation = new FaceInformation()
                    {
                        FaceRectangle = new Windows.Foundation.Rect(face.faceRectangle.left, face.faceRectangle.top, face.faceRectangle.width, face.faceRectangle.height),
                        Scores = new ObservableCollection<EmotionScoreInformation>()
                    };

                    faceInformation.Scores.Add(new EmotionScoreInformation() { Label = "Anger", Score = face.scores.anger });
                    faceInformation.Scores.Add(new EmotionScoreInformation() { Label = "Contempt", Score = face.scores.contempt });
                    faceInformation.Scores.Add(new EmotionScoreInformation() { Label = "Disgust", Score = face.scores.disgust });
                    faceInformation.Scores.Add(new EmotionScoreInformation() { Label = "Fear", Score = face.scores.fear });
                    faceInformation.Scores.Add(new EmotionScoreInformation() { Label = "Happiness", Score = face.scores.happiness });
                    faceInformation.Scores.Add(new EmotionScoreInformation() { Label = "Neutral", Score = face.scores.neutral });
                    faceInformation.Scores.Add(new EmotionScoreInformation() { Label = "Sadness", Score = face.scores.sadness });
                    faceInformation.Scores.Add(new EmotionScoreInformation() { Label = "Surprise", Score = face.scores.surprise });

                    faceInformation.Scores = new ObservableCollection<EmotionScoreInformation>( faceInformation.Scores.OrderByDescending(o => o.Score));

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

        //public async Task<bool> AnalyzeImageAsync()
        //{
        //    bool successful = false;

        //    this.IsBusy = true;

        //    try
        //    {    
        //        var analysis = await Helpers.EmotionHelper.GetEmotionAnalysisAsync(Guid.NewGuid(), this.FileBytes);

        //        foreach (var face in analysis)
        //        {
        //            var faceInformation = new FaceInformation()
        //            {
        //                FaceRectangle = new Windows.Foundation.Rect(face.faceRectangle.left, face.faceRectangle.top, face.faceRectangle.width, face.faceRectangle.height),
        //                Scores = new ObservableCollection<EmotionScoreInformation>()
        //            };

        //            faceInformation.Scores.Add(new EmotionScoreInformation() { Label = "Anger", Score = face.scores.anger });
        //            faceInformation.Scores.Add(new EmotionScoreInformation() { Label = "Contempt", Score = face.scores.contempt });
        //            faceInformation.Scores.Add(new EmotionScoreInformation() { Label = "Disgust", Score = face.scores.disgust });
        //            faceInformation.Scores.Add(new EmotionScoreInformation() { Label = "Fear", Score = face.scores.fear });
        //            faceInformation.Scores.Add(new EmotionScoreInformation() { Label = "Happiness", Score = face.scores.happiness });
        //            faceInformation.Scores.Add(new EmotionScoreInformation() { Label = "Neutral", Score = face.scores.neutral });
        //            faceInformation.Scores.Add(new EmotionScoreInformation() { Label = "Sadness", Score = face.scores.sadness });
        //            faceInformation.Scores.Add(new EmotionScoreInformation() { Label = "Surprise", Score = face.scores.surprise });

        //            this.Faces.Add(faceInformation);                    
        //        }
                
        //        successful = true;

        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    this.IsBusy = false;

        //    return successful;
        //}

    }
}
