using StoreApi.Entities;

namespace StoreApi.IRepositories
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        List<Product> GetProductsByCategoryId(int categoryId, int pageIndex, int pagesize);
        List<Product> GetProductsBySearchString(string searchStr, int pageIndex, int pagesize);
    }
}
