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
            var client = new RSClient("localhost:5001/api/","Employees");
            var fetchedData = client.fetchData();
            Console.WriteLine(fetchedData);
        }


    }
}
