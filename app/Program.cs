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
        public static void Main(string[] args)
        {
            Array array = menuChoices();

            Task task = fetch();
        }

        private static Array menuChoices()
        {
            string[] choices = new string[3];

            Console.WriteLine("How many anrop do you want to send? Type 1, 8, 64, 128...: ");
            var amountOfCalls = Console.ReadLine();
            Console.WriteLine("You entered '{0}'", amountOfCalls);

            Console.WriteLine("How much data per call do you want? Type 1, 2 or 3: ");
            var amountOfDataPerCall = Console.ReadLine();
            Console.WriteLine("You entered '{0}'", amountOfDataPerCall);

            Console.WriteLine("Which Http Client do you want to use? Type FL or RS: ");
            var typeOfClient = Console.ReadLine();
            Console.WriteLine("You entered '{0}'", typeOfClient);

            choices[0] = amountOfCalls;
            choices[1] = amountOfDataPerCall;
            choices[2] = typeOfClient;

            return choices;
        }

        private async static Task fetch() {
            Console.WriteLine("hello there");

            var response = await "https://api.postcodes.io"
                .AppendPathSegment("postcodes")
                .AppendPathSegment("IP1 3JR")
                .GetJsonAsync();
            
            Console.WriteLine("API hämtar postcoden från json objektets resultat:" + response.result.postcode);

            //-------------------------

            var client = new RestClient("https://api.postcodes.io");
            var getRequest = new RestRequest("postcodes/{postcode}");
            getRequest.AddUrlSegment("postcode", "IP1 3JR");
            var singleGeocodeResponseContainer = await client.ExecuteAsync(getRequest);
            var singleGeocodeResponse = singleGeocodeResponseContainer.Content;

            Console.WriteLine("FRÅN API:"+singleGeocodeResponse);
        }


    }
}
