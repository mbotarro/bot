using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.BotBuilderSamples
{
    public static class HttpGetter
    {
        public static string GetterAsync(string word)
        {
            var client = new HttpClient();

            // Create the HttpContent for the form to be posted.
            var requestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("ml", "hello"),
            });

            client.DefaultRequestHeaders.Add("app_id", "bba4308f");
            client.DefaultRequestHeaders.Add("app_key", "f5ea0a4c2eb52716e478cfd53f04e97d");
            client.BaseAddress = new Uri("https://od-api.oxforddictionaries.com:443/api/v1/entries/en/");

            Task<HttpResponseMessage> responseTask = client.GetAsync(word);
            responseTask.Wait();
            HttpResponseMessage response = responseTask.Result;
            if (response.IsSuccessStatusCode)
            {
                Task<string> t = response.Content.ReadAsStringAsync();
                t.Wait();
                JObject jObject = JObject.Parse(t.Result);
                //return (string)jObject["results"];
                return (string)jObject["results"][0]["lexicalEntries"][0]["entries"][0]["senses"][0]["definitions"][0];
            }

            return "nothing";

            //requestContent.Headers.Add("Accept", "application/json");
            //requestContent.Headers.Add("app_id", "bba4308f");
            //requestContent.Headers.Add("app_key", "f5ea0a4c2eb52716e478cfd53f04e97d");


            // Get the response.
            //Task<HttpResponseMessage> response = client.PostAsync(
            //    "https://od-api.oxforddictionaries.com:443/api/v1/entries/en/house",
            //    requestContent);

            //// Get the response content.


            //HttpContent responseContent = null;
            //response.Wait();
            //responseContent = response.Result.Content;

            //string receiveStream = await responseContent.ReadAsStringAsync();

            ////StreamReader readStream = new StreamReader(receiveStream, System.Text.Encoding.UTF8);
            ////return readStream.ReadToEnd();
            //return receiveStream;

            ////return responseContent;

            ////// Get the stream of the content.
            ////Task<Stream> stream = responseContent.ReadAsStreamAsync();
            ////StreamReader reader = null;
            ////stream.ContinueWith(x => { reader = new StreamReader(stream.Result); });
            ////stream.Wait();

            ////using (reader)
            ////{
            ////    // Write the output.
            ////    return reader.ReadToEnd();
            ////}
        }
    }
}
