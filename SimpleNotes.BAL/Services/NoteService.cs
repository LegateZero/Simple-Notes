using System;
using System.Collections.Generic;
using System.Linq;
using SimpleNotes.BAL.Services.Interfaces;
using SimpleNotes.DAL.Context;
using SimpleNotes.DAL.Entities;

namespace SimpleNotes.BAL.Services
{
    public class NoteService : INoteService
    {
        private SimpleNotesDb _context;

        public NoteService(SimpleNotesDb context)
        {
            _context = context;
        }
        public void DeleteNote(int noteId)
        {
            var noteToDelete = _context.Notes.Local.FirstOrDefault(note => note.NoteId == noteId) ?? new Note(){NoteId = noteId};
            _context.Notes.Remove(noteToDelete);
            _context.SaveChanges();
        }

        public void AddNote(Note note)
        {
            _context.Notes.Add(note);
            _context.SaveChanges();
        }

        public IEnumerable<Note> GetAllNotes()
        {
            return _context.Notes.ToList();
        }
    }
}
