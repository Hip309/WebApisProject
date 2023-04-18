using StoreApi.Models.RequestModels;
using StoreApi.Models.RspondModels;

namespace StoreApi.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductRespond> CreateProduct(ProductRequest pRequest);

        ProductRespondList GetAllProducts(int pageIndex, int pageSize);
        ProductRespondList GetProductListByCategoryId(int categoryId, int pageIndex, int pageSize);
        ProductRespondList GetProductsBySearchString(string searchStr, int pageIndex, int pageSize);

        Task<ProductRespond> GetProductById(Guid ProductId);

        Task<ProductRespond> UpdateProduct(ProductRequest pRequest);

        Task<ProductRespond> DeleteProduct(Guid ProductId);
    }
}
