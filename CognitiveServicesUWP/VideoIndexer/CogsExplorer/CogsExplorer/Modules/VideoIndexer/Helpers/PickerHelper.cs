using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.VideoIndexer.Helpers
{
    public static class PickerHelper
    {
        public static async Task<VideoInformation> SelectSingleVideoAsync()
        {
            var picker =
                new Windows.Storage.Pickers.FileOpenPicker
                {
                    ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail,
                    SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.VideosLibrary
                };

            picker.FileTypeFilter.Add(".mp4");

            var file = await picker.PickSingleFileAsync();

            var video = new VideoInformation()
            {
                DisplayName = file.Name,
                File = await file.AsByteArrayAsync()
            };

            return video;
        }
    }
}
