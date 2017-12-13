using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Face
{

    public class PersonCreationResult
    {
        public string personId { get; set; }        
    }

    public class PersonResult
    {
        public string personId { get; set; }
        public string name { get; set; }
        public string userData { get; set; }
        public List<string> persistedFaceIds { get; set; }
    }

    public class PersonInfo
    {
        public string name { get; set; }
        public string userData { get; set; }
    }
}
