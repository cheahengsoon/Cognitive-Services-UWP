using CogsExplorer.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace CogsExplorer.Modules.Emotion
{
    public class FaceInformation : ObservableBase
    {
        private Rect _faceRectangle;
        public Rect FaceRectangle
        {
            get { return _faceRectangle; }
            set { Set(ref _faceRectangle, value); }
        }

        private ObservableCollection<EmotionScoreInformation> _scores;
        public ObservableCollection<EmotionScoreInformation> Scores
        {
            get { return _scores; }
            set { Set(ref _scores, value); }
        }
        
        public EmotionScoreInformation TopScore
        {
            get { return this.Scores.OrderByDescending(o => o.Score).FirstOrDefault(); }
            
        }
    }
}
 