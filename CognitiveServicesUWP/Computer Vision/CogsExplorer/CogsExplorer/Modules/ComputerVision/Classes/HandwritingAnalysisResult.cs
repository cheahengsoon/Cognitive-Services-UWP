using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.ComputerVision.Handwriting
{
    public class HandwritingAnalysisResult
    {
        public string Status { get; set; }
        public bool Succeeded { get; set; }
        public bool Failed { get; set; }
        public bool Finished { get; set; }
        public Recognitionresult RecognitionResult { get; set; }
    }

    public class Recognitionresult
    {
        public Line[] Lines { get; set; }
    }

    public class Line
    {
        public int[] BoundingBox { get; set; }
        public string Text { get; set; }
        public Word[] Words { get; set; }
    }

    public class Word
    {
        public int[] BoundingBox { get; set; }
        public string Text { get; set; }
    }

}
