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

namespace CogsExplorer.Modules.Face
{
    public class ServiceViewModel : ObservableBase
    {
        public ServiceViewModel()
        {
            RefreshPersonGroupsCommand = new RelayCommand(() => { RefreshPersonGroups(); });
            RefreshPersonsCommand = new RelayCommand(() => { RefreshPersons(); });
            RefreshPersonCommand = new RelayCommand(() => { RefreshPerson(); });
            CreateEmptyPersonGroupCommand = new RelayCommand(() => { CreateEmptyPersonGroup(); });
            SavePersonGroupCommand = new RelayCommand(async () => { await SavePersonGroupAsync(); });
            SavePersonCommand = new RelayCommand(async () => { await SavePersonAsync(); });
        }

        public ICommand RefreshPersonGroupsCommand { get; private set; }
        public ICommand RefreshPersonsCommand { get; private set; }
        public ICommand RefreshPersonCommand { get; private set; }
        public ICommand CreateEmptyPersonGroupCommand { get; private set; }
        public ICommand SavePersonGroupCommand { get; private set; }
        public ICommand SavePersonCommand { get; private set; }

        public void Initialize(Canvas detectionCanvas)
        {
            this.InitializeDetectionCanvas(detectionCanvas);

            RefreshPersonGroups();
        }

        public void InitializeDetectionCanvas(Canvas detectionCanvas)
        {
            this.DetectionCanvas = detectionCanvas;
        }

        public Canvas DetectionCanvas { get; set; }
        
        private ObservableCollection<PersonGroupInformation> _currentPersonGroups = new ObservableCollection<PersonGroupInformation>();
        public ObservableCollection<PersonGroupInformation> CurrentPersonGroups
        {
            get { return _currentPersonGroups; }
            set { Set(ref _currentPersonGroups, value); }
        }

        private PersonGroupInformation _selectedPersonGroup;
        public PersonGroupInformation SelectedPersonGroup
        {
            get { return _selectedPersonGroup; }
            set { Set(ref _selectedPersonGroup, value); }
        }

        private ObservableCollection<PersonInformation> _currentPersons = new ObservableCollection<PersonInformation>();
        public ObservableCollection<PersonInformation> CurrentPersons
        {
            get { return _currentPersons; }
            set { Set(ref _currentPersons, value); }
        }

        private PersonInformation _selectedPerson;
        public PersonInformation SelectedPerson
        {
            get { return _selectedPerson; }
            set { Set(ref _selectedPerson, value); }
        }

        private PersonInformation _identifiedPerson;
        public PersonInformation IdentifiedPerson
        {
            get { return _identifiedPerson; }
            set { Set(ref _identifiedPerson, value); }
        }

        private ImageInformation _currentImage;
        public ImageInformation CurrentImage
        {
            get { return _currentImage; }
            set { Set(ref _currentImage, value); }
        }

        private ObservableCollection<ImageInformation> _currentImages = new ObservableCollection<ImageInformation>();
        public ObservableCollection<ImageInformation> CurrentImages
        {
            get { return _currentImages; }
            set { Set(ref _currentImages, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(ref _isBusy, value); }
        }

        public void CreateEmptyPersonGroup()
        {
            this.SelectedPersonGroup = new PersonGroupInformation()
            {
                DisplayName = "",
                Id = "",
            };
        }

        public void CreateEmptyPerson()
        {
            this.SelectedPerson = new PersonInformation()
            {
                DisplayName = "",
                Id = "",
            };
        }
        
        public async Task<bool> TrainPeopleGroupAsync()
        {
            this.IsBusy = true;

            bool isTrained = await Helpers.RecognitionHelper.TrainPersonGroupAsync(this.SelectedPersonGroup.Id);

            this.IsBusy = false;

            return isTrained;
        }

        private async Task<bool> SavePersonGroupAsync()
        {
            var personGroup = await Helpers.RecognitionHelper.CreatePersonGroupAsync(this.SelectedPersonGroup.DisplayName);

            this.CurrentPersonGroups.Add(new PersonGroupInformation()
            {
                DisplayName = personGroup.name,
                Id = personGroup.personGroupId,
            });

            this.SelectedPersonGroup = this.CurrentPersonGroups.LastOrDefault();

            return true;
        }

        private async Task<bool> SavePersonAsync()
        {
            var person = await Helpers.RecognitionHelper.CreatePersonAsync(this.SelectedPersonGroup.Id, this.SelectedPerson.DisplayName);

            this.CurrentPersons.Add(new PersonInformation()
            {
                DisplayName = person.name,
                Id = person.personId,                
            });

            this.SelectedPerson = this.CurrentPersons.LastOrDefault();

            return true;
        }

        public async Task<IdentifyResult> BrowseAndDetectFacesAsync()
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
                DisplayName = file.DisplayName,
                Description = "(no description)",
                FileBytes = imageBytes,
                Url = file.Path,
                ImageHeight = (int)fileProperties.Height,
                ImageWidth = (int)fileProperties.Width,

            };

