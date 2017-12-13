using CogsExplorer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Face 
{
    public class FaceInformation : ObservableBase
    {
        private string _gender;
        public string Gender
        {
            get { return _gender; }
            set { Set(ref _gender, value); }
        }

        private double _age;
        public double Age
        {
            get { return _age; }
            set { Set(ref _age, value); }
        }

        private string _hairColor;
        public string HairColor
        {
            get { return _hairColor; }
            set { Set(ref _hairColor, value); }
        }

        private bool _isWearingGlasses;
        public bool IsWearingGlasses
        {
            get { return _isWearingGlasses; }
            set { Set(ref _isWearingGlasses, value); }
        }

        private bool _hasFacialHair;
        public bool HasFacialHair
        {
            get { return _hasFacialHair; }
            set { Set(ref _hasFacialHair, value); }
        }

        private bool _isSmiling;
        public bool IsSmiling
        {
            get { return _isSmiling; }
            set { Set(ref _isSmiling, value); }
        }

        private bool _isWearingMakeup;
        public bool IsWearingMakeup
        {
            get { return _isWearingMakeup; }
            set { Set(ref _isWearingMakeup, value); }
        }
    }
}
