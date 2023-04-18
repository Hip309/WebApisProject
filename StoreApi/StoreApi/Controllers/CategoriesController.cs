using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApi.Entities;
using StoreApi.Models.RequestModels;
using StoreApi.Models.RspondModels;
using StoreApi.Services.Interfaces;

namespace StoreApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public IActionResult GetCategoryList(int pageIndex, int pageSize)
        {
            if (pageSize <= 0 || pageIndex <= 0)
            {
                pageIndex = 1;
                pageSize = 10;
            }
            var categoryList = _categoryService.GetAllCategories(pageIndex, pageSize);
            if (categoryList == null)
            {
                return BadRequest();
            }
            return Ok(categoryList);
        }

        // GET: api/Categories/5
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var categoryRespond = await _categoryService.GetCategoryById(categoryId);
            if (categoryRespond.StatusCode == 200)
            {
                return StatusCode(StatusCodes.Status200OK, categoryRespond);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, categoryRespond);
            }
        }

        // PUT: api/Categories/5
        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int categoryId, [FromBody] CategoryRequest categoryRequest)
        {
            CategoryRespond categoryRespond = new CategoryRespond();
            if (categoryId != categoryRequest.Id)
            {
                return StatusCode(StatusCodes.Status400BadRequest, categoryRespond);
            }
            else
            {
                categoryRespond = await _categoryService.UpdateCategory(categoryRequest);
                if (categoryRespond.StatusCode == 200)
                {
                    return StatusCode(StatusCodes.Status200OK, categoryRespond);
                }
                else if (categoryRespond.StatusCode == 400)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, categoryRespond);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, categoryRespond);
                }
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryRequest categoryRequest)
        {
            var categoryRespond = await _categoryService.CreateCategory(categoryRequest);
            if (categoryRespond.StatusCode == 200)
            {
                return StatusCode(StatusCodes.Status200OK, categoryRespond);
            }
            else if (categoryRespond.StatusCode == 400)
            {
                return StatusCode(StatusCodes.Status400BadRequest, categoryRespond);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, categoryRespond);
            }
        }

        // DELETE: api/Categories/5
        [HttpDelete("{categoryId}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var categoryRespond = await _categoryService.DeleteCategory(categoryId);
            if (categoryRespond.StatusCode == 200)
            {
                return StatusCode(StatusCodes.Status200OK, categoryRespond);
            }
            else if (categoryRespond.StatusCode == 404)
            {
                return StatusCode(StatusCodes.Status404NotFound, categoryRespond);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, categoryRespond);
            }
        }
    }
}
