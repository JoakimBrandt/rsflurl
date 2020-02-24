using System;
using RestSharp;
using System.Threading.Tasks;
using System.Text.Json;



namespace Client {
    class RSClient {
        private RestClient _client;
        private RestRequest _request;
        public RSClient() {
            _client = new RestClient("");
            _request = new RestRequest("");
        }

        public async Task<String> fetchData() {
            var data = await _client.ExecuteAsync(_request);
            var dataResponse = data.Content;
            var rootData = JsonSerializer.Deserialize<String>(dataResponse);
            //var rootDataAsync = await JsonSerializer.DeserializeAsync<String>(dataResponse);
            return rootData;
        }
    }
}