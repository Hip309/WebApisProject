using AutoMapper;
using StoreApi.Entities;
using StoreApi.IRepositories;
using StoreApi.Logger;
using StoreApi.Models.RequestModels;
using StoreApi.Models.RspondModels;
using StoreApi.Services.Interfaces;
using System.Drawing.Printing;

namespace StoreApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        public readonly ILoggerService _logger;
        public IUnitOfWork _unitOfwork;

        public ProductService(IUnitOfWork unitOfwork, IMapper mapper, ILoggerService logger)
        {
            _unitOfwork = unitOfwork;
            _mapper = mapper;
            _logger = logger;
        }

        //add new product
        public async Task<ProductRespond> CreateProduct(ProductRequest pRequest)
        {
            ProductRespond pRespond = new ProductRespond();
            if (pRequest != null)
            {
                _logger.LogInfo("Inserting new Product into database");
                pRequest.Id = Guid.NewGuid();
                var newProduct = _mapper.Map<Product>(pRequest);
                await _unitOfwork.Products.Add(newProduct);
                var result = _unitOfwork.Save();
                if (result > 0)
                {
                    _logger.LogInfo("Insert new Product success");
                    pRespond = _mapper.Map<ProductRespond>(newProduct);
                    pRespond.StatusCode = 200;
                    pRespond.ResponseMessage = "Create new Product success";
                    return pRespond;
                }
                else
                {
                    pRespond.StatusCode = 500;
                    pRespond.ResponseMessage = "Create Product method went wrong";
                    return pRespond;
                }
            }
            _logger.LogWarn("Request is wrong");
            pRespond.StatusCode = 400;
            pRespond.ResponseMessage = "Request is empty";
            return pRespond;
        }

        //delete a exist product
        public async Task<ProductRespond> DeleteProduct(Guid ProductId)
        {
            ProductRespond pRespond = new ProductRespond();
            if (ProductId != null)
            {
                _logger.LogInfo("Searching the detele product in database");
                var deleteProduct = await _unitOfwork.Products.GetById(ProductId);
                if (deleteProduct != null)
                {
                    _logger.LogInfo("Deleting product");
                    _unitOfwork.Products.Delete(deleteProduct);
                    var result = _unitOfwork.Save();

                    if (result > 0)
                    {
                        _logger.LogInfo("Delete product success");
                        pRespond = _mapper.Map<ProductRespond>(deleteProduct);
                        pRespond.StatusCode = 200;
                        pRespond.ResponseMessage = "Delete product success";
                        return pRespond;
                    }
                    else
                    {
                        pRespond.StatusCode = 500;
                        pRespond.ResponseMessage = "Delete product method went wrong";
                        return pRespond;
                    }
                }
            }
            _logger.LogWarn("Product is not exist");
            pRespond.StatusCode = 404;
            pRespond.ResponseMessage = "Product is not exist";
            return pRespond;
        }

        //get list all product
        public ProductRespondList GetAllProducts(int pageIndex, int pageSize)
        {
            _logger.LogInfo("Fetching All Product");
            var productList = _unitOfwork.Products.GetAll(pageIndex,pageSize);
            var respondList = new ProductRespondList();
            respondList.RespondList = _mapper.Map<List<ProductRespond>>(productList);
            respondList.PageCurrent= pageIndex;
            respondList.PageSize = pageSize;
            respondList.PageCount = productList.Count;
            if (productList == null)
            {
                _logger.LogWarn("No Product is exist in database");
                respondList.StatusCode = 204;
            }
            _logger.LogInfo($"Total Product responses: {respondList.PageCount}");
            return respondList;
        }

        //get a product by id
        public async Task<ProductRespond> GetProductById(Guid ProductId)
        {
            ProductRespond pRespond = new ProductRespond();
            _logger.LogInfo("Searching product in database");
            var product = await _unitOfwork.Products.GetById(ProductId);
            if (product != null)
            {
                _logger.LogInfo("Find out product in database");
                pRespond = _mapper.Map<ProductRespond>(product);
                pRespond.StatusCode = 200;
                pRespond.ResponseMessage = "Get product success";
                return pRespond;
            }
            _logger.LogWarn("Can not find product in database");
            pRespond.StatusCode = 404;
            pRespond.ResponseMessage = "Get product method went wrong";
            return pRespond;
        }

        //get list product of a product by productId
        public ProductRespondList GetProductListByCategoryId(int productId, int pageIndex, int pageSize)
        {
            _logger.LogInfo("Searching product in database");
            var productList = _unitOfwork.Products.GetProductsByCategoryId(productId, pageIndex, pageSize);
            ProductRespondList respondList = new ProductRespondList(); 
            respondList.RespondList = _mapper.Map<List<ProductRespond>>(productList);
            respondList.PageCurrent = pageIndex;
            respondList.PageSize = pageSize;
            respondList.PageCount = productList.Count;
            if (respondList.RespondList == null)
            {
                _logger.LogWarn("Request Product is not exist in database");
                respondList.StatusCode = 204;
            }
            _logger.LogInfo($"Total Product responses: {respondList.PageCount}");
            return respondList;
        }

        // search product
        public ProductRespondList GetProductsBySearchString(string searchStr, int pageIndex, int pageSize)
        {
            _logger.LogInfo("Searching product in database");
            var productList =  _unitOfwork.Products.GetProductsBySearchString(searchStr,pageIndex, pageSize);
            ProductRespondList respondList = new ProductRespondList();
            respondList.RespondList = _mapper.Map<List<ProductRespond>>(productList);
            respondList.PageCurrent = pageIndex;
            respondList.PageSize = pageSize;
            if (productList == null)
            {
                _logger.LogWarn("Request Product is not exist in database");
                respondList.StatusCode = 204;
            }
            _logger.LogInfo($"Total Product responses: {respondList.PageCount}");
            return respondList;
        }

        //edit a product
        public async Task<ProductRespond> UpdateProduct(ProductRequest pRequest)
        {
            ProductRespond cRespond = new ProductRespond();
            if (pRequest != null)
            {
                _logger.LogInfo("Searching the update product in database");
                var existedProduct = await _unitOfwork.Products.GetById(pRequest.Id);
                if (existedProduct != null)
                {
                    existedProduct.Name = pRequest.Name;
                    existedProduct.Unit = pRequest.Unit;
                    existedProduct.Price = pRequest.Price;
                    existedProduct.Cost = pRequest.Cost;
                    existedProduct.Quantity = pRequest.Quantity;
                    existedProduct.Image = pRequest.Image;
                    existedProduct.CategoryId = pRequest.CategoryId;

                    _logger.LogInfo("Updating product");
                    _unitOfwork.Products.Update(existedProduct);
                    var result = _unitOfwork.Save();
                    if (result > 0)
                    {
                        _logger.LogInfo("Update product success");
                        cRespond = _mapper.Map<ProductRespond>(existedProduct);
                        cRespond.StatusCode = 200;
                        cRespond.ResponseMessage = "Update product success";
                        return cRespond;
                    }
                    else
                    {
                        cRespond = _mapper.Map<ProductRespond>(pRequest);
                        cRespond.StatusCode = 500;
                        cRespond.ResponseMessage = "Update product method went wrong";
                        return cRespond;
                    }
                }
            }
            _logger.LogWarn("Can not update product in");
            cRespond.StatusCode = 400;
            cRespond.ResponseMessage = "Request is empty";
            return cRespond;
        }

        
    }
}
