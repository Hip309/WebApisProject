using Microsoft.EntityFrameworkCore;
using StoreApi.Entities;
using StoreApi.IRepositories;

namespace StoreApi.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(StoreContext context) : base(context)
        {
        }
    }
}
