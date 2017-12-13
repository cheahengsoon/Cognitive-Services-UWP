using CogsExplorer.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.LUIS
{
    public class ConversationInformation : ObservableCollection<ConversationItem>
    {
        public ConversationInformation(List<ConversationItem> items)
        {
            foreach (var item in items)
            {
                this.Add(item);
            }
        }

        public ConversationInformation()
        {

        }

    }

    public class ConversationItem : ObservableBase
    {
        private string _content;
        public string Content
        {
            get { return _content; }
            set { Set(ref _content, value); }
        }

        private DateTime _timestamp;
        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { Set(ref _timestamp, value); }
        }

        private bool _isBot;
        public bool IsBot
        {
            get { return _isBot; }
            set { Set(ref _isBot, value); }
        }
    }
}
