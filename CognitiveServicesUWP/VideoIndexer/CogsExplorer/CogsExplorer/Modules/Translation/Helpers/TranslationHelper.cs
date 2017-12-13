using CogsExplorer.Modules.Translation.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace CogsExplorer.Modules.Translation.Helpers
{
    public static class TranslationHelper
    {
        public async static Task<SpeechLanguageCollection> GetSpeechTranslationLanguagesAsync()
        {
            SpeechLanguageCollection languageCollection = new SpeechLanguageCollection();

            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

            var response = await client.GetAsync(new Uri($"https://dev.microsofttranslator.com/languages?api-version=1.0&scope=text,tts,speech"));

            var results = await response.Content.ReadAsStringAsync();

            dynamic jsonObject = Newtonsoft.Json.Linq.JObject.Parse(results);

            var allVoices = new List<SpeechVoiceInformation>();

            foreach (var item in jsonObject.tts)
            {
                allVoices.Add(new SpeechVoiceInformation()
                {
                    DisplayName = $"{item.Value.displayName} ({item.Value.gender}) ({item.Value.regionName})",
                    Name = item.Name,
                    Gender = item.Value.gender,
                    Region = item.Value.regionName,
                    Language = item.Value.language,
                    Label = string.Join("-", ((string)item.Name).Split('-').Take(2))
                });
            }

            foreach (var item in jsonObject.speech)
            {
                languageCollection.SpeechLanguages.Add(new SpeechLanguageInformation()
                {
                    DisplayName = item.Value.name,
                    Abbreviation = item.Name,

                });
            }

            foreach (var item in jsonObject.text)
            {
                languageCollection.TextLanguages.Add(new SpeechLanguageInformation()
                {
                    DisplayName = item.Value.name,
                    Abbreviation = item.Name,
                    Voices = allVoices.Where(w => w.Language.Equals(item.Name)).ToList()
                });
            }


            languageCollection.SpeechLanguages.Sort((x, y) => x.DisplayName.CompareTo(y.DisplayName));
            languageCollection.TextLanguages.Sort((x, y) => x.DisplayName.CompareTo(y.DisplayName));

            return languageCollection;
        }

        public async static Task<string> DecipherTextAsync(string content)
        {
            string decipheredText = "";

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.TranslatorTextSubscriptionKey);

            var result = await client.GetAsync(new Uri($"http://api.microsofttranslator.com/V3/json/TransformText?sentence={content}&language=en"));

            var transform = await result.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<DeciperResult>(transform);

            decipheredText = results.sentence;

            return decipheredText;
        }

        public async static Task<string> GetAccessTokenAsync(AccessTokenType type = AccessTokenType.Text)
        {
            string accessToken = "";

            HttpClient client = new HttpClient();

            string subscriptionKey = (type == AccessTokenType.Text) ? Common.CoreConstants.TranslatorTextSubscriptionKey : Common.CoreConstants.TranslatorSpeechSubscriptionKey;

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            var result = await client.PostAsync(new Uri($"{CogsExplorer.Common.CoreConstants.SearchServicesBaseUrl}/sts/v1.0/issueToken"), null);

            accessToken = await result.Content.ReadAsStringAsync();

            return accessToken;

        }

        public async static Task<string> GetTextTranslationAsync(string content, string languageCode)
        {
            string translatedText = "";

            HttpClient client = new HttpClient();

            //YOU CAN USE AN ACCESS TOKEN
            string accessToken = await GetAccessTokenAsync();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            //OR USE YOUR SUBSCRIPTION KEY
            //client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.TranslatorTextSubscriptionKey);

            var result = await client.GetAsync(new Uri($"http://api.microsofttranslator.com/v2/Http.svc/Translate?text={Uri.EscapeDataString(content)}&to={languageCode}"));

            var resultDocument = await result.Content.ReadAsStringAsync();

            System.Xml.XmlDocument xTranslation = new System.Xml.XmlDocument();

            xTranslation.LoadXml(resultDocument);

            translatedText = xTranslation.InnerText;

            return translatedText;
        }

        public async static Task<List<LanguageInformation>> GetTextTranslationLanguageNamesAsync()
        {
            List<LanguageInformation> translationLanguages = new List<LanguageInformation>();

            var languageAbbreviations = await Helpers.TranslationHelper.GetTextTranslationLanguagesAsync();
            var speechLanguageAbbreviations = await Helpers.TranslationHelper.GetTextTranslationLanguagesForSpeechAsync();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.TranslatorTextSubscriptionKey);

            var result = await client.GetAsync(new Uri($"https://api.microsofttranslator.com/v1/http.svc/GetLanguageNames"));

            var resultDocument = await result.Content.ReadAsStringAsync();

            var languageNames = resultDocument.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < languageAbbreviations.Count; i++)
            {
                translationLanguages.Add(new LanguageInformation() { DisplayName = languageNames[i], Abbreviation = languageAbbreviations[i], SupportsSpeech = speechLanguageAbbreviations.Contains(languageAbbreviations[i]) });
            }

            return translationLanguages.OrderBy(o => o.DisplayName).ToList();
        }

        private async static Task<List<string>> GetTextTranslationLanguagesAsync()
        {
            List<string> translationLanguages = new List<string>();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.TranslatorTextSubscriptionKey);

            var result = await client.GetAsync(new Uri($"http://api.microsofttranslator.com/v2/Http.svc/GetLanguagesForTranslate"));

            var resultDocument = await result.Content.ReadAsStringAsync();

            Windows.Data.Xml.Dom.XmlDocument languageDocument = new Windows.Data.Xml.Dom.XmlDocument();

            languageDocument.LoadXml(resultDocument);

            translationLanguages = (from node in languageDocument.DocumentElement.ChildNodes
                                    select node.InnerText).ToList();

            return translationLanguages;
        }

        private async static Task<List<string>> GetTextTranslationLanguagesForSpeechAsync()
        {
            List<string> translationLanguages = new List<string>();

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.TranslatorTextSubscriptionKey);

            var result = await client.GetAsync(new Uri($"http://api.microsofttranslator.com/v2/Http.svc/GetLanguagesForSpeak"));

            var resultDocument = await result.Content.ReadAsStringAsync();

            Windows.Data.Xml.Dom.XmlDocument languageDocument = new Windows.Data.Xml.Dom.XmlDocument();

            languageDocument.LoadXml(resultDocument);

            translationLanguages = (from node in languageDocument.DocumentElement.ChildNodes
                                    select node.InnerText).ToList();

            return translationLanguages;
        }

        public async static Task<IInputStream> GetTextTranslationForSpeechAsync(string content, GenderType gender = GenderType.Female)
        {
            HttpClient client = new HttpClient();

            var language = await DetectLanguageAsync(content);

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.TranslatorTextSubscriptionKey);

            var result = await client.GetAsync(new Uri($"https://api.microsofttranslator.com/V2/Http.svc/Speak?text={Uri.EscapeDataString(content)}&language={language}&options={gender}"));

            var fileResult = await result.Content.ReadAsInputStreamAsync();

            return fileResult;
        }

        public async static Task<string> DetectLanguageAsync(string content)
        {
            string detectedlanguage = "";

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.TranslatorTextSubscriptionKey);

            var result = await client.GetAsync(new Uri($"https://api.microsofttranslator.com/V2/Http.svc/Detect?text={Uri.EscapeDataString(content)}"));

            var resultDocument = await result.Content.ReadAsStringAsync();

            System.Xml.XmlDocument xDetection = new System.Xml.XmlDocument();

            xDetection.LoadXml(resultDocument);

            detectedlanguage = xDetection.InnerText;

            return detectedlanguage;
        }
    }
}
