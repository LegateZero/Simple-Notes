using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleNotes.DAL.Context;

namespace SimpleNotes.DAL.Entities
{
    public class SimpleNoteDb : DbContext
    {
        public DbSet<Note> Notes { get; set; }

        public SimpleNoteDb(DbContextOptions<SimpleNoteDb> options) 
            : base(options) { }
    }
}
