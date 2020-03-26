﻿using System;
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

            Console.WriteLine("How much data per call do you want? Type 1, 2 or 3: ");
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
                Stopwatch stopwatch = new Stopwatch();
                Stopwatch stopwatch2 = new Stopwatch();
                List<Task> allDownloads = new List<Task>{};

                if(success) {
                    if(typeOfClient == "RS") {
                        stopwatch.Start();
                        stopwatch2.Start();

                        //TODO loopen i sig tar 154 ms... kom ihåg
                        for (int i = 0; i < amountOfCalls; i++)
                        {
                            allDownloads.Add(RSFetchAsync());
                        }

                        stopwatch.Stop();
                        await Task.WhenAll(allDownloads);
                        stopwatch2.Stop();

                    } else if(typeOfClient == "FL") {
                        stopwatch.Start();
                        stopwatch2.Start();

                        //TODO loopen i sig tar 154 ms... kom ihåg
                        for (int i = 0; i < amountOfCalls; i++)
                        {
                            allDownloads.Add(FLFetchAsync());
                        }

                        stopwatch.Stop();
                        await Task.WhenAll(allDownloads);
                        stopwatch2.Stop();
                    } else { 
                        Console.WriteLine("You have not choosed any HTTP-CLient");
                    }

                    long elapsedTime = stopwatch.ElapsedMilliseconds;
                    Console.WriteLine("RunTime downloads123 " + elapsedTime);
                    
                    long elapsedTime2 = stopwatch2.ElapsedMilliseconds;
                    Console.WriteLine("RunTime downloads123 " + elapsedTime2);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"There was an exception: {ex.ToString()}");
                throw;
            }   
        }


        private async static Task RSFetchAsync() {
            var client = new RestClient("https://api.postcodes.io");
            var getRequest = new RestRequest("postcodes/{postcode}");
            getRequest.AddUrlSegment("postcode", "IP1 3JR");

            //TODO is this the best stopwatch?
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var singleGeocodeResponseContainer = await client.ExecuteAsync(getRequest);
            
            stopwatch.Stop();

            long elapsedTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine("RS RunTime " + elapsedTime);

            var singleGeocodeResponse = singleGeocodeResponseContainer.Content;
            Console.WriteLine("FRÅN API:"+singleGeocodeResponse); 
        }

        private async static Task FLFetchAsync() {

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var response = await "https://api.postcodes.io"
                .AppendPathSegment("postcodes")
                .AppendPathSegment("IP1 3JR")
                .GetJsonAsync();
            
            stopwatch.Stop();

            long elapsedTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine("FL RunTime " + elapsedTime);
            
            
            Console.WriteLine("API hämtar postcoden från json objektets resultat:" + response.result.postcode);
        }

        


    }
}