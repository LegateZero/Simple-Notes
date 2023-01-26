using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleNotes.BAL.Services.Interfaces
{
    public interface INoteService
    {
        void DeleteNote(int noteId);
        void AddNote(NoteService note);
        IEnumerable<Note>
    }
}
