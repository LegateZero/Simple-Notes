using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNotes.DAL.Context
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
    }
}
