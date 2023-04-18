using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StoreApi.Entities;
using StoreApi.IRepositories;
using StoreApi.Models;

namespace StoreApi.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(StoreContext context) : base(context)
        {
        }

        public List<Product> GetProductsBySearchString(string searchStr, int pageIndex, int pagesize)
        {
            var productList = _context.Products.AsQueryable();
            if (!String.IsNullOrEmpty(searchStr))
            {
                productList = productList.Where(p => p.Name.Contains(searchStr));
            }
            
            var respondList = PaginatedList<Product>.Create(productList, pageIndex, pagesize);
            return respondList.Select(p => new Product
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                Name = p.Name,
                Unit = p.Unit,
                Price = p.Price,
                Cost = p.Cost,
                Quantity = p.Quantity,
                Image = p.Image,
            }).ToList();
            
        }

        public List<Product> GetProductsByCategoryId(int categoryId, int pageIndex, int pagesize)
        {
            var productList = _context.Products.AsQueryable();
            productList = productList.Where(p => p.CategoryId == categoryId); 
            var respondList = PaginatedList<Product>.Create(productList, pageIndex, pagesize);
            return respondList.Select(p => new Product
            {
                Id= p.Id,
                CategoryId = p.CategoryId,
                Name= p.Name,
                Unit = p.Unit,
                Price= p.Price,
                Cost= p.Cost,
                Quantity = p.Quantity,
                Image  = p.Image,
            }).ToList();
        }
    }
    
}
