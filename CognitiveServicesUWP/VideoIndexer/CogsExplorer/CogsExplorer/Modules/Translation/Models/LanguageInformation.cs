using CogsExplorer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Translation
{
    public class LanguageInformation : ObservableBase
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

        private bool _supportsSpeech;
        public bool SupportsSpeech
        {
            get { return _supportsSpeech; }
            set { Set(ref _supportsSpeech, value); }
        }
    }
}
