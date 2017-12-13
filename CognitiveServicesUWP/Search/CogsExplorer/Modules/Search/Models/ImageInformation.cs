using CogsExplorer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Search
{
    public class ImageInformation : ObservableBase
    {
        private string _id;
        public string Id
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }


        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        private DateTime _datePublished;
        public DateTime DatePublished
        {
            get { return _datePublished; }
            set { Set(ref _datePublished, value); }
        }

        private string _accentColor;
        public string AccentColor
        {
            get { return _accentColor; }
            set { Set(ref _accentColor, value); }
        }

        private string _thumbnailUrl;
        public string ThumbnailUrl
        {
            get { return _thumbnailUrl; }
            set { Set(ref _thumbnailUrl, value); }
        }

        private string _contentUrl;
        public string ContentUrl
        {
            get { return _contentUrl; }
            set { Set(ref _contentUrl, value); }
        }

        private int _width;
        public int Width
        {
            get { return _width; }
            set { Set(ref _width, value); }
        }

        private int _height;
        public int Height
        {
            get { return _height; }
            set { Set(ref _height, value); }
        }

        
    }
}
