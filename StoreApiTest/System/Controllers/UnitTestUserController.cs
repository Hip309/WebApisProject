using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StoreApi.Controllers;
using StoreApi.Models.RequestModels;
using StoreApi.Models.RspondModels;
using StoreApi.Services.Interfaces;
using StoreApi_Test.MockData;

namespace StoreApi_Test.System.Controllers
{
    public class UnitTestUserController
    {
        private readonly Mock<IUserService> _userService;
        public UnitTestUserController() 
        { 
            _userService = new Mock<IUserService>();
        }

        //GetUserList method test
        [Fact]
        public void GetUserList_Return200OKStatus()
        {
            //Arrange
            List<UserRespond> userList = UserMockData.GetUsersData();
            UserRespondList userRespondList = new UserRespondList();
            userRespondList.RespondList = userList;
            _userService.Setup(u => u.GetAllUsers(1, 10)).Returns(userRespondList);
            var userController = new UsersController(_userService.Object);
            //Act
            var result = (OkObjectResult)userController.GetUserList(1, 10);
            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public void GetUserList_Return204NoContentStatus()
        {
            //Arrange
            UserRespondList userRespondList = new UserRespondList();
            _userService.Setup(u => u.GetAllUsers(1, 10)).Returns(userRespondList);
            var userController = new UsersController(_userService.Object);
            //Act
            var result = (NoContentResult)userController.GetUserList(1, 10);
            //Assert
            result.StatusCode.Should().Be(204);
            _userService.Verify(_ => _.GetAllUsers(1,10), Times.Exactly(1)) ;
        }

        //GetUserById method test
        [Fact]
        public void GetUserById_Return200OKStatus()
        {
            //Arrange
            UserRespond uRespond = UserMockData.GetUserRespond();
            uRespond.StatusCode = 200;
            uRespond.ResponseMessage = "Get user success";
            _userService.Setup(u => u.GetUserById(1)).ReturnsAsync(uRespond);
            var userController = new UsersController(_userService.Object);

            //Act 
            var result = (ObjectResult)userController.GetUserById(1).Result;

            //Assert 
            result.StatusCode.Should().Be(200);
            _userService.Verify(_ => _.GetUserById(1), Times.Exactly(1));
            Assert.NotNull(result);
            Assert.Equal(1, uRespond.Id);
            Assert.True(uRespond.Id == 1);
        }

        [Fact]
        public void GetUserById_Return400NotFoundStatus()
        {
            //Arrange
            UserRespond uRespond = new UserRespond();
            uRespond.StatusCode = 404;
            uRespond.ResponseMessage = "User is not exist";
            _userService.Setup(u => u.GetUserById(5)).ReturnsAsync(uRespond);
            var userController = new UsersController(_userService.Object);

            //Act 
            var result = (ObjectResult)userController.GetUserById(5).Result;

            //Assert 
            result.StatusCode.Should().Be(404);
            _userService.Verify(_ => _.GetUserById(5), Times.Exactly(1));
        }


        //CreateUser method test
        [Fact]
        public void CreateUser_Return200OKStatus()
        {
            //Arrange
            UserRequest uRequest = UserMockData.GetUserRequest();
            UserRespond uRespond = new UserRespond();
            uRespond.Id = 5;
            uRespond.Name = uRequest.Name;
            uRespond.Email = uRequest.Email;
            uRespond.PhoneNumber = uRequest.PhoneNumber;
            uRespond.StatusCode = 200;
            uRespond.ResponseMessage = "Create new user success";
            _userService.Setup(u => u.CreateUser(uRequest)).ReturnsAsync(uRespond);
            var userController = new UsersController(_userService.Object);

            //Act 
            var result = (ObjectResult)userController.CreateUser(uRequest).Result;

            //Assert 
            result.StatusCode.Should().Be(200);
            _userService.Verify(_ => _.CreateUser(uRequest), Times.Exactly(1));
        }

        [Fact]
        public void CreateUser_Return400BadRequestStatus()
        {
            //Arrange
            UserRequest uRequest = null;
            UserRespond uRespond = new UserRespond();
            uRespond.StatusCode = 400;
            uRespond.ResponseMessage = "Request is wrong";
            _userService.Setup(u => u.CreateUser(uRequest)).ReturnsAsync(uRespond);
            var userController = new UsersController(_userService.Object);

            //Act 
            var result = (ObjectResult)userController.CreateUser(uRequest).Result;

            //Assert 
            result.StatusCode.Should().Be(400);
            _userService.Verify(_ => _.CreateUser(uRequest), Times.Exactly(1));
        }


        //DeleteUser method test
        [Fact]
        public void DeleteUser_Return200OKStatus()
        {
            //Arrange
            UserRespond uRespond = UserMockData.GetUserRespond();
            uRespond.StatusCode = 200;
            uRespond.ResponseMessage = "Delete user success";
            _userService.Setup(u => u.DeleteUser(1)).ReturnsAsync(uRespond);
            var userController = new UsersController(_userService.Object);

            //Act 
            var result = (ObjectResult)userController.DeleteUser(1).Result;

            //Assert 
            result.StatusCode.Should().Be(200);
            _userService.Verify(_ => _.DeleteUser(1), Times.Exactly(1));
            Assert.NotNull(result);
            Assert.Equal(1, uRespond.Id);
            Assert.True(uRespond.Id == 1);
        }

        [Fact]
        public void DeleteUser_Return400NotFoundStatus()
        {
            //Arrange
            UserRespond uRespond = UserMockData.GetUserRespond();
            uRespond.StatusCode = 404;
            uRespond.ResponseMessage = "User is not exist";
            _userService.Setup(u => u.DeleteUser(-1)).ReturnsAsync(uRespond);
            var userController = new UsersController(_userService.Object);

            //Act 
            var result = (ObjectResult)userController.DeleteUser(-1).Result;

            //Assert 
            result.StatusCode.Should().Be(404);
            _userService.Verify(_ => _.DeleteUser(-1), Times.Exactly(1));
        }

        //UpdateUser method test
        [Fact]
        public void UpdateUser_Return200OKStatus()
        {
            //Arrange
            UserRequest uRequest = UserMockData.GetUserRequest();
            uRequest.Id = 1;
            UserRespond uRespond = UserMockData.GetUserRespond();
            uRespond.Name = uRequest.Name;
            uRespond.PhoneNumber = uRequest.PhoneNumber;
            uRespond.Email = uRequest.Email;
            uRespond.StatusCode = 200;
            uRespond.ResponseMessage = "Update user success";
            _userService.Setup(u => u.UpdateUser(uRequest)).ReturnsAsync(uRespond);
            var userController = new UsersController(_userService.Object);

            //Act 
            var result = (ObjectResult)userController.UpdateUser(1,uRequest).Result;

            //Assert 
            result.StatusCode.Should().Be(200);
            _userService.Verify(_ => _.UpdateUser(uRequest), Times.Exactly(1));
            Assert.NotNull(result);
            Assert.Equal(1, uRespond.Id);
            Assert.True(uRespond.Id == 1);
        }

        [Fact]
        public void UpdateUser_Return400BadRequestStatus_DonotCallUserService()
        {
            //Arrange
            UserRequest uRequest = UserMockData.GetUserRequest();
            uRequest.Id = 1;
            var userController = new UsersController(_userService.Object);
            //Act
            var result = (BadRequestObjectResult)userController.UpdateUser(2, uRequest).Result;
            //Assert
            result.StatusCode.Should().Be(400);
        }
    }
    
}