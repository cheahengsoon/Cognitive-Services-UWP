using CogsExplorer.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.VideoIndexer
{
    public class InsightInformation : ObservableBase
    {
        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { Set(ref _displayName, value); }
        }

        private string _thumbnailUrl;
        public string ThumbnailUrl
        {
            get { return _thumbnailUrl; }
            set { Set(ref _thumbnailUrl, value); }
        }

        private ObservableCollection<string> _topics = new ObservableCollection<string>();
        public ObservableCollection<string> Topics
        {
            get { return _topics; }
            set { Set(ref _topics, value); }
        }

        private ObservableCollection<FaceInformation> _faces = new ObservableCollection<FaceInformation>();
        public ObservableCollection<FaceInformation> Faces
        {
            get { return _faces; }
            set { Set(ref _faces, value); }
        }
    }

    public class FaceInformation : ObservableBase
    {
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

        private string _thumbnailUrl;
        public string ThumbnailUrl
        {
            get { return _thumbnailUrl; }
            set { Set(ref _thumbnailUrl, value); }
        }
    }
}
