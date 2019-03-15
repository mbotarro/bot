using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Net.Http;
using System.IO;

namespace Microsoft.BotBuilderSamples
{
    public static class HttpGetter
    {
        public static string GetterAsync()
        {
            var client = new HttpClient();

            // Create the HttpContent for the form to be posted.
            var requestContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("ml", "hello"),
            });

            // Get the response.
            Task<HttpResponseMessage> response = client.PostAsync(
                "api.datamuse.com/words",
                requestContent);

            // Get the response content.


            HttpContent responseContent = null;
            response.ContinueWith(x => { responseContent = x.Result.Content; });
            response.Wait();

            // Get the stream of the content.
            Task<Stream> stream = responseContent.ReadAsStreamAsync();
            StreamReader reader = null;
            stream.ContinueWith(x => { reader = new StreamReader(stream.Result); });
            stream.Wait();

            using (reader)
            {
                // Write the output.
                return reader.ReadToEnd();
            }
        }
    }
}
