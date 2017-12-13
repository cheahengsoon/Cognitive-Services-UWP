using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.VideoIndexer.Search
{
    public class SearchResult
    {
        public Result[] results { get; set; }
        public Nextpage nextPage { get; set; }
    }

    public class Nextpage
    {
        public int pageSize { get; set; }
        public int skip { get; set; }
        public bool done { get; set; }
    }

    public class Result
    {
        public string accountId { get; set; }
        public string id { get; set; }
        public object partition { get; set; }
        public object externalId { get; set; }
        public object metadata { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime createTime { get; set; }
        public string organization { get; set; }
        public string privacyMode { get; set; }
        public string userName { get; set; }
        public bool isOwned { get; set; }
        public bool isBase { get; set; }
        public string state { get; set; }
        public string processingProgress { get; set; }
        public int durationInSeconds { get; set; }
        public string thumbnailUrl { get; set; }
        public Social social { get; set; }
        public Searchmatch[] searchMatches { get; set; }
    }

    public class Social
    {
        public bool likedByUser { get; set; }
        public int likes { get; set; }
        public int views { get; set; }
    }

    public class Searchmatch
    {
        public string startTime { get; set; }
        public string type { get; set; }
        public string text { get; set; }
        public string exactText { get; set; }
    }

}
