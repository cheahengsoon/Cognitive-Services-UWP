using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.LUIS
{
    public class AnswerResult
    {
        public Answer[] answers { get; set; }
    }

    public class Answer
    {
        public string answer { get; set; }
        public string[] questions { get; set; }
        public float score { get; set; }
    }

}
