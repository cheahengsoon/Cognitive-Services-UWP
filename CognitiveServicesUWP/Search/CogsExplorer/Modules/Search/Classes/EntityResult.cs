using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Search.Entities
{   
    public class EntityResult
    {
        public string _type { get; set; }
        public Querycontext queryContext { get; set; }
        public Entities entities { get; set; }
        public Places places { get; set; }
    }

    public class Querycontext
    {
        public string originalQuery { get; set; }
        public bool askUserForLocation { get; set; }
    }

    public class Entities
    {
        public Value[] value { get; set; }
    }

    public class Value
    {
        public Contractualrule[] contractualRules { get; set; }
        public string webSearchUrl { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public Image image { get; set; }
        public string description { get; set; }
        public Entitypresentationinfo entityPresentationInfo { get; set; }
        public string bingId { get; set; }
    }

    public class Image
    {
        public string name { get; set; }
        public string thumbnailUrl { get; set; }
        public Provider[] provider { get; set; }
        public string hostPageUrl { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Provider
    {
        public string _type { get; set; }
        public string url { get; set; }
    }

    public class Entitypresentationinfo
    {
        public string entityScenario { get; set; }
        public string[] entityTypeHints { get; set; }
        public string entityTypeDisplayHint { get; set; }
    }

    public class Contractualrule
    {
        public string _type { get; set; }
        public string targetPropertyName { get; set; }
        public bool mustBeCloseToContent { get; set; }
        public License license { get; set; }
        public string licenseNotice { get; set; }
        public string text { get; set; }
        public string url { get; set; }
    }

    public class License
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Places
    {
        public Value1[] value { get; set; }
    }

    public class Value1
    {
        public string _type { get; set; }
        public string webSearchUrl { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public Entitypresentationinfo1 entityPresentationInfo { get; set; }
        public Address address { get; set; }
        public string telephone { get; set; }
    }

    public class Entitypresentationinfo1
    {
        public string entityScenario { get; set; }
        public string[] entityTypeHints { get; set; }
    }

    public class Address
    {
        public string addressLocality { get; set; }
        public string addressRegion { get; set; }
        public string postalCode { get; set; }
        public string addressCountry { get; set; }
        public string neighborhood { get; set; }
    }

}
