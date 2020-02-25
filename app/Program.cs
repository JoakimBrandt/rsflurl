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

        static async void fetch() {
            //var client = new RSClient("https://api.nasa.gov/","planetary/apod");
            var client = new FlurlClient("https://api.nasa.gov/", "planetary/apod");
            //var fetchedData = client.fetchData();
            var response = await client.getJsonAsync();
            //Console.WriteLine("FRÅN NASA:"+fetchedData);
                        Console.WriteLine("CAN U SEE ME??????");

            Console.WriteLine("FRÅN NASA:"+response);
        }


    }
}
