using CogsExplorer.Modules.Emotion.Language;
using CogsExplorer.Modules.Emotion.Topics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace CogsExplorer.Modules.Emotion.Helpers
{
    public static class TextAnalyticsHelper
    {
        public async static Task<bool> DetectTopicsAsync(List<string> documents, List<string> stopWords, List<string> topicsToExclude)
        {
            bool successful = false;

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.TextAnalyticsSubscriptionKey);

            try 
            {
                TopicDocumentsInfo documentsInfo = new TopicDocumentsInfo()
                {
                    documents = (from document in documents select new TopicDocument() { id = Guid.NewGuid().ToString(), text = document }).ToList(),
                    stopWords = stopWords,
                    topicsToExclude = topicsToExclude,
                };
                
                var payload = new HttpStringContent(JsonConvert.SerializeObject(documentsInfo));
                payload.Headers.ContentType = new HttpMediaTypeHeaderValue("application/json");

                var response = await client.PostAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/text/analytics/v2.0/topics"), payload);

                string operationLocation = response.Headers["Operation-Location"];

                DetectOperationResult trainOperationResult = null;

                string status = "";
                HttpResponseMessage results = null;

                while (!status.Equals("Succeeded", StringComparison.OrdinalIgnoreCase))
                {
                    await Task.Delay(TimeSpan.FromMinutes(1));

                    results = await client.GetAsync(new Uri(operationLocation));

                    var operationResult = await results.Content.ReadAsStringAsync();

                    trainOperationResult = JsonConvert.DeserializeObject<DetectOperationResult>(operationResult);

                    status = trainOperationResult.status;
                }

                successful = true;

            }
            catch (Exception ex)
            {

            }

            return successful;
        }

        public async static Task<List<string>> ExtractKeyPhrasesAsync(List<string> documents, string language)
        {
            List<string> keyPhrases = new List<string>();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.TextAnalyticsSubscriptionKey);

            DocumentsInfo documentsInfo = new DocumentsInfo()
            {
                documents = new List<DocumentInfo>()
            };

            foreach (var document in documents)
            {
                documentsInfo.documents.Add(new DocumentInfo() { id = Guid.NewGuid().ToString(), language= language, text = document });
            }

            var payload = new HttpStringContent(JsonConvert.SerializeObject(documentsInfo));
            payload.Headers.ContentType = new HttpMediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/text/analytics/v2.0/keyPhrases"), payload);

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                var keyPhrasesResults = JsonConvert.DeserializeObject<KeyPhrases.DocumentKeyPhrasesResult>(results);

                foreach(var document in keyPhrasesResults.documents)
                {
                    keyPhrases.AddRange(document.keyPhrases);
                }
                
            }
            catch (Exception ex)
            {

            }

            return keyPhrases;
        }

        public async static Task<LanguageInformation> DetectLanguagesAsync(List<string> documents)
        {
            LanguageInformation detectedLanguage = null;

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.TextAnalyticsSubscriptionKey);

            DocumentsInfo documentsInfo = new DocumentsInfo()
            {
                documents = new List<DocumentInfo>()
            };

            foreach (var document in documents)
            {
                documentsInfo.documents.Add(new DocumentInfo() { id = Guid.NewGuid().ToString(), text = document });
            }

            var payload = new HttpStringContent(JsonConvert.SerializeObject(documentsInfo));
            payload.Headers.ContentType = new HttpMediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/text/analytics/v2.0/languages"), payload);

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                var languageResults = JsonConvert.DeserializeObject<DocumentLanguageResult>(results);

                if (languageResults.documents.Count() > 0)
                {
                    var primaryLanguage = languageResults.documents.FirstOrDefault().detectedLanguages.FirstOrDefault();

                    detectedLanguage = new LanguageInformation()
                    {
                        DisplayName = primaryLanguage.name,
                        Abbreviation = primaryLanguage.iso6391Name,
                    };
                }

            }
            catch (Exception ex)
            {

            }

            return detectedLanguage;
        }

        public async static Task<List<DocumentSentimentInformation>> AnalyzeSentimentAsync(List<string> documents)
        {
            List<DocumentSentimentInformation> documentSentiments = new List<DocumentSentimentInformation>(); ;

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.TextAnalyticsSubscriptionKey);

            DocumentsInfo documentsInfo = new DocumentsInfo()
            {
                documents = new List<DocumentInfo>()
            };

            foreach(var document in documents)
            {
                documentsInfo.documents.Add(new DocumentInfo() { id = Guid.NewGuid().ToString(), language="en", text = document });
            }
            
            var payload = new HttpStringContent(JsonConvert.SerializeObject(documentsInfo));
            payload.Headers.ContentType = new HttpMediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/text/analytics/v2.0/sentiment"), payload);

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                var sentimentResults = JsonConvert.DeserializeObject<DocumentSentimentResult>(results);

                documentSentiments = (from result in sentimentResults.documents
                                      select new DocumentSentimentInformation()
                                      {
                                          Id = result.id,
                                          Score = result.score,

                                      }).ToList();
            }
            catch (Exception ex)
            {

            }

            return documentSentiments;
        }
    }
}
