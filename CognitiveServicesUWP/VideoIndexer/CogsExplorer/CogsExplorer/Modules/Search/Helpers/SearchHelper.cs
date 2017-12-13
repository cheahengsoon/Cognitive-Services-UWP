using CogsExplorer.Modules.Search.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace CogsExplorer.Modules.Search.Helpers
{
    public static class SearchHelper
    {
        public async static Task<EntityCollection> SearchEntitiesAsync(string query, BasicGeoposition? location)
        {
            EntityCollection entities = new EntityCollection();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.EntitiesApiSubscriptionKey);

            if (location != null) client.DefaultRequestHeaders.Add("X-Search-Location", $"lat:{((BasicGeoposition)location).Latitude.ToString(CultureInfo.InvariantCulture)};long:{((BasicGeoposition)location).Longitude.ToString(CultureInfo.InvariantCulture)};re:100");

            var response = await client.GetAsync(new Uri($"{CogsExplorer.Common.CoreConstants.SearchServicesBaseUrl}/bing/v7.0/Entities?q={query}&mkt={CultureInfo.CurrentCulture.Name}"));

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                var entityResults = JsonConvert.DeserializeObject<Entities.EntityResult>(results);
                var entityValues = (entityResults.entities == null) ? new List<Entities.Value>() : entityResults.entities.value.ToList();
                var placeValues = (entityResults.places == null) ? new List<Entities.Value1>() : entityResults.places.value.ToList();

                entities.Entities = (from result in entityValues
                                     select new EntityInformation()
                                     {
                                         Description = result.description,
                                         DisplayType = result.entityPresentationInfo.entityTypeDisplayHint,
                                         Id = result.bingId,
                                         Name = result.name,
                                         Scenario = result.entityPresentationInfo.entityScenario,
                                         SearchUrl = result.webSearchUrl,
                                         ItemUrl = result.image?.hostPageUrl,
                                         ThumbnailUrl = result.image?.thumbnailUrl,

                                     }).ToList();

                entities.Places = (from result in placeValues
                                   select new PlaceInformation()
                                   {
                                       AddressLine = $"{result.address.addressLocality}, {result.address.addressRegion} {result.address.addressCountry} {result.address.postalCode}",
                                       PhoneNumber = result.telephone,
                                       DisplayType = result.entityPresentationInfo.entityTypeHints.LastOrDefault() + "",
                                       Scenario = result.entityPresentationInfo.entityScenario,
                                       Name = result.name,
                                       Url = result.url,
                                       SearchUrl = result.webSearchUrl,

                                   }).ToList();

            }
            catch (Exception ex)
            {

            }

            return (entities == null) ? new EntityCollection() : entities;
        }

        public async static Task<List<string>> GetSuggestionsAsync(string query, BasicGeoposition? location)
        {
            List<string> suggestions = new List<string>();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.AutosuggestApiSubscriptionKey);

            if (location != null) client.DefaultRequestHeaders.Add("X-Search-Location", $"lat:{((BasicGeoposition)location).Latitude.ToString(CultureInfo.InvariantCulture)};long:{((BasicGeoposition)location).Longitude.ToString(CultureInfo.InvariantCulture)};re:100");

            var response = await client.GetAsync(new Uri($"{CogsExplorer.Common.CoreConstants.SearchServicesBaseUrl}/bing/v7.0/Suggestions?q={query}&mkt={CultureInfo.CurrentCulture.Name}"));

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                var suggestionResults = JsonConvert.DeserializeObject<Suggestion.SuggestionResult>(results);
                var suggestionGroups = suggestionResults.suggestionGroups.Select(s => s.searchSuggestions).FirstOrDefault();

                suggestions = suggestionGroups.Select(s => s.displayText).Distinct().ToList();

            }
            catch (Exception ex)
            {

            }

            return suggestions;
        }

        public async static Task<List<ImageInformation>> SearchImagesAsync(string query)
        {
            List<ImageInformation> imageResults = new List<ImageInformation>();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.SearchSubscriptionKey);

            var response = await client.GetAsync(new Uri($"{CogsExplorer.Common.CoreConstants.SearchServicesBaseUrl}/bing/v5.0/images/search?q={query}&mkt={CultureInfo.CurrentCulture.Name}"));

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                var searchResults = JsonConvert.DeserializeObject<Images.ImageSearchResult>(results);

                imageResults = (from result in searchResults.value
                          select new ImageInformation()
                          {
                              AccentColor = result.accentColor,
                              ContentUrl = result.contentUrl,
                              DatePublished = result.datePublished,
                              Height = result.height,
                              Width = result.width,
                              Id = result.imageId,
                              Name = result.name,
                              ThumbnailUrl = result.thumbnailUrl,


                          }).ToList();
            }
            catch (Exception ex)
            {

            }

            return imageResults;
        }

        public async static Task<List<VideoInformation>> SearchVideosAsync(string query)
        {
            List<VideoInformation> videoResults = new List<VideoInformation>();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.SearchSubscriptionKey);

            var response = await client.GetAsync(new Uri($"{CogsExplorer.Common.CoreConstants.SearchServicesBaseUrl}/bing/v5.0/videos/search?q={query}&mkt={CultureInfo.CurrentCulture.Name}"));

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                var searchResults = JsonConvert.DeserializeObject<Videos.VideoSearchResult>(results);

                videoResults = (from result in searchResults.value
                          select new VideoInformation()
                          {
                              CreatedBy = result.creator?.name,
                              Description = result.description,
                              Duration = result.duration,
                              ContentUrl = result.contentUrl,
                              DatePublished = result.datePublished,
                              Height = result.height,
                              Width = result.width,

                              Name = result.name,
                              ThumbnailUrl = result.thumbnailUrl,
                              EncodingFormat = result.encodingFormat,

                          }).ToList();
            }
            catch (Exception ex)
            {

            }

            return videoResults;
        }

        public async static Task<List<NewsInformation>> SearchNewsAsync(string query)
        {
            List<NewsInformation> newsResults = new List<NewsInformation>();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.SearchSubscriptionKey);

            var response = await client.GetAsync(new Uri($"{CogsExplorer.Common.CoreConstants.SearchServicesBaseUrl}/bing/v5.0/news/search?q={query}&offset=0&mkt={CultureInfo.CurrentCulture.Name}&safeSearch=Moderate"));

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                var searchResults = JsonConvert.DeserializeObject<News.NewsResult>(results);

                newsResults = (from result in searchResults.value
                               select new NewsInformation()
                               {
                                   Title = result.name,
                                   Description = result.description,
                                   ImageUrl = result.image?.thumbnail?.contentUrl,
                                   ArticleUrl = result.url,
                                   DatePublished = result.datePublished,

                               }).ToList();
            }
            catch (Exception ex)
            {

            }

            return newsResults;
        }


        public async static Task<List<WebInformation>> SearchWebAsync(string query)
        {
            List<WebInformation> webResults = new List<WebInformation>();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.SearchSubscriptionKey);

            var response = await client.GetAsync(new Uri($"{CogsExplorer.Common.CoreConstants.SearchServicesBaseUrl}/bing/v5.0/search?q={query}&mkt={CultureInfo.CurrentCulture.Name}"));

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                var searchResults = JsonConvert.DeserializeObject<Webs.WebResult>(results);

                webResults = (from result in searchResults.webPages.value
                              select new WebInformation()
                              {
                                  Name = result.name,
                                  Snippet = result.snippet,
                                  LastCrawledDate = result.dateLastCrawled,
                                  DisplayUrl = result.displayUrl,
                                  Url = result.url,

                              }).ToList();

            }
            catch (Exception ex)
            {

            }

            return webResults;
        }
    }
}
