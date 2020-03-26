using Flurl;
using Flurl.Http;
using System.Threading.Tasks;
using poco;
using System.Text.Json;

using System;
using System.Collections.Generic;


namespace Client {
    class FlurlClient {
        private string _targetUrl;
        private string _targetResource;
        private List<string> _targetResourceList;
        public FlurlClient(string url, string targetResource) {
            _targetUrl = url;
            _targetResource = targetResource;
            _targetResourceList = reformatResource(targetResource);

        }

//planetary/apod
        private List<string> reformatResource(string targetResource) {
            var resourceList = new List<string>();
            String[] separator = {"/"};
            Int32 count = 2;
            String[] strList = targetResource.Split(separator, count, 
                StringSplitOptions.RemoveEmptyEntries);

            foreach (var str in strList)
            {
                Console.WriteLine(str);
                resourceList.Add(str);
            }

            Console.WriteLine("listelement 1:"+resourceList[1]);
            return resourceList;

        }

        public async Task<string> getJsonAsync() {


            var JsonResponse = await _targetUrl.AppendPathSegments("planetary", "apod").SetQueryParam("api_key", "DEMO_KEY").GetJsonAsync();
                        Console.WriteLine("HELLLLLLLO?????");



                        //var rootData = JsonSerializer.Deserialize<String>(dataResponse);
            //var rootData = JsonSerializer.Deserialize<string>(JsonResponse);
            //Console.WriteLine("root data:"+rootData);
            //varför skrivs inte json responsen ut?
            Console.WriteLine("jsonresponse:"+JsonResponse);
            return JsonResponse;
        }
    }
}