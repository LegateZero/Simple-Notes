using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleNotes.DAL.Entities
{
    public class Note
    {
        public int NoteId { get; set; }

        [MaxLength(255)]
        public string Header { get; set; }
        public string Body { get; set; }
    }
}
