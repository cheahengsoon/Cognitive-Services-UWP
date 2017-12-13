using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Emotion.KeyPhrases
{    
    public class DocumentKeyPhrasesResult
    {
        public Document[] documents { get; set; }
        public object[] errors { get; set; }
    }

    public class Document
    {
        public string[] keyPhrases { get; set; }
        public string id { get; set; }
    }

}
