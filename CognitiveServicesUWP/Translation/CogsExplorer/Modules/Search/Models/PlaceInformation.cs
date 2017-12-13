using CogsExplorer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Search
{
    public class PlaceInformation : ObservableBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        private string _addressLine;
        public string AddressLine
        {
            get { return _addressLine; }
            set { Set(ref _addressLine, value); }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { Set(ref _phoneNumber, value); }
        }

        private string _searchUrl;
        public string SearchUrl
        {
            get { return _searchUrl; }
            set { Set(ref _searchUrl, value); }
        }

        private string _url;
        public string Url
        {
            get { return _url; }
            set { Set(ref _url, value); }
        }

        private string _scenario;
        public string Scenario
        {
            get { return _scenario; }
            set { Set(ref _scenario, value); }
        }

        private string _displayType;
        public string DisplayType
        {
            get { return _displayType; }
            set { Set(ref _displayType, value); }
        }
    }
}
