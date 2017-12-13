using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Search.Webs
{ 
    public class WebResult
    {
        public string _type { get; set; }
        public Webpages webPages { get; set; }
        public Images images { get; set; }
        public object news { get; set; }
        public object relatedSearches { get; set; }
        public Videos videos { get; set; }
        public Rankingresponse rankingResponse { get; set; }
    }

    public class Webpages
    {
        public string webSearchUrl { get; set; }
        public int totalEstimatedMatches { get; set; }
        public Value[] value { get; set; }
    }

    public class Value
    {
        public string id { get; set; }
        public object deepLinks { get; set; }
        public DateTime dateLastCrawled { get; set; }
        public object about { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string displayUrl { get; set; }
        public string snippet { get; set; }
    }

    public class Images
    {
        public string id { get; set; }
        public string readLink { get; set; }
        public string webSearchUrl { get; set; }
        public bool isFamilyFriendly { get; set; }
        public Value1[] value { get; set; }
        public bool displayShoppingSourcesBadges { get; set; }
        public bool displayRecipeSourcesBadges { get; set; }
    }

    public class Value1
    {
        public string name { get; set; }
        public DateTime datePublished { get; set; }
        public string hostPageUrl { get; set; }
        public string contentSize { get; set; }
        public string hostPageDisplayUrl { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Thumbnail thumbnail { get; set; }
        public string webSearchUrl { get; set; }
        public string thumbnailUrl { get; set; }
        public string encodingFormat { get; set; }
        public string contentUrl { get; set; }
    }

    public class Thumbnail
    {
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Videos
    {
        public string id { get; set; }
        public string readLink { get; set; }
        public string webSearchUrl { get; set; }
        public bool isFamilyFriendly { get; set; }
        public Value2[] value { get; set; }
        public string scenario { get; set; }
    }

    public class Value2
    {
        public string description { get; set; }
        public DateTime datePublished { get; set; }
        public Publisher[] publisher { get; set; }
        public string contentUrl { get; set; }
        public string hostPageUrl { get; set; }
        public string encodingFormat { get; set; }
        public string hostPageDisplayUrl { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string duration { get; set; }
        public string embedHtml { get; set; }
        public bool allowHttpsEmbed { get; set; }
        public Thumbnail1 thumbnail { get; set; }
        public bool allowMobileEmbed { get; set; }
        public bool isSuperfresh { get; set; }
        public string name { get; set; }
        public string thumbnailUrl { get; set; }
        public string webSearchUrl { get; set; }
        public string motionThumbnailUrl { get; set; }
    }

    public class Thumbnail1
    {
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Publisher
    {
        public string name { get; set; }
    }

    public class Rankingresponse
    {
        public Mainline mainline { get; set; }
        public object sidebar { get; set; }
    }

    public class Mainline
    {
        public Item[] items { get; set; }
    }

    public class Item
    {
        public string answerType { get; set; }
        public Value3 value { get; set; }
        public int resultIndex { get; set; }
    }

    public class Value3
    {
        public string id { get; set; }
    }

}
