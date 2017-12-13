using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Emotion.Topics
{ 
    public class TopicDocumentsInfo
    {
        public List<string> stopWords { get; set; }
        public List<string> topicsToExclude { get; set; }
        public List<TopicDocument> documents { get; set; }
    }

    public class TopicDocument
    {
        public string id { get; set; }
        public string text { get; set; }
    }

}