            image.Url = await Helpers.StorageHelper.SaveToTemporaryFileAsync("Face", file.Name, imageBytes);
            
            this.CurrentImage = image;

            this.IsBusy = false;

            return null;

        }

        public async Task<IdentifyResult> BrowseAndIdentifyFaceAsync()
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
                DisplayName = file.DisplayName,
                Description = "(no description)",
                FileBytes = imageBytes,
                Url = file.Path,
                ImageHeight = (int)fileProperties.Height,
                ImageWidth = (int)fileProperties.Width,

            };

            image.Url = await Helpers.StorageHelper.SaveToTemporaryFileAsync("Face", file.Name, imageBytes);
 
            var detectedFace = (await Helpers.DetectionHelper.DetectFacesAsync(imageBytes)).FirstOrDefault();

            List<IdentifyResult> identifiedFaces = new List<IdentifyResult>();

            if (detectedFace != null)
            {
                identifiedFaces = await Helpers.RecognitionHelper.IdentifyFacesAsync(this.SelectedPersonGroup.Id, new List<string>() { detectedFace.faceId });

                if (identifiedFaces.Count > 0)
                {
                    this.IdentifiedPerson = this.CurrentPersons.Where(w => w.Id.Equals(identifiedFaces.First().candidates.First().personId)).FirstOrDefault();
                }
            }

            this.CurrentImage = image;

            this.IsBusy = false;

            return identifiedFaces.FirstOrDefault(); 

        }
         

        public async Task<bool> BrowseDetectAndAddFaceAsync()
        {
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
            
           var faceUrl = await Helpers.StorageHelper.SaveToTemporaryFileAsync("Face", file.Name, imageBytes);
            
            var detectedFace = (await Helpers.DetectionHelper.DetectFacesAsync(imageBytes)).FirstOrDefault();

            if (detectedFace != null)
            {
                await Helpers.RecognitionHelper.AddPersonFaceAsync(this.SelectedPersonGroup.Id, this.SelectedPerson.Id, imageBytes, file.Name, detectedFace.faceRectangle);

                this.SelectedPerson.FaceUrls.Add(faceUrl);
            }
            
            this.IsBusy = false;

            return true;

        }

        public async void RefreshPersonGroups()
        {
            this.CurrentPersonGroups.Clear();

            var personGroups = await Helpers.RecognitionHelper.GetPersonGroupsAsync();

            foreach (var personGroup in personGroups)
            {
                this.CurrentPersonGroups.Add(new PersonGroupInformation()
                {
                    Id = personGroup.personGroupId,
                    DisplayName = personGroup.name,

                });
            }

            this.SelectedPersonGroup = this.CurrentPersonGroups.FirstOrDefault();
        }

        public async void RefreshPersons()
        {
            this.CurrentPersons.Clear();

            var persons = await Helpers.RecognitionHelper.GetPersonsAsync(this.SelectedPersonGroup.Id);

            foreach (var person in persons)
            {
                this.CurrentPersons.Add(new PersonInformation()
                {
                    Id = person.personId,
                    DisplayName = person.name,
                });
            }

            this.SelectedPerson = this.CurrentPersons.FirstOrDefault();

        }

        public async void RefreshPerson()
        {
            if (this.SelectedPerson != null)
            {
                this.SelectedPerson.FaceUrls.Clear();

                var person = await Helpers.RecognitionHelper.GetPersonAsync(this.SelectedPersonGroup.Id, this.SelectedPerson.Id);

                foreach (var faceId in person.persistedFaceIds)
                {
                    var personFace = await Helpers.RecognitionHelper.GetPersonFaceAsync(this.SelectedPersonGroup.Id, this.SelectedPerson.Id, faceId);

                    this.SelectedPerson.FaceUrls.Add($"ms-appdata:///local/Face/{personFace.userData}");
                }
            }
        }
    }
}
