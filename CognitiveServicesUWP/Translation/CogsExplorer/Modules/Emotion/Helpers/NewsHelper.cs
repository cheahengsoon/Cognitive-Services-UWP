using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace CogsExplorer.Modules.Emotion.Helpers
{
    public static class NewsHelper
    {
        public async static Task<List<NewsInformation>> GetNewsAsync(string query, int maximumResults)
        {
            List<NewsInformation> searchResults = new List<NewsInformation>();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.SearchSubscriptionKey);

            var response = await client.GetAsync(new Uri($"{CogsExplorer.Common.CoreConstants.SearchServicesBaseUrl}/bing/v5.0/news/search?q={query}&count={maximumResults}&offset=0&mkt=en-us&safeSearch=Moderate"));

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                var newsResults = JsonConvert.DeserializeObject<News.NewsResult>(results);

                searchResults = (from result in newsResults.value
                                 select new NewsInformation()
                                 {
                                     Title = result.name,
                                     Description = result.description,
                                     ImageUrl = result.image?.thumbnail?.contentUrl

                                 }).ToList();
            }
            catch (Exception ex)
            {

            }

            return searchResults;
        }
    }
}
