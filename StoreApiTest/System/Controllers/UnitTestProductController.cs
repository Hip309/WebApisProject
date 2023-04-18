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
    public class UnitTestProductController
    {
        private readonly Mock<IProductService> _productService;
        public UnitTestProductController()
        {
            _productService = new Mock<IProductService>();
        }

        //GetProductList method test
        [Fact]
        public void GetProductList_Return200OKStatus()
        {
            //Arrange
            List<ProductRespond> productList = ProductMockData.GetProductsData();
            ProductRespondList productRespondList = new ProductRespondList();
            productRespondList.RespondList = productList;
            _productService.Setup(p => p.GetAllProducts(1, 10)).Returns(productRespondList);
            var productController = new ProductsController(_productService.Object);
            //Act
            var result = (OkObjectResult)productController.GetProductList(1, 10);
            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public void GetProductList_Return204NoContentStatus()
        {
            //Arrange
            ProductRespondList productRespondList = new ProductRespondList();
            _productService.Setup(p => p.GetAllProducts(1, 10)).Returns(productRespondList);
            var productController = new ProductsController(_productService.Object);
            //Act
            var result = (NoContentResult)productController.GetProductList(1, 10);
            //Assert
            result.StatusCode.Should().Be(204);
            _productService.Verify(_ => _.GetAllProducts(1, 10), Times.Exactly(1));
        }

        //GetProductsByCategoryId method test
        [Fact]
        public void GetProductsByCategoryId_Return200OKStatus()
        {
            //Arrange
            List<ProductRespond> productList = ProductMockData.GetProductsOfCategory();
            ProductRespondList productRespondList = new ProductRespondList();
            productRespondList.RespondList = productList;
            _productService.Setup(p => p.GetProductListByCategoryId(1,1, 10)).Returns(productRespondList);
            var productController = new ProductsController(_productService.Object);
            //Act
            var result = (OkObjectResult)productController.GetProductsByCategoryId(1,1, 10);
            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public void GetProductsByCategoryId_Return204NoContentStatus()
        {
            //Arrange
            ProductRespondList productRespondList = new ProductRespondList();
            _productService.Setup(p => p.GetProductListByCategoryId(5, 1, 10)).Returns(productRespondList);
            var productController = new ProductsController(_productService.Object);
            //Act
            var result = (NoContentResult)productController.GetProductsByCategoryId(5, 1, 10);
            //Assert
            result.StatusCode.Should().Be(204);
            _productService.Verify(_ => _.GetProductListByCategoryId(5, 1, 10), Times.Exactly(1));
        }

        //GetProductsByCategoryId method test
        [Fact]
        public void GetProductsBySearchString_Return200OKStatus()
        {
            //Arrange
            List<ProductRespond> productList = ProductMockData.GetProductsOfCategory();
            ProductRespondList ProductRespondList = new ProductRespondList();
            ProductRespondList.RespondList = productList;
            _productService.Setup(p => p.GetProductsBySearchString("Milk", 1, 10)).Returns(ProductRespondList);
            var productController = new ProductsController(_productService.Object);
            //Act
            var result = (OkObjectResult)productController.GetProductsBySearchString("Milk", 1, 10);
            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public void GetProductsBySearchString_Return204NoContentStatus()
        {
            //Arrange
            ProductRespondList productRespondList = new ProductRespondList();
            _productService.Setup(p => p.GetProductsBySearchString("food", 1, 10)).Returns(productRespondList);
            var productController = new ProductsController(_productService.Object);
            //Act
            var result = (NoContentResult)productController.GetProductsBySearchString("food", 1, 10);
            //Assert
            result.StatusCode.Should().Be(204);
            _productService.Verify(_ => _.GetProductsBySearchString("food", 1, 10), Times.Exactly(1));
        }

        //GetProductById method test
        [Fact]
        public void GetProductById_Return200OKStatus()
        {
            //Arrange
            var testGuid = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f");
            ProductRespond pRespond = ProductMockData.GetProductRespond();
            pRespond.StatusCode = 200;
            pRespond.ResponseMessage = "Get product success";
            _productService.Setup(u => u.GetProductById(testGuid)).ReturnsAsync(pRespond);
            var productController = new ProductsController(_productService.Object);

            //Act 
            var result = (ObjectResult)productController.GetProductById(testGuid).Result;

            //Assert 
            result.StatusCode.Should().Be(200);
            _productService.Verify(_ => _.GetProductById(testGuid), Times.Exactly(1));
            Assert.Equal(testGuid, pRespond.Id);
            Assert.True(testGuid == pRespond.Id);
        }

        [Fact]
        public void GetProductById_Return400NotFoundStatus()
        {
            //Arrange
            var testGuid = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7d");
            ProductRespond pRespond = new ProductRespond();
            pRespond.StatusCode = 404;
            pRespond.ResponseMessage = "Product is not existed";
            _productService.Setup(u => u.GetProductById(testGuid)).ReturnsAsync(pRespond);
            var productController = new ProductsController(_productService.Object);

            //Act 
            var result = (ObjectResult)productController.GetProductById(testGuid).Result;

            //Assert 
            result.StatusCode.Should().Be(404);
            _productService.Verify(_ => _.GetProductById(testGuid), Times.Exactly(1));
        }

        //CreateProduct method test
        [Fact]
        public void CreateProduct_Return200OKStatus()
        {
            //Arrange
            ProductRequest pRequest = ProductMockData.GetProductRequest();
            ProductRespond pRespond = new ProductRespond();
            pRespond.Id = pRequest.Id;
            pRespond.Name = pRequest.Name;
            pRequest.Unit = pRequest.Unit;
            pRespond.Price= pRequest.Price;
            pRespond.Cost= pRequest.Cost;
            pRespond.Quantity= pRequest.Quantity;
            pRespond.Image = pRequest.Image;
            pRespond.CategoryId= pRequest.CategoryId;
            pRespond.StatusCode = 200;
            pRespond.ResponseMessage = "Create new product success";
            _productService.Setup(p => p.CreateProduct(pRequest)).ReturnsAsync(pRespond);
            var productController = new ProductsController(_productService.Object);

            //Act 
            var result = (ObjectResult)productController.CreateProduct(pRequest).Result;

            //Assert 
            result.StatusCode.Should().Be(200);
            _productService.Verify(_ => _.CreateProduct(pRequest), Times.Exactly(1));
            Assert.Equal(pRequest.Id, pRespond.Id);
            Assert.True(pRequest.Id == pRespond.Id);
        }

        [Fact]
        public void CreateProduct_Return400BadRequestStatus()
        {
            //Arrange
            ProductRequest pRequest = null!;
            ProductRespond pRespond = new ProductRespond();
            pRespond.StatusCode = 400;
            pRespond.ResponseMessage = "Request is wrong";
            _productService.Setup(p => p.CreateProduct(pRequest)).ReturnsAsync(pRespond);
            var productController = new ProductsController(_productService.Object);

            //Act 
            var result = (ObjectResult)productController.CreateProduct(pRequest).Result;

            //Assert 
            result.StatusCode.Should().Be(400);
            _productService.Verify(_ => _.CreateProduct(pRequest), Times.Exactly(1));
        }

        //DeleteProduct method test
        [Fact]
        public void DeleteProduct_Return200OKStatus()
        {
            //Arrange
            var testGuid = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f");
            ProductRespond pRespond = ProductMockData.GetProductRespond();
            pRespond.StatusCode = 200;
            pRespond.ResponseMessage = "Delete product success";
            _productService.Setup(p => p.DeleteProduct(testGuid)).ReturnsAsync(pRespond);
            var productController = new ProductsController(_productService.Object);

            //Act 
            var result = (ObjectResult)productController.DeleteProduct(testGuid).Result;

            //Assert 
            result.StatusCode.Should().Be(200);
            _productService.Verify(_ => _.DeleteProduct(testGuid), Times.Exactly(1));
            Assert.NotNull(result);
            Assert.Equal(testGuid, pRespond.Id);
            Assert.True(pRespond.Id == testGuid);
        }

        [Fact]
        public void DeleteProduct_Return400BadRequestStatus()
        {
            //Arrange 
            var testGuid = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7d");
            ProductRespond pRespond = new ProductRespond();
            pRespond.StatusCode = 404;
            pRespond.ResponseMessage = "Product is not exist";
            _productService.Setup(p => p.DeleteProduct(testGuid)).ReturnsAsync(pRespond);
            var productController = new ProductsController(_productService.Object);

            //Act 
            var result = (ObjectResult)productController.DeleteProduct(testGuid).Result;

            //Assert 
            result.StatusCode.Should().Be(404);
            _productService.Verify(_ => _.DeleteProduct(testGuid), Times.Exactly(1));
        }

        //UpdateProduct method test
        [Fact]
        public void UpdateProduct_Return200OKStatus()
        {
            //Arrange
            var testGuid = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f");
            ProductRequest pRequest = ProductMockData.GetProductRequest();
            pRequest.Id = testGuid;
            ProductRespond pRespond = new ProductRespond();
            pRespond.Id= pRequest.Id;
            pRespond.Name = pRequest.Name;
            pRequest.Unit = pRequest.Unit;
            pRespond.Price = pRequest.Price;
            pRespond.Cost = pRequest.Cost;
            pRespond.Quantity = pRequest.Quantity;
            pRespond.Image = pRequest.Image;
            pRespond.CategoryId = pRequest.CategoryId;
            pRespond.StatusCode = 200;
            pRespond.ResponseMessage = "Update product success";
            _productService.Setup(p => p.UpdateProduct(pRequest)).ReturnsAsync(pRespond);
            var productController = new ProductsController(_productService.Object);

            //Act 
            var result = (ObjectResult)productController.UpdateProduct(testGuid, pRequest).Result;

            //Assert 
            result.StatusCode.Should().Be(200);
            _productService.Verify(_ => _.UpdateProduct(pRequest), Times.Exactly(1));
            Assert.NotNull(result);
            Assert.Equal(testGuid, pRespond.Id);
            Assert.True(pRespond.Id == testGuid);
        }

        [Fact]
        public void UpdateProduct_Return400BadRequestStatus_DonotCallProductService()
        {
            //Arrange
            var testGuid = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7d");
            ProductRequest pRequest = ProductMockData.GetProductRequest();
            var productController = new ProductsController(_productService.Object);
            //Act
            var result = (BadRequestObjectResult)productController.UpdateProduct(testGuid, pRequest).Result;
            //Assert
            result.StatusCode.Should().Be(400);
        }
    }
}
