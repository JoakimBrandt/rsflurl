﻿using System;
using System.Threading.Tasks;
using Client;

using Flurl;
using Flurl.Http;
using RestSharp;



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
                
                if(typeOfClient == "RS") {
                    if(success) {
                        //TODO skapa en funktion som kör x-antal loops
                        for (int i = 0; i < amountOfCalls; i++)
                        //TODO starta upp en tråd för varje anrop
                        {
                            var response = await "https://api.postcodes.io"
                            .AppendPathSegment("postcodes")
                            .AppendPathSegment("IP1 3JR")
                            .GetJsonAsync();
                        
                            Console.WriteLine("API hämtar postcoden från json objektets resultat:" + response.result.postcode);
                        }
                    }
                } else if(typeOfClient == "FL") {
                    //TODO skapa en funktion som kör x-antal loops
                    //TODO run FLurl http-client here
                } else { 
                    Console.WriteLine("You have not choosed any HTTP-CLient");
                }
            }
            
            catch (Exception ex)
            {
                Console.WriteLine($"There was an exception: {ex.ToString()}");
                throw;
            }

        
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
