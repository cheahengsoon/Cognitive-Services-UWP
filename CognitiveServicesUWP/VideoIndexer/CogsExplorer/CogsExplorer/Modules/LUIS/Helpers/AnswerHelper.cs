using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace CogsExplorer.Modules.LUIS.Helpers
{
    public static class AnswerHelper
    {
        public async static Task<IntentionResult> GetIntentionAsync(string question)
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/luis/v2.0/apps/{Common.CoreConstants.LuisApplicationId}?subscription-key={Common.CoreConstants.LuisApiSubscriptionKey}&timezoneOffset=0&verbose=true&q={question}"));
            
            IntentionResult intention = new IntentionResult();

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                intention = JsonConvert.DeserializeObject<IntentionResult>(results);

            }
            catch (Exception ex)
            {

            }

            return intention;
        }

        public async static Task<string> AskAsync(string question)
        {
            string answer = "";

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.QnAApiSubscriptionKey);

            var content = new
            {
                question = question,
            };

            var payload = new HttpStringContent(JsonConvert.SerializeObject(content));
            payload.Headers.ContentType = new HttpMediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/qnamaker/v2.0/knowledgebases/47cba087-b1fa-4830-9f92-aaac6f63ee28/generateAnswer"), payload);

            AnswerResult answerResult = new AnswerResult();

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                answerResult = JsonConvert.DeserializeObject<AnswerResult>(results);

                if (answerResult.answers.FirstOrDefault() != null)
                {
                    if (answerResult.answers?.First().score > 50)
                    {
                        answer = answerResult.answers?.First()?.answer;
                    }
                    else
                    {
                        answer = "I actually don't know the answer to this...";
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return answer;
        }
    }
}
