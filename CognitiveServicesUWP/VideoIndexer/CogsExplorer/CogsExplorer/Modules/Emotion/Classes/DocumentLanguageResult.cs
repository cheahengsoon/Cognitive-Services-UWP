using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Emotion.Language
{ 
    public class DocumentLanguageResult
    {
        public Document[] documents { get; set; }
        public object[] errors { get; set; }
    }

    public class Document
    {
        public string id { get; set; }
        public Detectedlanguage[] detectedLanguages { get; set; }
    }

    public class Detectedlanguage
    {
        public string name { get; set; }
        public string iso6391Name { get; set; }
        public float score { get; set; }
    }

}
