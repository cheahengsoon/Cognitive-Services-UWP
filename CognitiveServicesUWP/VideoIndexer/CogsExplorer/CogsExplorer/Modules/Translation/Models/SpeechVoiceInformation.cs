using CogsExplorer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Translation
{
    public class SpeechVoiceInformation : ObservableBase
    {
        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { Set(ref _displayName, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        private string _label;
        public string Label
        {
            get { return _label; }
            set { Set(ref _label, value); }
        }

        private string _gender;
        public string Gender
        {
            get { return _gender; }
            set { Set(ref _gender, value); }
        }

        private string _language;
        public string Language
        {
            get { return _language; }
            set { Set(ref _language, value); }
        }

        private string _region;
        public string Region
        {
            get { return _region; }
            set { Set(ref _region, value); }
        }

    }
}
