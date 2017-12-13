using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Search.Suggestion
{
    
    public class SuggestionResult
    {
        public string _type { get; set; }
        public Querycontext queryContext { get; set; }
        public Suggestiongroup[] suggestionGroups { get; set; }
    }

    public class Querycontext
    {
        public string originalQuery { get; set; }
    }

    public class Suggestiongroup
    {
        public string name { get; set; }
        public Searchsuggestion[] searchSuggestions { get; set; }
    }

    public class Searchsuggestion
    {
        public string url { get; set; }
        public string displayText { get; set; }
        public string query { get; set; }
        public string searchKind { get; set; }
    }

}
