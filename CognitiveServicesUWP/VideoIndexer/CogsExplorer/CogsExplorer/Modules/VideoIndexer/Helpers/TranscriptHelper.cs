using CogsExplorer.Modules.VideoIndexer.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace CogsExplorer.Modules.VideoIndexer.Helpers
{
    public static class TranscriptHelper
    {
        public static async Task<List<SearchResultInformation>> SearchAsync(string videoId, string searchQuery, VideoPrivacyType privacyType)
        {
            List<SearchResultInformation> searchResults;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key",
                    Common.CoreConstants.VideoIndexerSubscriptionKey);

                var statusresult =
                    await client.GetAsync(
                        new Uri(
                            $"{CogsExplorer.Common.CoreConstants.VideoIndexerBaseUrl}/Search?privacy={privacyType}&id={videoId}&query={searchQuery}"));
                var json = await statusresult.Content.ReadAsStringAsync();

                var results = JsonConvert.DeserializeObject<Search.SearchResult>(json);

                searchResults = (from result in results.results[0].searchMatches
                                 select new SearchResultInformation()
                                 {
                                     ExactText = result.exactText,
                                     ResultType = result.type,
                                     Text = result.text,
                                     StartTime = TimeSpan.Parse(result.startTime)

                                 }).ToList();

            }

            return searchResults;
        }

        public static async Task<string> GetTextTracksAsync(string videoId)
        {
            string content = "";

            string language = "English";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key",
                    Common.CoreConstants.VideoIndexerSubscriptionKey);

                var statusresult =
                    await client.GetAsync(
                        new Uri($"{CogsExplorer.Common.CoreConstants.VideoIndexerBaseUrl}/{videoId}/VttUrl?language={language}"));
                var json = await statusresult.Content.ReadAsStringAsync();

                dynamic urlResult = JsonConvert.DeserializeObject(json);

                var ttsResult = await client.GetAsync(new Uri(urlResult));

                content = await ttsResult.Content.ReadAsStringAsync();
            }

            return content;
        }
    }
}
