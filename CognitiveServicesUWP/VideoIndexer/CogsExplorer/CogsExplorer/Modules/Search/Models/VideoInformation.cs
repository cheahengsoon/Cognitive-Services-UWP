using CogsExplorer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Search
{
    public class VideoInformation : ObservableBase
    {
        
        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { Set(ref _description, value); }
        }

        private DateTime _datePublished;
        public DateTime DatePublished
        {
            get { return _datePublished; }
            set { Set(ref _datePublished, value); }
        }

        private string _encodingFormat;
        public string EncodingFormat
        {
            get { return _encodingFormat; }
            set { Set(ref _encodingFormat, value); }
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

        private string _duration;
        public string Duration
        {
            get { return _duration; }
            set { Set(ref _duration, value); }
        }

        private string _createdBy;
        public string CreatedBy
        {
            get { return _createdBy; }
            set { Set(ref _createdBy, value); }
        }

        //public string name { get; set; }
        //public string description { get; set; }
        //public string webSearchUrl { get; set; }
        //public string thumbnailUrl { get; set; }
        //public DateTime datePublished { get; set; }
        //public Publisher[] publisher { get; set; }
        //public string contentUrl { get; set; }
        //public string hostPageUrl { get; set; }
        //public string encodingFormat { get; set; }
        //public string hostPageDisplayUrl { get; set; }
        //public int width { get; set; }
        //public int height { get; set; }
        //public bool allowHttpsEmbed { get; set; }
        //public Thumbnail thumbnail { get; set; }
        //public string videoId { get; set; }
        //public bool allowMobileEmbed { get; set; }
        //public bool isSuperfresh { get; set; }
        //public Creator creator { get; set; }
        //public string duration { get; set; }
        //public string embedHtml { get; set; }
        //public int viewCount { get; set; }
    }
}
