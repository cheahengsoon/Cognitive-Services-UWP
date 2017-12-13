using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace CogsExplorer.Modules.VideoIndexer.Helpers
{
    public static class BreakdownHelper
    {
        public static async Task<InsightInformation> GetVideoInsightsAsync(string videoId)
        {
            InsightInformation insights;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key",
                    Common.CoreConstants.VideoIndexerSubscriptionKey);

                var statusresult =
                    await client.GetAsync(
                        new Uri($"{CogsExplorer.Common.CoreConstants.VideoIndexerBaseUrl}/{videoId}?language=English"));
                var json = await statusresult.Content.ReadAsStringAsync();

                var breakdown = JsonConvert.DeserializeObject<VideoBreakdownResult>(json);

                insights = new InsightInformation()
                {
                    DisplayName = breakdown.name,
                    ThumbnailUrl = breakdown.summarizedInsights.thumbnailUrl,
                    Topics = new System.Collections.ObjectModel.ObservableCollection<string>(
                        breakdown.summarizedInsights.topics.Select(s => s.name)),
                    Faces = new System.Collections.ObjectModel.ObservableCollection<FaceInformation>(
                        from face in breakdown.summarizedInsights.faces
                        select new FaceInformation()
                        {
                            DisplayName = face.name,
                            Description = face.description,
                            ThumbnailUrl = face.thumbnailFullUrl,
                        }),
                };

            }

            return insights;
        }
    }
}
