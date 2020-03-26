using System;
using RestSharp;
using System.Threading.Tasks;
using System.Text.Json;


namespace Client {
    class RSClient {
        //Client connection
        private RestClient _client;
        //Request to get data
        private RestRequest _request;
        public RSClient(String restClient, String request) {
            _client = new RestClient(restClient);
            _request = new RestRequest(request, DataFormat.Json);
        }

        public string fetchData() {
            //_request.AddQueryParameter("api_key", "DEMO_KEY");
            var RSresponse = _client.Execute(_request);
            var dataResponse = RSresponse.Content;
            return dataResponse;
        }

        private int deserializeToTree() {
            //var rootData = JsonSerializer.Deserialize<String>(dataResponse);
            //var rootDataAsync = await JsonSerializer.DeserializeAsync<String>(dataResponse);
            return 1;
        }
    }
}