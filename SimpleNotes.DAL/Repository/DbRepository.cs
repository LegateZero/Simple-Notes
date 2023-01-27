using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using SimpleNotes.DAL.Context;
using SimpleNotes.DAL.Entities;
using SimpleNotes.DAL.Repository.Interfaces;

namespace SimpleNotes.DAL.Repository
{
    public class NoteRepository : IDbRepository<Note>
    {
        private readonly SimpleNotesDb _db;
        private readonly DbSet<Note> _Set;
        public NoteRepository(SimpleNotesDb db)
        {
            _db = db;
            _Set = db.Set<Note>();
        }

        public virtual IQueryable<Note> Items => _Set;


        public IList<Note> GetAll() => 
            Items.ToList();

        public async Task<IList<Note>> GetAllAsync(CancellationToken cancel = default) =>
            await Items.ToListAsync(cancel);

        public Note Get(int id) => Items.SingleOrDefault(item => item.NoteId == id);

        public async Task<Note> GetAsync(int id, CancellationToken cancel = default) =>
            await Items.SingleOrDefaultAsync(item => item.NoteId == id, cancel)
                .ConfigureAwait(false);

        public void Add(Note item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            _db.SaveChanges();
        }

        public async Task AddAsync(Note item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Added;
            await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
        }

        public void Update(Note item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public async Task UpdateAsync(Note item, CancellationToken cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _db.Entry(item).State = EntityState.Modified;
            await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
        }

        public void Remove(int id)
        {
            var item = 
                _Set.Local.FirstOrDefault(i => i.NoteId == id) 
                ?? new Note { NoteId = id };
            _db.Remove(item);
            _db.SaveChanges();
        }

        public async Task RemoveAsync(int id, CancellationToken cancel = default)
        {
            _db.Remove(new Note { NoteId = id });
            await _db.SaveChangesAsync(cancel).ConfigureAwait(false);
        }
    }
}
