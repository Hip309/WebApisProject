using StoreApi.Models.RequestModels;
using StoreApi.Models.RspondModels;

namespace StoreApi.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryRespond> CreateCategory(CategoryRequest cRequest);

        CategoryRespondList GetAllCategories(int pageIndex, int pageSize);

        Task<CategoryRespond> GetCategoryById(int CategoryId);

        Task<CategoryRespond> UpdateCategory(CategoryRequest cRequest);

        Task<CategoryRespond> DeleteCategory(int CategoryId);
    }
}
