using CogsExplorer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CogsExplorer.Modules.VideoIndexer.Common;

namespace CogsExplorer.Modules.VideoIndexer
{
    public class VideoInformation : ObservableBase
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }

        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { Set(ref _displayName, value); }
        }

        private byte[] _file;
        public byte[] File
        {
            get { return _file; }
            set { Set(ref _file, value); }
        }
    }
}
