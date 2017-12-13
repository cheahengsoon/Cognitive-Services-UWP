using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace CogsExplorer.Modules.Emotion.Helpers
{
    public static class EmotionHelper
    { 
        public async static Task<List<Image.EmotionAnalysisResult>> GetEmotionAnalysisAsync(Guid id, byte[] bytes)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.EmotionApiSubscriptionKey);

            var payload = new HttpBufferContent(bytes.AsBuffer());
            payload.Headers.ContentType = new HttpMediaTypeHeaderValue("application/octet-stream");
            
            var results = await client.PostAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/emotion/v1.0/recognize"), payload);

            List<Image.EmotionAnalysisResult> emotionAnalysisResults = new List<Image.EmotionAnalysisResult>();

            try
            {
                var analysisResults = await results.Content.ReadAsStringAsync();

                emotionAnalysisResults = JsonConvert.DeserializeObject<List<Image.EmotionAnalysisResult>>(analysisResults);
                
            }
            catch (Exception ex)
            {

            }

            return emotionAnalysisResults;
        }

      
    }
}
