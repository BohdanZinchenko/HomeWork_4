using System;
using Newtonsoft.Json;
using System.Net;
using ClassLibrary;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.VisualBasic;

namespace Task_2
{
    class Program
    {
        
        static void Main(string[] args)
        {


            
            Console.WriteLine("Zinchenko Bohdan , student of PM Tech Academy");
            Console.WriteLine("Task 2 , Currency Converter ");
            Console.WriteLine("Input the current currency and new , and amount what you want to convert");

            HttpClient client = new HttpClient();
            try
            { 
                var response = client.GetAsync("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json"); 
                response.Result.EnsureSuccessStatusCode(); 
                var responseBody = response.Result.Content.ReadAsStringAsync(); 
                var readFile = JsonConvert.DeserializeObject<List<ExchangeRates>>(responseBody.Result); 
                var writeFile = JsonConvert.SerializeObject(readFile); 
                File.WriteAllText(path:"cache.json",writeFile);

            }
            catch
            {
                Console.WriteLine("We cant update Exchange");
                try
                {
                    JsonConvert.DeserializeObject(File.ReadAllText(path:"cache.json"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Problem with take currency Error (" + ex.Message+")");
                    return;
                }
            }
           
            var readFileLocal = JsonConvert.DeserializeObject<List<ExchangeRates>>(File.ReadAllText(path: "cache.json"));
            
            while(true)
            {
                Console.Write("Input current currency (3 symbols type ) = ");
                var currentCurrency = Console.ReadLine();
                
                if( string.IsNullOrWhiteSpace(currentCurrency))
                {
                    Console.WriteLine("Error input ");
                    continue;
                }
                currentCurrency = currentCurrency.Trim();
                if (currentCurrency.Length != 3)
                {
                    Console.WriteLine("Error input ");
                    continue;
                }
                Console.Write("Input currency you want to transfer  (3 symbols type ) = ");

                var newCurrency = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(newCurrency))
                {
                    Console.WriteLine("Error input ");
                    continue;
                }
                newCurrency = newCurrency.Trim();
                if (newCurrency.Length != 3)
                {
                    Console.WriteLine("Error input ");
                    continue;
                }
                Console.Write("Input amount = ");

                var currentAmount = 0m;

                decimal.TryParse(Console.ReadLine(),out currentAmount);
                if (currentAmount <= 0)
                {
                    Console.WriteLine("Error input");
                    continue;
                }
                
                var answer = Converter(currentCurrency, newCurrency, currentAmount, readFileLocal);
                if(answer <=0)
                {
                  Console.WriteLine($"Pair {currentCurrency} , {newCurrency} not founded");
                  return;
                }
                Console.WriteLine($"Amount is = {answer.ToString("#.##")}");
                
                break;
            }
        }

        
        public static decimal Converter(string currentCurrency, string newCurrency, decimal currentAmount , List<ExchangeRates> readFileLocal)
        {
          var currentEx = 0m;
          var newEx = 0m;
          var result = 0m;

            var elem = readFileLocal.Find(x => x.Cc == currentCurrency);
            if (elem != null)
            {
                currentEx = elem.Rate;
            }
            else
            {
                return -1;
            }
            elem = readFileLocal.Find(x => x.Cc == newCurrency);
            if ( elem != null)
            {
                newEx= elem.Rate;
                Console.WriteLine($"Current rate is = {elem.Rate}");
                Console.WriteLine($"Course is valid  as of {elem.ExchangeDate}");
            }
            else
            {
                return -1;
            }
            
            result = currentAmount * currentEx / newEx;
            return result;
        }
    }
}
