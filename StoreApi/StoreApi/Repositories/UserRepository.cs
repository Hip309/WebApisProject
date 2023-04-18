using Microsoft.EntityFrameworkCore;
using StoreApi.Entities;
using StoreApi.IRepositories;

namespace StoreApi.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(StoreContext context) : base(context) { }

    }
}
