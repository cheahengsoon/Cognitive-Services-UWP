using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;

namespace CogsExplorer.Modules.Face.Helpers
{
    public static class RecognitionHelper
    {
        public async static Task<List<IdentifyResult>> IdentifyFacesAsync(string personGroupId, List<string> faceIds)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.FaceApiSubscriptionKey);

            IdentifyInfo identifyInfo = new IdentifyInfo()
            {
                personGroupId = personGroupId,
                faceIds = faceIds,
            };

            var payload = new HttpStringContent(JsonConvert.SerializeObject(identifyInfo));
            payload.Headers.ContentType = new HttpMediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/face/v1.0/identify"), payload);

            List<IdentifyResult> identifyResults = new List<IdentifyResult>();

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                dynamic f = JsonConvert.DeserializeObject(results);

                identifyResults = JsonConvert.DeserializeObject<List<IdentifyResult>>(results);
            }
            catch (Exception ex)
            {

            }

            return identifyResults;
        }

        public async static Task<bool> TrainPersonGroupAsync(string personGroupId)
        {
            bool successful = false;

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.FaceApiSubscriptionKey);

            try
            {
                var response = await client.PostAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/face/v1.0/persongroups/{personGroupId}/train"), null);

                TrainOperationResult trainOperationResult = null;

                string status = "";
                HttpResponseMessage results = null;
                
                while (!status.Equals("Succeeded", StringComparison.OrdinalIgnoreCase))
                {
                    await Task.Delay(1000);

                    results = await client.GetAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/face/v1.0/persongroups/{personGroupId}/training"));

                    var operationResult = await results.Content.ReadAsStringAsync();

                    trainOperationResult = JsonConvert.DeserializeObject<TrainOperationResult>(operationResult);

                    status = trainOperationResult.status;
                }

                successful = true;
                
            }
            catch (Exception ex)
            {

            }

            return successful;
        }

        public async static Task<bool> DeletePersonGroupAsync(string groupId)
        {
            bool successful = false;

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.FaceApiSubscriptionKey);

            try
            {
                var response = await client.DeleteAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/face/v1.0/persongroups/{groupId}"));

                successful = response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {

            }

            return successful;
        }

        public async static Task<bool> AddPersonFaceAsync(string personGroupId, string personId, byte[] bytes, string fileName, Facerectangle faceRectangle)
        {
            bool successful = false;

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.FaceApiSubscriptionKey);

            var payload = new HttpBufferContent(bytes.AsBuffer());
            payload.Headers.ContentType = new HttpMediaTypeHeaderValue("application/octet-stream");
            try
            {
                var response = await client.PostAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/face/v1.0/persongroups/{personGroupId}/persons/{personId}/persistedFaces?userData={fileName}&targetFace={faceRectangle.left},{faceRectangle.top},{faceRectangle.width},{faceRectangle.height}"), payload);

                var results = await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {

            }

            return successful;
        }


        public async static Task<PersonResult> CreatePersonAsync(string personGroupId, string displayName)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.FaceApiSubscriptionKey);

            string personId = displayName.Replace(" ", "").ToLower();

            PersonInfo personInfo = new PersonInfo()
            {
                name = displayName,
                userData = "",
            };

            var payload = new HttpStringContent(JsonConvert.SerializeObject(personInfo));
            payload.Headers.ContentType = new HttpMediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/face/v1.0/persongroups/{personGroupId}/persons"), payload);

            PersonResult personResult = null;

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                var personCreationResult = JsonConvert.DeserializeObject<PersonCreationResult>(results);

                personResult = new PersonResult()
                {
                    personId = personCreationResult.personId,
                    name = displayName,
                    userData = "",
                };

            }
            catch (Exception ex)
            {

            }

            return personResult;
        }

        public async static Task<PersonGroupResult> CreatePersonGroupAsync(string displayName)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.FaceApiSubscriptionKey);

            string groupId = displayName.Replace(" ", "").ToLower();

            PersonGroupInfo groupInfo = new PersonGroupInfo()
            {
                name = displayName,
                userData = "",
            };

            var payload = new HttpStringContent(JsonConvert.SerializeObject(groupInfo));
            payload.Headers.ContentType = new HttpMediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/face/v1.0/persongroups/{groupId}"), payload);

            PersonGroupResult personGroupResult = null;

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                personGroupResult = new PersonGroupResult()
                {
                    personGroupId = groupId,
                    name = displayName,
                    userData = "",
                };

            }
            catch (Exception ex)
            {

            }

            return personGroupResult;
        }

        public async static Task<PersonResult> GetPersonAsync(string personGroupId, string personId)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.FaceApiSubscriptionKey);

            var response = await client.GetAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/face/v1.0/persongroups/{personGroupId}/persons/{personId}"));

            PersonResult personResult = null;

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                personResult = JsonConvert.DeserializeObject<PersonResult>(results);

            }
            catch (Exception ex)
            {

            }

            return personResult;
        }

        public async static Task<PersonResult> GetPersonFaceAsync(string personGroupId, string personId, string faceId)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.FaceApiSubscriptionKey);

            var response = await client.GetAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/face/v1.0/persongroups/{personGroupId}/persons/{personId}/persistedFaces/{faceId}"));

            PersonResult personResult = null;

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                personResult = JsonConvert.DeserializeObject<PersonResult>(results);

            }
            catch (Exception ex)
            {

            }

            return personResult;
        }


        public async static Task<List<PersonResult>> GetPersonsAsync(string personGroupId)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.FaceApiSubscriptionKey);

            var response = await client.GetAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/face/v1.0/persongroups/{personGroupId}/persons"));

            List<PersonResult> personResults = new List<PersonResult>();

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                personResults = JsonConvert.DeserializeObject<List<PersonResult>>(results);

            }
            catch (Exception ex)
            {

            }

            return personResults;
        }

        public async static Task<List<PersonGroupResult>> GetPersonGroupsAsync()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.FaceApiSubscriptionKey);

            var response = await client.GetAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/face/v1.0/persongroups"));

            List<PersonGroupResult> personGroupResults = new List<PersonGroupResult>();

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                personGroupResults = JsonConvert.DeserializeObject<List<PersonGroupResult>>(results);

            }
            catch (Exception ex)
            {

            }

            return personGroupResults;
        }



        public async static Task<List<FaceListResult>> GetFaceListsAsync()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.FaceApiSubscriptionKey);

            var response = await client.GetAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/face/v1.0/facelists"));

            List<FaceListResult> faceListResults = new List<FaceListResult>();

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                faceListResults = JsonConvert.DeserializeObject<List<FaceListResult>>(results);

            }
            catch (Exception ex)
            {

            }

            return faceListResults;
        }

        public async static Task<bool> DeleteFaceListAsync(string listId)
        {
            bool successful = false;

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.FaceApiSubscriptionKey);

            try
            {
                var response = await client.DeleteAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/face/v1.0/facelists/{listId}"));

                successful = response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {

            }

            return successful;
        }

        public async static Task<FaceListResult> CreateFaceListAsync(string displayName)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Common.CoreConstants.FaceApiSubscriptionKey);

            string listId = displayName.Replace(" ", "").ToLower();

            FaceListInfo listInfo = new FaceListInfo()
            {
                name = displayName,
                userData = "",
            };

            var payload = new HttpStringContent(JsonConvert.SerializeObject(listInfo));
            payload.Headers.ContentType = new HttpMediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(new Uri($"{CogsExplorer.Common.CoreConstants.CognitiveServicesBaseUrl}/face/v1.0/facelists/{listId}"), payload);

            FaceListResult faceListResult = null;

            try
            {
                var results = await response.Content.ReadAsStringAsync();

                faceListResult = new FaceListResult()
                {
                    faceListId = listId,
                    name = displayName,
                    userData = "",
                };

            }
            catch (Exception ex)
            {

            }

            return faceListResult;
        }
    }
}


 
