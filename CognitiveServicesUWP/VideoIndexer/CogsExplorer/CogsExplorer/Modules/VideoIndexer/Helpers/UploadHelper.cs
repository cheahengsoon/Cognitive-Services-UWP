using CogsExplorer.Modules.VideoIndexer.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace CogsExplorer.Modules.VideoIndexer.Helpers
{
    public static class UploadHelper
    {
        public static async Task<VideoUploadStatusInfo> CheckUploadStatusAsync(string videoId)
        {
            VideoUploadStatusInfo status;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key",
                    Common.CoreConstants.VideoIndexerSubscriptionKey);

                var statusresult =
                    await client.GetAsync(
                        new Uri($"{CogsExplorer.Common.CoreConstants.VideoIndexerBaseUrl}/{videoId}/State"));
                var json = await statusresult.Content.ReadAsStringAsync();

                status = JsonConvert.DeserializeObject<VideoUploadStatusInfo>(json);

            }

            return status;
        }

        public static async Task<string> UploadVideoAsync(string name, string description, VideoPrivacyType privacyType,
            byte[] bytes)
        {
            string videoId = null;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key",
                    Common.CoreConstants.VideoIndexerSubscriptionKey);

                var uri = new Uri(
                    $"{CogsExplorer.Common.CoreConstants.VideoIndexerBaseUrl}?name={name}&description={description}&privacy={privacyType}");

                try
                {
                    var payload = new HttpMultipartContent { new HttpBufferContent(bytes.AsBuffer()) };

                    var response = await client.PostAsync(uri, payload);

                    var result = await response.Content.ReadAsStringAsync();

                    dynamic videoIdResult = JsonConvert.DeserializeObject(result);

                    videoId = videoIdResult.ToString();

                }
                catch (Exception ex)
                {

                }
            }

            return videoId;
        }
    }
}
