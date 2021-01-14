using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace ClassLibrary
{
    public class ControlMenu
    {
        private static  List<Note> _listNote;
       
        private static int _choseNum;
        public ControlMenu (List<Note> listNote)
        {
            _listNote = listNote ?? new List<Note>();
            
        }
        
        public  void FirstMenu()
        {
            NoteAction action;
            while (true)
            {

                if (_listNote.Count<=0)
                {
                    action = new NoteAction(new ControlMenu(_listNote));
                }
                else
                {
                    action = new NoteAction(new ControlMenu(_listNote), _listNote.Last());
                }
                

                Console.WriteLine("1: Find Note");
                Console.WriteLine("2: Overview Note");
                Console.WriteLine("3: Add Note");
                Console.WriteLine("4: Delete Note");
                Console.WriteLine("5: Exit");
                while (!int.TryParse(Console.ReadLine(), out _choseNum) && _choseNum < 0 || _choseNum > 5)
                {
                    Console.WriteLine("Error input , chose something from 1 to 5");
                }
                switch (_choseNum)
                {
                    case 1:
                        action.FindNoteByElem(_listNote);
                        break;
                    case 2:
                        action.FindNote(_listNote);
                        break;
                    case 3:
                        _listNote.Add(action.AddNote());
                        action.Update(_listNote);
                        break;
                    case 4:
                        _listNote.Remove(action.DeleteNote(_listNote));
                        action.Update(_listNote);
                        break;
                    case 5:
                        return;
                    default:
                        Console.WriteLine("You choose wrong command ,chose something from 1 to 5");
                        break;

                }
            }


        }
        

    }
}


