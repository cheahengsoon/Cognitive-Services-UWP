using CogsExplorer.Common;
using CogsExplorer.Modules.VideoIndexer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.VideoIndexer
{
    public class VideoUploadStatusInformation : ObservableBase
    {
        private VideoUploadStatusType _status;
        public VideoUploadStatusType Status
        {
            get { return _status; }
            set { Set(ref _status, value); }
        }

        private int _percentageComplete;
        public int PercentageComplete
        {
            get { return _percentageComplete; }
            set { Set(ref _percentageComplete, value); }
        }
    }
}
