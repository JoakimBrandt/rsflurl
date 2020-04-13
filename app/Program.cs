using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using Client;
using System.Linq;
using Flurl;
using Flurl.Http;
using RestSharp;
using FlurlClient = Flurl.Http.FlurlClient;
using RestSharp.Serializers.NewtonsoftJson;

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

            Console.WriteLine("How many calls do you want to send?");
            var amountOfCalls = Console.ReadLine();
            Console.WriteLine("You entered '{0}'", amountOfCalls);

            Console.WriteLine("How many entries per call do you want?");
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
                List<Task> allDownloads = new List<Task> { };
                List<RestRequest> restRequestsList = new List<RestRequest> { };
                var restClient = new RestClient("https://localhost:44371/");
                var flurlClient = new FlurlClient("https://localhost:44371/");
                restClient.UseNewtonsoftJson();

                Thread.Sleep(2000);
                
                if (success) {
                    if(typeOfClient == "rs") {

                        for (int i = 0; i < amountOfCalls; i++)
                        {
                            var getRequest = new RestRequest($"employees/{amountOfDataPerCall}");
                            restRequestsList.Add(getRequest);
                        }

                        Thread.Sleep(2000);

                        stopwatch.Start();

                        //var requestTasks = restRequestsList.Select(i => RSFetchAsync(restClient, i)).ToArray();

                        foreach (var request in restRequestsList)
                        {
                            allDownloads.Add(RSFetchAsync(restClient, request));
                        }

                        await Task.WhenAll(allDownloads);
                        stopwatch.Stop();

                    } else if(typeOfClient == "fl") {
                        
                        stopwatch.Start();

                        for (int i = 0; i < amountOfCalls; i++)
                        {
                            allDownloads.Add(FLFetchAsync(flurlClient, amountOfDataPerCall));
                        }

                        await Task.WhenAll(allDownloads);
                        stopwatch.Stop();

                    }
                    else { 
                        throw new Exception("You have not chosen any HTTP-Client");
                    }

                    long elapsedTime = stopwatch.ElapsedMilliseconds;
                    Console.WriteLine("RunTime in ms: " + elapsedTime);
                    
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"There was an exception: {ex.ToString()}");
                throw;
            }   
        }


        private async static Task RSFetchAsync(RestClient client, RestRequest getRequest) {

            var response = await client.ExecuteAsync(getRequest);
        }

        private async static Task FLFetchAsync(FlurlClient client, string amountOfData) {

            var response = await client.Request().AppendPathSegment("employees/").AppendPathSegment(amountOfData).GetStringAsync();
        }
    }
}
