using CogsExplorer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Search
{
    public class WebInformation : ObservableBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        private string _snippet;
        public string Snippet
        {
            get { return _snippet; }
            set { Set(ref _snippet, value); }
        }

        private string _url;
        public string Url
        {
            get { return _url; }
            set { Set(ref _url, value); }
        }

        private string _displayUrl;
        public string DisplayUrl
        {
            get { return _displayUrl; }
            set { Set(ref _displayUrl, value); }
        }

        private DateTime _lastCrawledDate;
        public DateTime LastCrawledDate
        {
            get { return _lastCrawledDate; }
            set { Set(ref _lastCrawledDate, value); }
        }
    }
}
