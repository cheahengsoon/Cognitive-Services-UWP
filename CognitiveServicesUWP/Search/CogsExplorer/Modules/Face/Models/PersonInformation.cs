using CogsExplorer.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Face
{
    public class PersonInformation : ObservableBase
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

        private ObservableCollection<string> _faceUrls = new ObservableCollection<string>();
        public ObservableCollection<string> FaceUrls
        {
            get { return _faceUrls; }
            set { Set(ref _faceUrls, value); }
        }
    }
}
