using CogsExplorer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.VideoIndexer
{
    public class SearchResultInformation : ObservableBase
    {
        private string _exactText;
        public string ExactText
        {
            get { return _exactText; }
            set { Set(ref _exactText, value); }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { Set(ref _text, value); }
        }

        private TimeSpan _startTime;
        public TimeSpan StartTime
        {
            get { return _startTime; }
            set { Set(ref _startTime, value); }
        }

        private string _resultType;
        public string ResultType
        {
            get { return _resultType; }
            set { Set(ref _resultType, value); }
        }
    }
}
