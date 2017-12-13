using CogsExplorer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Search
{
    public class NewsInformation : ObservableBase
    {       
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

        private string _articleUrl;
        public string ArticleUrl
        {
            get { return _articleUrl; }
            set { Set(ref _articleUrl, value); }
        }
        
        private DateTime _datePublished;
        public DateTime DatePublished
        {
            get { return _datePublished; }
            set { Set(ref _datePublished, value); }
        }

    }
}
