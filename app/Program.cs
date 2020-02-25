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
            var client = new RSClient("https://cat-fact.herokuapp.com/","facts");
            var fetchedData = client.fetchData();
            Console.WriteLine(fetchedData);
            Console.WriteLine(client);
        }


    }
}
