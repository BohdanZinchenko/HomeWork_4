using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    interface INote
    {
        int Id { get; }
        string Title { get; }   
        string Text { get; }     
        DateTime CreatedOn { get; } 

    }
}
