using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Face
{
    public class IdentifyResult
    {
        public string faceId { get; set; }
        public Candidate[] candidates { get; set; }
    }

    public class Candidate
    {
        public string personId { get; set; }
        public float confidence { get; set; }
    }
    

    public class IdentifyInfo
    {
        public string personGroupId { get; set; }
        public List<string> faceIds { get; set; }
    }
}
