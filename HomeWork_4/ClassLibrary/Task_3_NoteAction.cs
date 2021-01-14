using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClassLibrary;
using Newtonsoft.Json;

namespace ClassLibrary
{
    public class NoteAction
    {
        private readonly ControlMenu _controlMenu;
        private static int _id;
        private readonly Note note;

        public NoteAction(ControlMenu controlMenu)
        {
            _controlMenu = controlMenu;
            _id = 0;
        }
        public NoteAction(ControlMenu controlMenu,Note listNote)
        {
            _controlMenu = controlMenu;
            _id = listNote.Id;
        }
        public void Update(List<Note> listNote)
        {
            var update = JsonConvert.SerializeObject(listNote);
            File.WriteAllText(path:"Data.json",update);
        }
        public Note AddNote()
        {
            Console.WriteLine("Your note : ");
            var body = Console.ReadLine();
            if (string.IsNullOrEmpty(body))
            {
                _controlMenu.FirstMenu();
                return null;
            }
            var trimedBody = body.Trim();
            if (string.IsNullOrEmpty(trimedBody))
            {
                Console.WriteLine("Program cant add empty note ");
                _controlMenu.FirstMenu();
                return null;
            }
            string title ="";
            if (trimedBody.Length > 32)
            {
                var titleArray = trimedBody.ToCharArray(0, 32);
                foreach (var item in titleArray)
                {
                    title = title + item;
                }
            }
            else
            {
                title = trimedBody;
            }

            _id++;
            Note newNote = new Note()
            {
                Id = _id,
                Title = title,
                Text = trimedBody,
                CreatedOn = DateTime.UtcNow.Date
            };
            Console.WriteLine("Note was added");
            return newNote;
        }

        public void FindNoteByElem(List<Note> listNotes)
        {
            Console.Write("What you want to find : ");
            var searchElem = Console.ReadLine();
            if (string.IsNullOrEmpty(searchElem))
            {
                foreach (var item in listNotes)
                {
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.WriteLine($"Id : {item.Id}");
                    Console.WriteLine($"Title : {item.Title}...");
                    Console.WriteLine($"Created on {item.CreatedOn.ToString("d")}");
                    Console.WriteLine("---------------------------------------------------------------------");
                }
            }
            else
            {
                var findedElems = listNotes.Select(x => x).Where(x =>
                    x.Text.Contains(searchElem) || x.CreatedOn.ToString().Contains(searchElem) ||
                    x.Id.ToString().Contains(searchElem)); 
                
                foreach (var item in findedElems)
                {
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.WriteLine($"Id : {item.Id}");
                    Console.WriteLine($"Title : {item.Title}...");
                    Console.WriteLine($"Created on {item.CreatedOn.ToString("d")}");
                    Console.WriteLine("---------------------------------------------------------------------");
                }
            }

        }

        public void FindNote(List<Note> listNotes)
        { 
            Console.Write("Input id of note what you want to find : "); 
            var id =  Console.ReadLine();

           var FindedElem = listNotes.Find(x => x.Id.ToString() == id);
           if (FindedElem == null)
           {
               Console.WriteLine("Note with that id not founded ");
               return;
           }
           Console.WriteLine("---------------------------------------------------------------------");
           Console.WriteLine($"Id : {FindedElem.Id}");
           Console.WriteLine($"Title : {FindedElem.Title}...");
           Console.WriteLine($"Text : {FindedElem.Text}");
           Console.WriteLine($"Created on {FindedElem.CreatedOn.ToString("d")}");
            Console.WriteLine("---------------------------------------------------------------------");


        }

        public Note DeleteNote(List<Note> listNotes)
        {
            Console.Write("Input id of note what you want to find : ");
            var id = Console.ReadLine();

            var FindedElem = listNotes.Find(x => x.Id.ToString() == id);
            if (FindedElem == null)
            {
                Console.WriteLine("Note with that id not founded ");
                
                return null;
            }
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine($"Id : {FindedElem.Id}");
            Console.WriteLine($"Title : {FindedElem.Title}...");
            Console.WriteLine($"Text : {FindedElem.Text}");
            Console.WriteLine($"Created on {FindedElem.CreatedOn.ToString("d")}");
            Console.WriteLine("---------------------------------------------------------------------");

            Console.WriteLine("Are you sure you want to delete note");
            Console.WriteLine("Yes is Y / N is No");
            var answer = Console.ReadLine().ToUpper();
            switch (answer)
            {
                case "Y":
                    Console.WriteLine("Note Was Deleted");
                    return FindedElem;
                case "N":
                    _controlMenu.FirstMenu();
                    return null;
                
                default:
                    Console.WriteLine("Incorrect answer");
                    _controlMenu.FirstMenu();
                    return null;
                    

            }

            

        }

    }
}