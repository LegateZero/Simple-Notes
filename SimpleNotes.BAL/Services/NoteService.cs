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

        public Note GetNote(int noteId)
        {
            return _context.Notes.FirstOrDefault(note => note.NoteId == noteId);
        }

        public IList<Note> GetAllNotes()
        {
            return _context.Notes.ToList();
        }

        public void AddNote(Note note)
        {
            _context.Notes.Add(note);
            _context.SaveChanges();
        }

        public void UpdateNote(Note noteUpdate)
        {
            var tempNote = _context.Notes.FirstOrDefault(note => note.NoteId == noteUpdate.NoteId);
            if (tempNote == null) throw new KeyNotFoundException();
            _context.Notes.Update(noteUpdate);
            _context.SaveChanges();
        }


        
        public void DeleteNote(int noteId)
        {
            var noteToDelete = _context.Notes.Local.FirstOrDefault(note => note.NoteId == noteId) ?? new Note() { NoteId = noteId };
            _context.Notes.Remove(noteToDelete);
            _context.SaveChanges();
        }
    }
}
