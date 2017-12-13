using CogsExplorer.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CogsExplorer
{
    public class ServiceCollection : ObservableCollection<ServiceInformation>
    {
        public ServiceCollection(List<ServiceInformation> items)
        {
            foreach (var item in items)
            {
                this.Add(item);
            }
        }

        public ServiceCollection()
        {

        }

    }

    public class ServiceInformation : Common.ObservableBase
    {
        public ServiceInformation()
        {
            NavigateToModuleCommand = new RelayCommand(NavigateToModule);
        }

        private void NavigateToModule()
        {
            Services.NavigationService.Navigate(this.ModuleType);
        }

        public ICommand NavigateToModuleCommand { get; private set; }

        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { Set(ref _displayName, value); }
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

        private Type _moduleType;
        public Type ModuleType
        {
            get { return _moduleType; }
            set { Set(ref _moduleType, value); }
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { Set(ref _isEnabled, value); }
        }
    }
}
