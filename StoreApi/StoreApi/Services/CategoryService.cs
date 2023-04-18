using AutoMapper;
using StoreApi.Entities;
using StoreApi.IRepositories;
using StoreApi.Logger;
using StoreApi.Models.RequestModels;
using StoreApi.Models.RspondModels;
using StoreApi.Services.Interfaces;

namespace StoreApi.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly IMapper _mapper;
        public readonly ILoggerService _logger;
        public IUnitOfWork _unitOfwork;

        public CategoryService(IUnitOfWork unitOfwork, IMapper mapper, ILoggerService logger)
        {
            _unitOfwork = unitOfwork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CategoryRespond> CreateCategory(CategoryRequest cRequest)
        {
            CategoryRespond cRespond = new CategoryRespond();
            if (cRequest != null)
            {
                _logger.LogInfo("Inserting new Category into database");
                var newCategory = _mapper.Map<Category>(cRequest);
                await _unitOfwork.Categories.Add(newCategory);
                var result = _unitOfwork.Save();
                if (result > 0)
                {
                    _logger.LogInfo("Insert new Category success");
                    cRespond = _mapper.Map<CategoryRespond>(newCategory);
                    cRespond.StatusCode = 200;
                    cRespond.ResponseMessage = "Create new category success";
                    return cRespond;
                }
                else
                {
                    cRespond.StatusCode = 500;
                    cRespond.ResponseMessage = "Create Category method went wrong";
                    return cRespond;
                }
            }
            _logger.LogWarn("Request is wrong");
            cRespond.StatusCode = 400;
            cRespond.ResponseMessage = "Request is empty";
            return cRespond;
        }

        public async Task<CategoryRespond> DeleteCategory(int CategoryId)
        {
            CategoryRespond cRespond = new CategoryRespond();
            if (CategoryId > 0)
            {
                _logger.LogInfo("Searching the detele category in database");
                var deleteCategory = await _unitOfwork.Categories.GetById(CategoryId);
                if (deleteCategory != null)
                {
                    _logger.LogInfo("Deleting category");
                    _unitOfwork.Categories.Delete(deleteCategory);
                    var result = _unitOfwork.Save();

                    if (result > 0)
                    {
                        _logger.LogInfo("Delete category success");
                        cRespond = _mapper.Map<CategoryRespond>(deleteCategory);
                        cRespond.StatusCode = 200;
                        cRespond.ResponseMessage = "Delete category success";
                        return cRespond;
                    }
                    else
                    {
                        cRespond.StatusCode = 500;
                        cRespond.ResponseMessage = "Delete category method went wrong";
                        return cRespond;
                    }
                }
            }
            _logger.LogWarn("Category is not exist");
            cRespond.StatusCode = 404;
            cRespond.ResponseMessage = "Category is not exist";
            return cRespond;
        }

        public CategoryRespondList GetAllCategories(int pageIndex, int pageSize)
        {
            _logger.LogInfo("Fetching All Categories");
            var categoryList = _unitOfwork.Categories.GetAll(pageIndex, pageSize);
            var respondList = new CategoryRespondList();
            respondList.RespondList = _mapper.Map<List<CategoryRespond>>(categoryList);
            respondList.PageCurrent = pageIndex;
            respondList.PageSize = pageSize;
            respondList.PageCount = categoryList.Count;
            if (categoryList == null)
            {
                _logger.LogWarn("No Category is exist in database");
                respondList.StatusCode = 204;
            }
            _logger.LogInfo($"Total Category responses: {respondList.PageCount}");
            return respondList;
        }

        public async Task<CategoryRespond> GetCategoryById(int CategoryId)
        {
            CategoryRespond cRespond = new CategoryRespond();
            _logger.LogInfo("Searching category in database");
            var category = await _unitOfwork.Categories.GetById(CategoryId);
            if (category != null)
            {
                _logger.LogInfo("Find out category in database");
                cRespond = _mapper.Map<CategoryRespond>(category);
                cRespond.StatusCode = 200;
                cRespond.ResponseMessage = "Get category success";
                return cRespond;
            }
            _logger.LogWarn("Can not find category in database");
            cRespond.StatusCode = 404;
            cRespond.ResponseMessage = "Get category method went wrong";
            return cRespond;
        }

        public async Task<CategoryRespond> UpdateCategory(CategoryRequest cRequest)
        {
            CategoryRespond cRespond = new CategoryRespond();
            if (cRequest != null)
            {
                _logger.LogInfo("Searching the update category in database");
                var existedCategory = await _unitOfwork.Categories.GetById(cRequest.Id);
                if (existedCategory != null)
                {
                    existedCategory.Name = cRequest.Name;
                    existedCategory.Image = cRequest.Image;

                    _logger.LogInfo("Updating category");
                    _unitOfwork.Categories.Update(existedCategory);
                    var result = _unitOfwork.Save();
                    if (result > 0)
                    {
                        _logger.LogInfo("Update category success");
                        cRespond = _mapper.Map<CategoryRespond>(existedCategory);
                        cRespond.StatusCode = 200;
                        cRespond.ResponseMessage = "Update category success";
                        return cRespond;
                    }
                    else
                    {
                        cRespond = _mapper.Map<CategoryRespond>(cRequest);
                        cRespond.StatusCode = 500;
                        cRespond.ResponseMessage = "Update category method went wrong";
                        return cRespond;
                    }   
                }
            }
            _logger.LogWarn("Can not update category in");
            cRespond.StatusCode = 400;
            cRespond.ResponseMessage = "Request is empty";
            return cRespond;
        }
    }
}
