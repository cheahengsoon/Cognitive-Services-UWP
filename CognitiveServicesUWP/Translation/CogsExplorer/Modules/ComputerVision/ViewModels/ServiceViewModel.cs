using CogsExplorer.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.ComputerVision
{
    public class ServiceViewModel : ObservableBase
    {
        public ServiceViewModel()
        {

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

        public async Task<int> BrowseForImagesAsync()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            var files = await picker.PickMultipleFilesAsync();

            this.IsBusy = true;

            foreach (var file in files)
            {
                var fileProperties = await file.Properties.GetImagePropertiesAsync();
 
                byte[] imageBytes = await file.AsByteArrayAsync();

                var image = new ImageInformation()
                { 
                    DisplayName = file.DisplayName,
                    Description = "(no description)",
                    Tags = new ObservableCollection<string>(),
                    FileBytes = imageBytes,
                    Url = file.Path,   
                 
                };

                image.Url = await Helpers.StorageHelper.SaveToTemporaryFileAsync("ComputerVision", file.Name, imageBytes);

                this.CurrentImages.Add(image);
            }

            this.IsBusy = false;

            return files.Count;

        }
    }
}
