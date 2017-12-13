using CogsExplorer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Emotion
{
    public class DocumentSentimentInformation : ObservableBase
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }

        private double _score;
        public double Score
        {
            get { return _score; }
            set { Set(ref _score, value); }
        }
    }
}
