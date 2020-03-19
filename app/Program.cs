using System;
using System.Threading.Tasks;
using Client;

using Flurl;
using Flurl.Http;
using RestSharp;



namespace app
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World! ");
            
            var response = await "https://api.postcodes.io"
                .AppendPathSegment("postcodes")
                .AppendPathSegment("IP1 3JR")
                .GetJsonAsync();
            
            Console.WriteLine("API hämtar postcoden från json objektets resultat:"+response.result.postcode);

            //-------------------------

            // instantiate the RestClient with the base API url
            var client = new RestClient("https://api.postcodes.io");
            // specify the resource, e.g. https://api.postcodes.io/postcodes/IP1 3JR
            var getRequest = new RestRequest("postcodes/{postcode}");
            getRequest.AddUrlSegment("postcode", "IP1 3JR");
            // send the GET request and return an object which contains the API's JSON response
            var singleGeocodeResponseContainer = await client.ExecuteAsync(getRequest);
            // get the API's JSON response
            var singleGeocodeResponse = singleGeocodeResponseContainer.Content;

            Console.WriteLine("FRÅN API:"+singleGeocodeResponse);



        }
    }
}
