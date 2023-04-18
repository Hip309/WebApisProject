using Microsoft.EntityFrameworkCore;
using StoreApi.Entities;
using StoreApi.IRepositories;
using StoreApi.Models;
using System.Drawing.Printing;
using System.Linq;

namespace StoreApi.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly StoreContext _context;
        protected GenericRepository(StoreContext context)
        {
            _context = context;
        }
        public virtual async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual List<T> GetAll(int pageIndex, int pageSize)
        {
            var templist = _context.Set<T>().AsQueryable();
            var respondList = PaginatedList<T>.Create(templist,pageIndex, pageSize);
            return  respondList.Select(x => x).ToList();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
