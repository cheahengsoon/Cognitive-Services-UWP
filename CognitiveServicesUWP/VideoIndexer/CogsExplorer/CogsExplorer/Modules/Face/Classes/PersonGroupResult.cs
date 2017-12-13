using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Face
{   
    public class PersonGroupResult
    {
        public string personGroupId { get; set; }
        public string name { get; set; }
        public string userData { get; set; }
    }

    public class PersonGroupInfo
    {
        public string name { get; set; }
        public string userData { get; set; }
    }
}
