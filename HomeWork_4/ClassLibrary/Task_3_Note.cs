using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ClassLibrary
{
    public class Note : INote
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }

        

    }
}

