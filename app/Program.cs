using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;

using Client;

using Flurl;
using Flurl.Http;
using RestSharp;

/*
    /get request to localhost:3000/Employees
 */

namespace app
{
    class Program
    {
        private static int _amountOfCallsArrPosition = 0;
        private static int _amountOfDataPerCallArrPosition = 1;
        private static int _typeOfClientArrPosition = 2;

        public static void Main(string[] args)
        {
            string[] arrayChoices = menuChoices();

            try
            {
                //ha inparameter i fetch()
                fetch(arrayChoices).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an exception: {ex.ToString()}");
            }
        }

        private static string[] menuChoices()
        {
            string[] choicesArr = new string[3];

            Console.WriteLine("How many anrop do you want to send? Type 1, 8, 64, 128...: ");
            var amountOfCalls = Console.ReadLine();
            Console.WriteLine("You entered '{0}'", amountOfCalls);

            Console.WriteLine("How much entries per call do you want? Type 100, 3213 or 10000: ");
            var amountOfDataPerCall = Console.ReadLine();
            Console.WriteLine("You entered '{0}'", amountOfDataPerCall);

            Console.WriteLine("Which Http Client do you want to use? Type FL or RS: ");
            var typeOfClient = Console.ReadLine();
            Console.WriteLine("You entered '{0}'", typeOfClient);

            choicesArr[0] = amountOfCalls;
            choicesArr[1] = amountOfDataPerCall;
            choicesArr[2] = typeOfClient;

            return choicesArr;
        }


        private async static Task fetch(string[] choicesArr) {            
            int amountOfCalls;
            bool success = int.TryParse(choicesArr[_amountOfCallsArrPosition], out amountOfCalls);

            try
            {
                string typeOfClient = choicesArr[_typeOfClientArrPosition];
                string amountOfDataPerCall = choicesArr[_amountOfDataPerCallArrPosition];
                Stopwatch stopwatch = new Stopwatch();
                List<Task> allDownloads = new List<Task>{};

                if(success) {
                    if(typeOfClient == "RS") {
                        //TODO bästa stopwatchen?
                        stopwatch.Start();

                        //TODO loopen i sig tar 154 ms... kom ihåg
                        for (int i = 0; i < amountOfCalls; i++)
                        {
                            allDownloads.Add(RSFetchAsync(amountOfDataPerCall));
                        }

                        await Task.WhenAll(allDownloads);
                        stopwatch.Stop();

                    } else if(typeOfClient == "FL") {
                        
                        stopwatch.Start();
                        //TODO loopen i sig tar 154 ms... kom ihåg
                        for (int i = 0; i < amountOfCalls; i++)
                        {
                            allDownloads.Add(FLFetchAsync(amountOfDataPerCall));
                        }

                        await Task.WhenAll(allDownloads);
                        stopwatch.Stop();

                    }
                    else { 
                        Console.WriteLine("You have not choosed any HTTP-CLient");
                    }

                    long elapsedTime = stopwatch.ElapsedMilliseconds;
                    Console.WriteLine("RunTime inner " + elapsedTime);
                    
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"There was an exception: {ex.ToString()}");
                throw;
            }   
        }


        private async static Task RSFetchAsync(string amountOfEmployees) {
            //TODO skapa ej RC och RR här? tar tid ifrån experimentet
            var client = new RestClient("https://localhost:44371/");
            var getRequest = new RestRequest($"employees/{amountOfEmployees}");
            var singleGeocodeResponseContainer = await client.ExecuteAsync(getRequest);
            
            /* 
            var singleGeocodeResponse = singleGeocodeResponseContainer.Content;
            Console.WriteLine("FRÅN API:"+singleGeocodeResponse); 
            */
        }

        private async static Task FLFetchAsync(string amountOfEmployees) {

            var response = await "https://localhost:44371/"
                .AppendPathSegment("employees")
                .AppendPathSegment(amountOfEmployees)
                .GetJsonListAsync();
            
            //Console.WriteLine("API hämtar postcoden från json objektets resultat:" + response[0].name);
        }


    }
}
