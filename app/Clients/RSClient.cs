using System;
using RestSharp;
using System.Threading.Tasks;
using System.Text.Json;


namespace Client {
    class RSClient {
        private RestClient _client;
        private RestRequest _request;
        public RSClient(String restClient, String request) {
            _client = new RestClient(restClient);
            _request = new RestRequest(request, DataFormat.Json);
        }

        public string fetchData() {
            var data = _client.Execute(_request);
            var dataResponse = data.Content;
            
            
            //var rootData = JsonSerializer.Deserialize<String>(dataResponse);
            //var rootDataAsync = await JsonSerializer.DeserializeAsync<String>(dataResponse);
            return dataResponse;
        }
    }
}