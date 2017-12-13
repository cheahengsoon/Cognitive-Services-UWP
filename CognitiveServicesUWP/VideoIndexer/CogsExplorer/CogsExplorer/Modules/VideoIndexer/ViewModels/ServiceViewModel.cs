using CogsExplorer.Common;
using CogsExplorer.Helpers;
using CogsExplorer.Modules.VideoIndexer.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace CogsExplorer.Modules.VideoIndexer
{
    public class ServiceViewModel : ObservableBase
    {
        public ServiceViewModel()
        {
            UploadVideoCommand = new RelayCommand(async () => { await StartUploadVideoAsync(); });
            SearchCommand = new RelayCommand(async () => { await StartSearchAsync(); });

        }

        public async Task StartUploadVideoAsync()
        {
            this.CurrentVideo = await Helpers.PickerHelper.SelectSingleVideoAsync();

            this.IsBusy = true;

            var videoId =
                await Helpers.UploadHelper.UploadVideoAsync(this.CurrentVideo.DisplayName, $"{this.CurrentVideo.DisplayName} upload from Cogs Explorer for Windows.", Common.VideoPrivacyType.Private,
                    this.CurrentVideo.File);

            this.CurrentVideo.Id = videoId;

            await PollVideoUploadStatusAsync(videoId);

            this.CurrentVideo = new VideoInformation() { Id = videoId };

            var insights = await Helpers.BreakdownHelper.GetVideoInsightsAsync(videoId);

            this.CurrentInsights.DisplayName = insights.DisplayName;
            this.CurrentInsights.ThumbnailUrl = insights.ThumbnailUrl;
            this.CurrentInsights.Faces.Clear();

            foreach (var face in insights.Faces) this.CurrentInsights.Faces.Add(face);

            this.CurrentInsights.Topics.Clear();

            foreach (var topic in insights.Topics) this.CurrentInsights.Topics.Add(topic);

            var transcript = await Helpers.TranscriptHelper.GetTextTracksAsync(videoId);

            this.CurrentTtsContent = transcript;

            this.IsBusy = false;

            return;
        }

        private async Task PollVideoUploadStatusAsync(string videoId)
        {
            this.IsBusy = false;
            this.IsProcessing = true;
            this.CurrentUploadStatus.PercentageComplete = 10;

            while (true)
            {
                await Task.Delay(10000);

                var status = await Helpers.UploadHelper.CheckUploadStatusAsync(videoId);

                this.CurrentUploadStatus.Status =
                    (VideoUploadStatusType)Enum.Parse(typeof(VideoUploadStatusType), status.state, true);

                int.TryParse(status.progress.Replace("%", "").Trim(), out var percentageComplete);

                this.CurrentUploadStatus.PercentageComplete = percentageComplete;

                if (this.CurrentUploadStatus.Status.Equals(VideoUploadStatusType.Processed) || this.CurrentUploadStatus.Status.Equals(VideoUploadStatusType.Failed))
                {
                    break;
                }
            }

            this.IsProcessing = false;

            return;

        }

        public async Task StartSearchAsync()
        {
            var searchResults =
                await Helpers.TranscriptHelper.SearchAsync(this.CurrentVideo.Id, this.SearchQuery, VideoPrivacyType.Private);

            this.SearchResults.Clear();

            foreach (var result in searchResults) this.SearchResults.Add(result);
        }

        public ICommand UploadVideoCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }

        private VideoInformation _currentVideo;
        public VideoInformation CurrentVideo
        {
            get { return _currentVideo; }
            set { Set(ref _currentVideo, value); }
        }

        private InsightInformation _currentInsights = new InsightInformation();
        public InsightInformation CurrentInsights
        {
            get { return _currentInsights; }
            set { Set(ref _currentInsights, value); }
        }

        private ObservableCollection<SearchResultInformation> _searchResults = new ObservableCollection<SearchResultInformation>();
        public ObservableCollection<SearchResultInformation> SearchResults
        {
            get { return _searchResults; }
            set { Set(ref _searchResults, value); }
        }

        private string _searchQuery;
        public string SearchQuery
        {
            get { return _searchQuery; }
            set { Set(ref _searchQuery, value); }
        }

        private VideoUploadStatusInformation _currentUploadStatus = new VideoUploadStatusInformation();
        public VideoUploadStatusInformation CurrentUploadStatus
        {
            get { return _currentUploadStatus; }
            set { Set(ref _currentUploadStatus, value); }
        }

        private string _currentTtsContent;
        public string CurrentTtsContent
        {
            get { return _currentTtsContent; }
            set { Set(ref _currentTtsContent, value); }
        }


        private bool _isProcessing;
        public bool IsProcessing
        {
            get { return _isProcessing; }
            set { Set(ref _isProcessing, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(ref _isBusy, value); }
        }

    }
}
