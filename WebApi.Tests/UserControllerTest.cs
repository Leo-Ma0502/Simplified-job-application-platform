using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Services;
using Xunit;

namespace WebApi.Tests
{
    public class UserControllerTests
    {
        [Fact]
        public async Task Get_WhenUserExists_ReturnsOkResultWithUser()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            mockService.Setup(service => service.GetUserByIdAsync(1))
                       .ReturnsAsync(new User { UId = 1, FirstName = "TestF", LastName = "TestL" });

            var controller = new UserController(mockService.Object);

            // Act
            var result = await controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<User>(okResult.Value);
            Assert.Equal(1, returnValue.UId);
            Assert.Equal("TestF", returnValue.FirstName);
            Assert.Equal("TestL", returnValue.LastName);
        }

        [Fact]
        public async Task Get_WhenUserDoesNotExist_ReturnsNotFoundResult()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            mockService.Setup(service => service.GetUserByIdAsync(It.IsAny<int>()))
                       .ReturnsAsync((User)null);

            var controller = new UserController(mockService.Object);

            // Act
            var result = await controller.Get(999); // Assume this ID does not exist

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Get_WhenUserExists_ReturnsOkObjectResultWithUser()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var testUser = new User { UId = 1, Email = "user@example.com", FirstName = "Test", LastName = "User" };
            mockService.Setup(service => service.GetUserByIdAsync(1))
                       .ReturnsAsync(testUser);

            var controller = new UserController(mockService.Object);

            // Act
            var result = await controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal(testUser.UId, returnUser.UId);
            Assert.Equal(testUser.Email, returnUser.Email);
        }

        [Fact]
        public async Task Register_WithValidUser_ReturnsCreatedAtAction()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var newUser = new User { Email = "newuser@example.com", Password = "newPassword", FirstName = "New", LastName = "User" };
            mockService.Setup(service => service.RegisterAsync(It.IsAny<User>()))
                       .ReturnsAsync(newUser);

            var controller = new UserController(mockService.Object);

            // Act
            var result = await controller.Register(newUser);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("Get", createdAtActionResult.ActionName);
            Assert.Equal(newUser, createdAtActionResult.Value);
        }

        [Fact]
        public async Task Register_WhenEmailExists_ReturnsBadRequest()
        {
            // Arrange
            var mockService = new Mock<IUserService>();
            var newUser = new User { Email = "existinguser@example.com", Password = "Password123", FirstName = "Existing", LastName = "User" };
            mockService.Setup(service => service.Exists(newUser.Email)).ReturnsAsync(true);

            var controller = new UserController(mockService.Object);

            // Act
            var result = await controller.Register(newUser);

            // Assert
            var objectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);
            Assert.Equal("User already exists.", objectResult.Value);
        }
    }
}
