using CogsExplorer.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Search
{
    public class EntityCollection : ObservableBase
    {
        private List<EntityInformation> _entities;
        public List<EntityInformation> Entities
        {
            get { return _entities; }
            set { Set(ref _entities, value); }
        }

        private List<PlaceInformation> _places;
        public List<PlaceInformation> Places
        {
            get { return _places; }
            set { Set(ref _places, value); }
        }
    }

    public class EntityInformation : ObservableBase
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

        private string _description;
        public string Description
        {
            get { return _description; }
            set { Set(ref _description, value); }
        }

        private string _searchUrl;
        public string SearchUrl
        {
            get { return _searchUrl; }
            set { Set(ref _searchUrl, value); }
        }
        
        private string _itemUrl;
        public string ItemUrl
        {
            get { return _itemUrl; }
            set { Set(ref _itemUrl, value); }
        }

        private string _thumbnailUrl;
        public string ThumbnailUrl
        {
            get { return _thumbnailUrl; }
            set { Set(ref _thumbnailUrl, value); }
        }

        private string _scenario;
        public string Scenario
        {
            get { return _scenario; }
            set { Set(ref _scenario, value); }
        }

        private string _displayType;
        public string DisplayType
        {
            get { return _displayType; }
            set { Set(ref _displayType, value); }
        }


    }
}
