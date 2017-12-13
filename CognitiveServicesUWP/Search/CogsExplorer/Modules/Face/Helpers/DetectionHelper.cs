using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace CogsExplorer.Modules.Face.Helpers
{
    public static class DetectionHelper
    {
        public async static Task<List<FaceDetectionResult>> DetectFacesAsync(byte[] bytes)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.FaceApiSubscriptionKey);

            var payload = new HttpBufferContent(bytes.AsBuffer());
            payload.Headers.ContentType = new HttpMediaTypeHeaderValue("application/octet-stream");

            string attributeFeatures = "age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";

            var response = await client.PostAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/face/v1.0/detect?returnFaceId=true&returnFaceLandmarks=false&returnFaceAttributes={attributeFeatures}"), payload);

            List<FaceDetectionResult> faceAnalysisResults = new List<FaceDetectionResult>();

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                faceAnalysisResults = JsonConvert.DeserializeObject<List<FaceDetectionResult>>(results);
            }
            catch (Exception ex)
            {

            }

            return faceAnalysisResults;
        }

    }
}
 
