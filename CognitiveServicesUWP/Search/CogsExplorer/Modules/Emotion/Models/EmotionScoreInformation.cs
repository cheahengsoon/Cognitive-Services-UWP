using CogsExplorer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Emotion
{
    public class EmotionScoreInformation : ObservableBase
    {
        private string _label;
        public string Label
        {
            get { return _label; }
            set { Set(ref _label, value); }
        }

        private double _score;
        public double Score
        {
            get { return _score; }
            set { Set(ref _score, value); }
        }
    }
}
