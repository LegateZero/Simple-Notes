using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SimpleNotes.BAL.Services.Interfaces;
using SimpleNotes.DAL.Context;
using SimpleNotes.DAL.Entities;
using SimpleNotes.DAL.Repository;

namespace SimpleNotes.BAL.Services
{
    public class NoteService : INoteService
    {
        private NoteRepository _notes;

        public NoteService(NoteRepository notes)
        {
            _notes = notes;
        }

        public Note GetNote(int noteId)
        {
            return _notes.Get(noteId);
        }

        public IList<Note> GetAllNotes()
        {
            return _notes.GetAll();
        }

        public void AddNote(Note note)
        {
            _notes.Add(note);
        }

        public void UpdateNote(Note noteUpdate)
        {
            if (_notes.GetAll().All(note => note.NoteId != noteUpdate.NoteId))
                throw new ArgumentOutOfRangeException();
            _notes.Update(noteUpdate);
        }


        
        public void DeleteNote(int noteId)
        {
            _notes.Remove(noteId);
        }
    }
}
