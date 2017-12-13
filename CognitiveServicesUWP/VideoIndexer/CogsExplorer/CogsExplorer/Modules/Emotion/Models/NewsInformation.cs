using CogsExplorer.Common;
using CogsExplorer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CogsExplorer.Modules.Emotion
{
    public class NewsInformation : ObservableBase
    {
        public NewsInformation()
        {
            AnalyzeSentimentCommand = new RelayCommand(async () => { await AnalyzeSentimentAsync(); });
        }
        
        public ICommand AnalyzeSentimentCommand { get; private set; }


        private string _title;
        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { Set(ref _description, value); }
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get { return _imageUrl; }
            set { Set(ref _imageUrl, value); }
        }

        private double _sentiment;
        public double Sentiment
        {
            get { return _sentiment; }
            set { Set(ref _sentiment, value); }
        }

        private string _sentimentLabel;
        public string SentimentLabel
        {
            get { return _sentimentLabel; }
            set { Set(ref _sentimentLabel, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { Set(ref _isBusy, value); }
        }

        private async Task<bool> AnalyzeSentimentAsync()
        {
            this.IsBusy = true;

           var sentiments = await Helpers.TextAnalyticsHelper.AnalyzeSentimentAsync(new List<string>() { this.Description });

            if (sentiments.Count > 0)
            {
                double sentimentScore = sentiments.FirstOrDefault().Score;

                this.Sentiment = sentimentScore;
                this.SentimentLabel = $"{sentimentScore.ToString("P1").Replace(" ","")}";
            }
            
            this.IsBusy = false;

            return true;
        }
    }
}
