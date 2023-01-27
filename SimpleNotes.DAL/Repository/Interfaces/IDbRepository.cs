using SimpleNotes.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleNotes.DAL.Repository.Interfaces
{
    public interface IDbRepository<T>
    {
        IList<T> GetAll();
        Task<IList<T>> GetAllAsync(CancellationToken cancel);
        T Get(int Id);
        Task<T> GetAsync(int Id, CancellationToken cancel);
        void Add(T item);
        Task AddAsync(T item, CancellationToken cancel);
        void Update(T item);
        Task UpdateAsync(T item, CancellationToken cancel);
        void Remove(int Id);
        Task RemoveAsync(int Id, CancellationToken cancel);
    }
}
