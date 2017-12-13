using CogsExplorer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Translation
{
    public class SpeechLanguageCollection : ObservableBase
    {
        private List<SpeechLanguageInformation> _speechLanguages = new List<SpeechLanguageInformation>();
        public List<SpeechLanguageInformation> SpeechLanguages
        {
            get { return _speechLanguages; }
            set { Set(ref _speechLanguages, value); }
        }

        private List<SpeechLanguageInformation> _textLanguages = new List<SpeechLanguageInformation>();
        public List<SpeechLanguageInformation> TextLanguages
        {
            get { return _textLanguages; }
            set { Set(ref _textLanguages, value); }
        }
        
    }

    public class SpeechLanguageInformation : ObservableBase
    {
        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { Set(ref _displayName, value); }
        }

        private string _abbreviation;
        public string Abbreviation
        {
            get { return _abbreviation; }
            set { Set(ref _abbreviation, value); }
        }

        private List<SpeechVoiceInformation> _voices = new List<SpeechVoiceInformation>();
        public List<SpeechVoiceInformation> Voices
        {
            get { return _voices; }
            set { Set(ref _voices, value); }
        }

    }
}
