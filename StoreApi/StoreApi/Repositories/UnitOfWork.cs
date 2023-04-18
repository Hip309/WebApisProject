using Microsoft.EntityFrameworkCore;
using StoreApi.Entities;
using StoreApi.IRepositories;

namespace StoreApi.Repositories
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly StoreContext _context;
        public IUserRepository Users { get; }
        public ICategoryRepository Categories { get; }
        public IProductRepository Products { get; }

        public UnitOfWork(StoreContext context, IUserRepository users, ICategoryRepository categories, IProductRepository products) { 
            _context = context;
            Users = users;
            Categories = categories;
            Products = products;
        }

        public int Save() {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
