using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StoreApi.Controllers;
using StoreApi.Models.RequestModels;
using StoreApi.Models.RspondModels;
using StoreApi.Services;
using StoreApi.Services.Interfaces;
using StoreApi_Test.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApi_Test.System.Controllers
{
    public class UnitTestCategoryController
    {
        private readonly Mock<ICategoryService> _categoryService;

        public UnitTestCategoryController()
        {
            _categoryService = new Mock<ICategoryService>();
        }

        //GetCategoryList method test
        [Fact]
        public void GetCategoryList_Return200OKStatus()
        {
            //Arrange
            List<CategoryRespond> cList = CategoryMockData.GetCategoriesData();
            CategoryRespondList categoryRespondList = new CategoryRespondList();
            categoryRespondList.RespondList = cList;
            _categoryService.Setup(c => c.GetAllCategories(1, 10)).Returns(categoryRespondList);
            var categoryController = new CategoriesController(_categoryService.Object);
            //Act
            var result = (OkObjectResult)categoryController.GetCategoryList(1, 10);
            //Assert
            result.StatusCode.Should().Be(200);
            _categoryService.Verify(_ => _.GetAllCategories(1,10), Times.Exactly(1));
        }

        [Fact]
        public void GetCategoryList_Return204NoContentStatus()
        {
            //Arrange
            CategoryRespondList categoryRespondList = new CategoryRespondList();
            _categoryService.Setup(c => c.GetAllCategories(1, 10)).Returns(categoryRespondList);
            var categoryController = new CategoriesController(_categoryService.Object);
            //Act
            var result = (NoContentResult)categoryController.GetCategoryList(1, 10);
            //Assert
            result.StatusCode.Should().Be(204);
            _categoryService.Verify(_ => _.GetAllCategories(1, 10), Times.Exactly(1));
        }

        //GetCategoryById method test
        [Fact]
        public void GetCategoryById_Return200OKStatus()
        {
            //Arrange
            CategoryRespond cRespond = CategoryMockData.GetCategoryRespond();
            cRespond.StatusCode = 200;
            cRespond.ResponseMessage = "Get category success";
            _categoryService.Setup(c => c.GetCategoryById(1)).ReturnsAsync(cRespond);
            var categoryController = new CategoriesController(_categoryService.Object);

            //Act 
            var result = (ObjectResult)categoryController.GetCategoryById(1).Result;

            //Assert 
            result.StatusCode.Should().Be(200);
            _categoryService.Verify(_ => _.GetCategoryById(1), Times.Exactly(1));
        }

        [Fact]
        public void GetCategoryById_Returns404NotFoundStatus()
        {
            CategoryRespond cRespond = new CategoryRespond();
            cRespond.StatusCode = 404;
            cRespond.ResponseMessage = "Category is not exist";
            _categoryService.Setup(c => c.GetCategoryById(-1)).ReturnsAsync(cRespond);
            var categoryController = new CategoriesController(_categoryService.Object);

            //Act 
            var result = (ObjectResult)categoryController.GetCategoryById(-1).Result;

            //Assert 
            result.StatusCode.Should().Be(404);
            _categoryService.Verify(_ => _.GetCategoryById(-1), Times.Exactly(1));
        }

        //CreateCategory method test
        [Fact]
        public void CreateCategory_Return200OKStatus()
        {
            //Arrange
            CategoryRequest cRequest = CategoryMockData.GetCategoryRequest();
            CategoryRespond cRespond = new CategoryRespond();
            cRespond.Id = 4;
            cRespond.Name = cRequest.Name;
            cRespond.Image = cRequest.Image;
            cRespond.StatusCode = 200;
            cRespond.ResponseMessage = "Create new category success";
            _categoryService.Setup(u => u.CreateCategory(cRequest)).ReturnsAsync(cRespond);
            var categoryController = new CategoriesController(_categoryService.Object);

            //Act 
            var result = (ObjectResult)categoryController.CreateCategory(cRequest).Result;

            //Assert 
            result.StatusCode.Should().Be(200);
            _categoryService.Verify(_ => _.CreateCategory(cRequest), Times.Exactly(1));
        }

        [Fact]
        public void CreateCategory_Return400BadRequestStatus()
        {
            //Arrange
            CategoryRequest cRequest = null!;
            CategoryRespond cRespond = new CategoryRespond();
            cRespond.StatusCode = 400;
            cRespond.ResponseMessage = "Request is empty";
            _categoryService.Setup(u => u.CreateCategory(cRequest)).ReturnsAsync(cRespond);
            var categoryController = new CategoriesController(_categoryService.Object);

            //Act 
            var result = (ObjectResult)categoryController.CreateCategory(cRequest).Result;

            //Assert 
            result.StatusCode.Should().Be(400);
            _categoryService.Verify(_ => _.CreateCategory(cRequest), Times.Exactly(1));
        }

        //DeleteCategory method test
        [Fact]
        public void DeleteCategory_Return200OkStatus()
        {
            //Arrange
            CategoryRespond cRespond = CategoryMockData.GetCategoryRespond();
            cRespond.StatusCode = 200;
            cRespond.ResponseMessage = "Delete category success";
            _categoryService.Setup(c => c.DeleteCategory(1)).ReturnsAsync(cRespond);
            var categoryController = new CategoriesController(_categoryService.Object);

            //Act 
            var result = (ObjectResult)categoryController.DeleteCategory(1).Result;

            //Assert 
            result.StatusCode.Should().Be(200);
            _categoryService.Verify(_ => _.DeleteCategory(1), Times.Exactly(1));
        }

        [Fact]
        public void DeleteCategory_Return400BadRequestStatus()
        {
            //Arrange
            CategoryRespond cRespond = new CategoryRespond();
            cRespond.StatusCode = 400;
            cRespond.ResponseMessage = "Category is not exist";
            _categoryService.Setup(c => c.DeleteCategory(-1)).ReturnsAsync(cRespond);
            var categoryController = new CategoriesController(_categoryService.Object);

            //Act 
            var result = (ObjectResult)categoryController.DeleteCategory(-1).Result;

            //Assert 
            result.StatusCode.Should().Be(400);
            _categoryService.Verify(_ => _.DeleteCategory(-1), Times.Exactly(1));
        }

        //UpdateCategory method test
        [Fact]
        public void UpdateCategory_Return200OkStatus()
        {
            //Arrange
            CategoryRequest cRequest = CategoryMockData.GetCategoryRequest();
            cRequest.Id = 1;
            CategoryRespond cRespond = new CategoryRespond();
            cRespond.StatusCode = 200;
            cRespond.ResponseMessage = "Update user success";
            _categoryService.Setup(c => c.UpdateCategory(cRequest)).ReturnsAsync(cRespond);
            var categoryController = new CategoriesController(_categoryService.Object);

            //Act 
            var result = (ObjectResult)categoryController.UpdateCategory(1, cRequest).Result;

            //Assert 
            result.StatusCode.Should().Be(200);
            _categoryService.Verify(_ => _.UpdateCategory(cRequest), Times.Exactly(1));
        }

        [Fact]
        public void UpdateCategory_Return400BadRequestStatus_DonotCallCategoryService()
        {
            //Arrange
            CategoryRequest cRequest = CategoryMockData.GetCategoryRequest();
            cRequest.Id = 2;
            var categoryController = new CategoriesController(_categoryService.Object);

            //Act 
            var result = (ObjectResult)categoryController.UpdateCategory(1, cRequest).Result;

            //Assert 
            result.StatusCode.Should().Be(400);
        }
    }
}
