using System;
using System.Threading.Tasks;
using Client;

using Flurl;
using Flurl.Http;


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
            
            Console.WriteLine("FRÅN API:"+response);
        }
    }
}
