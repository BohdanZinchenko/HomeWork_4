using System;
using System.IO;
using ClassLibrary;
using System.Text.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {

            var time = new Stopwatch();
            time.Start();
            

            var json = File.ReadAllText(path:"​settings.json");
            try
            {
                JsonSerializer.Deserialize<Settings>(json);
            }
            catch (Exception ex)
            {

                TimeSpan ts = time.Elapsed;
                var falseResult = new Result()
                {
                    Success = false,
                    Error = ex.Message.ToString(),
                    TimeWork = String.Format("{0:00}:{1:00}:{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10),
                    PrimesArray = null

                };
                var falseJsonResult = JsonSerializer.Serialize<Result>(falseResult);
                File.WriteAllText(path: "result.json", falseJsonResult);

                return;
            }
            var settings = JsonSerializer.Deserialize<Settings>(json);
            var primesArray = PrimeNumbers(settings.PrimesFrom, settings.PrimesTo).ToArray();
            
            TimeSpan ts1 = time.Elapsed;
            var trueResult = new Result()
            {
                Success = true,
                Error = null,
           TimeWork = String.Format("{0:00}:{1:00}:{2:00}", ts1.Minutes, ts1.Seconds, ts1.Milliseconds / 10),
            PrimesArray = primesArray
        };
            var TrueJsonResult = JsonSerializer.Serialize<Result>(trueResult);
            File.WriteAllText(path: "result.json", TrueJsonResult);
            



        }

        public static List<int> PrimeNumbers(int low , int top)
        {

            List<int> primesArray = new List<int>();
            
            for (var i = low; i < top; i++)
            {
                if(i<=1)
                {
                    continue;
                }
                var isPrime = true;
                for (var j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (!isPrime) continue;
                primesArray.Add(i);

            }
            return primesArray;
        }
    }
}
