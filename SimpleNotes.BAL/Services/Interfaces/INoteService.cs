using System;
using System.Collections.Generic;
using System.Text;
using SimpleNotes.DAL.Entities;

namespace SimpleNotes.BAL.Services.Interfaces
{
    public interface INoteService
    {
        IList<Note> GetAllNotes();
        Note GetNote(int noteId);
        void AddNote(Note note);
        void UpdateNote(Note note);
        void DeleteNote(int noteId);
    }
}
