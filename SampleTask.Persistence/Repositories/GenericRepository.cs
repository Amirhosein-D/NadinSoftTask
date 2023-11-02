
using Microsoft.EntityFrameworkCore;
using SampleTask.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTask.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SampleTaskDbContext _context;
        private readonly DbSet<T> _db;

        public GenericRepository(SampleTaskDbContext context)
        {
            _context = context;
            _db = context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _db.AddAsync(entity);
            await SaveChange();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _db.Remove(entity);
            await SaveChange();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await GetByIdAsync(id);
            _db.Remove(result);
            await SaveChange();
        }

        public async Task<bool> ExistAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            return entity != null;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _db.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _db.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _db.Update(entity);
            await SaveChange();
        }


        private async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        } 
    }
}
