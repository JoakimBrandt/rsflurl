using System;
using System.Threading.Tasks;
using Client;


namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! ");
            
            fetch();
            
        }

        static void fetch() {
            //var client = new RSClient("https://api.nasa.gov/","planetary/apod");
            var client = new FlurlClient("https://api.nasa.gov/", "planetary/apod");
            //var fetchedData = client.fetchData();
            Console.WriteLine("SKRIVER LITE SÅ DET TAR TID.........................");
            var response = client.getJsonAsync();
            //Console.WriteLine("FRÅN NASA:"+fetchedData);
            Console.WriteLine("FRÅN NASA:"+response);
        }


    }
}
