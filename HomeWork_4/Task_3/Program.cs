using System;
using System.Collections.Generic;
using System.IO;
using ClassLibrary;
using Newtonsoft.Json;
namespace Task_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 3 , Note program");
            Console.WriteLine("Program where you can note some info ");
            Console.WriteLine("Student of PM academy Zinchenko Bohdan");
            List<Note> readFile;
            try
            {
                readFile = JsonConvert.DeserializeObject<List<Note>>(File.ReadAllText(path: "Data.json"));
            }
            catch
            { 
                readFile = new List<Note>();
            }

            var menuControl = new ControlMenu(readFile);
            
            menuControl.FirstMenu();
        }
    }
}