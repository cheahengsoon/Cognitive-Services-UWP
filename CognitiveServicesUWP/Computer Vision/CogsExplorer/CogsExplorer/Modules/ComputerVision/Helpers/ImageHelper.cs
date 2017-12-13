using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace CogsExplorer.Modules.ComputerVision.Helpers
{
    public static class ImageHelper
    {
        public async static Task<Handwriting.Operations.HandwritingOperationResult> GetHandwritingAnalysisAsync(Guid id, byte[] bytes)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.ComputerVisionApiSubscriptionKey);

            var payload = new HttpBufferContent(bytes.AsBuffer());
            payload.Headers.ContentType = new HttpMediaTypeHeaderValue("application/octet-stream");

            var response = await client.PostAsync(new Uri($"https://westus.api.cognitive.microsoft.com/vision/v1.0/recognizeText"), payload);

            Handwriting.Operations.HandwritingOperationResult result = null;

            try
            {
                string operationLocation = response.Headers["Operation-Location"];

                string status = "";
                HttpResponseMessage results = null;
                Handwriting.Operations.HandwritingOperationResult handwritingAnalysisResult = null;

                while (status != "Succeeded")
                {
                    await Task.Delay(1000);

                    results = await client.GetAsync(new Uri(operationLocation));

                    var analysisResults = await results.Content.ReadAsStringAsync();

                    handwritingAnalysisResult = JsonConvert.DeserializeObject<Handwriting.Operations.HandwritingOperationResult>(analysisResults);

                    status = handwritingAnalysisResult.status;
                }

                result = handwritingAnalysisResult;

            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public async static Task<Ocr.OcrAnalysisResult> GetOcrAnalysisAsync(Guid id, byte[] bytes)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.ComputerVisionApiSubscriptionKey);

            var payload = new HttpBufferContent(bytes.AsBuffer());
            payload.Headers.ContentType = new HttpMediaTypeHeaderValue("application/octet-stream");

            var results = await client.PostAsync(new Uri($"https://westus.api.cognitive.microsoft.com/vision/v1.0/ocr"), payload);

            Ocr.OcrAnalysisResult result = null;

            try
            {
                var analysisResults = await results.Content.ReadAsStringAsync();

                var ocrAnalysisResult = JsonConvert.DeserializeObject<Ocr.OcrAnalysisResult>(analysisResults);

                result = ocrAnalysisResult;

            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public async static Task<Image.ImageAnalysisResult> GetImageAnalysisAsync(Guid id, byte[] bytes)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.ComputerVisionApiSubscriptionKey);

            var payload = new HttpBufferContent(bytes.AsBuffer());
            payload.Headers.ContentType = new HttpMediaTypeHeaderValue("application/octet-stream");

            string analysisFeatures = "Color,ImageType,Tags,Categories,Description,Adult";

            var results = await client.PostAsync(new Uri($"https://westus.api.cognitive.microsoft.com/vision/v1.0/analyze?visualFeatures={analysisFeatures}"), payload);

            Image.ImageAnalysisResult result = null;

            try
            {
                var analysisResults = await results.Content.ReadAsStringAsync();

                var imageAnalysisResult = JsonConvert.DeserializeObject<Image.ImageAnalysisInfo>(analysisResults);

                result = new Image.ImageAnalysisResult()
                {
                    id = id.ToString(),
                    details = imageAnalysisResult,
                    caption = imageAnalysisResult.description.captions.FirstOrDefault().text,
                    tags = imageAnalysisResult.description.tags.ToList(),
                };

            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
