using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SimpleNotes.DAL.Entities;

namespace SimpleNotes.DAL.Context
{
    public class SimpleNotesDb : DbContext
    {
        public DbSet<Note> Notes { get; set; }

        public SimpleNotesDb()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"Filename=SimpleNotes.db";
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
