using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApi.Entities;
using StoreApi.Models.RequestModels;
using StoreApi.Models.RspondModels;
using StoreApi.Services;
using StoreApi.Services.Interfaces;

namespace StoreApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService= productService;
        }

        // GET: api/Products
        [HttpGet]
        public IActionResult GetProductList(int pageIndex, int pageSize)
        {
            if (pageSize <= 0 || pageIndex <= 0)
            {
                pageIndex = 1;
                pageSize = 10;
            }
            var productList = _productService.GetAllProducts(pageIndex, pageSize);
            if (productList == null)
            {
                return BadRequest();
            }
            return Ok(productList);
        }

        [ActionName("fillByCategoryId")]
        [HttpGet("{categoryId}")]
        public IActionResult GetProductsByCategoryId([FromRoute]int categoryId, int pageIndex, int pageSize)
        {
            var productList = _productService.GetProductListByCategoryId(categoryId,pageIndex, pageSize);
            if (productList == null)
            {
                return BadRequest();
            }
            return Ok(productList);
        }

        [ActionName("SearchProduct")]
        [HttpGet("{search}")]
        public IActionResult GetProductsBySearchString([FromRoute]string search, int pageIndex, int pageSize)
        {
            var productList =  _productService.GetProductsBySearchString(search, pageIndex, pageSize);
            if (productList == null)
            {
                return BadRequest();
            }
            return Ok(productList);
        }

        // GET: api/Products/5
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            var productRespond = await _productService.GetProductById(productId);
            if (productRespond.StatusCode == 200)
            {
                return StatusCode(StatusCodes.Status200OK, productRespond);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, productRespond);
            }
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid productId, [FromBody] ProductRequest productRequest)
        {
            ProductRespond productRespond = new ProductRespond();
            if (productId != productRequest.Id)
            {
                return StatusCode(StatusCodes.Status400BadRequest, productRespond);
            }
            else
            {
                productRespond = await _productService.UpdateProduct(productRequest);
                if (productRespond.StatusCode == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, productRespond);
                }
                else if (productRespond.StatusCode == 400)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, productRespond);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, productRespond);
                }
            }

        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductRequest productRequest)
        {
            var productRespond = await _productService.CreateProduct(productRequest);
            if (productRespond.StatusCode == 200)
            {
                return StatusCode(StatusCodes.Status200OK, productRespond);
            }
            else if (productRespond.StatusCode == 400)
            {
                return StatusCode(StatusCodes.Status400BadRequest, productRespond);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, productRespond);
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            var productRespond = await _productService.DeleteProduct(productId);
            if (productRespond.StatusCode == 200)
            {
                return StatusCode(StatusCodes.Status200OK, productRespond);
            }
            else if (productRespond.StatusCode == 404)
            {
                return StatusCode(StatusCodes.Status404NotFound, productRespond);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, productRespond);
            }
        }
    }
}
