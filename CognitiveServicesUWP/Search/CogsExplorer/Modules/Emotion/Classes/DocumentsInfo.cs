using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Emotion
{
    public class DocumentsInfo
    {    
        public List<DocumentInfo> documents { get; set; }
    }

    public class DocumentInfo
    {
        public string language { get; set; }
        public string id { get; set; }
        public string text { get; set; }
    }
}
