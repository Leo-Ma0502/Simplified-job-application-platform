using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.DTO;
using WebApi.Models;
using WebApi.Services;
using Xunit;

namespace WebApi.Tests.Unit
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
        public async Task Register_WithValidUser_ReturnsLoginSuccess()
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

        [Fact]
        public async Task Login_WithValidCredentials_SetsCookieAndReturnsOk()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockTokenService = new Mock<ITokenService>();
            var loginDto = new LoginDTO { Email = "valid@example.com", Password = "Password123" };
            var user = new User();

            mockUserService.Setup(service => service.AuthenticateAsync(loginDto.Email, loginDto.Password))
                           .ReturnsAsync(user);
            mockTokenService.Setup(service => service.GenerateToken(It.IsAny<User>()))
                            .ReturnsAsync("GeneratedToken123");

            var controllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            var controller = new UserController(mockUserService.Object, mockTokenService.Object)
            {
                ControllerContext = controllerContext
            };

            // Act
            var result = await controller.Login(loginDto);

            // Assert
            // Check that the result is an OkObjectResult with the expected message
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            var responseMessage = okResult.Value.GetType().GetProperty("Message").GetValue(okResult.Value, null);
            Assert.Equal("Login successful", responseMessage);
        }



        [Fact]
        public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockTokenService = new Mock<ITokenService>();
            var loginDto = new LoginDTO { Email = "invalid@example.com", Password = "WrongPassword" };

            mockUserService.Setup(service => service.AuthenticateAsync(loginDto.Email, loginDto.Password))
                           .ReturnsAsync((User)null);

            var controller = new UserController(mockUserService.Object, mockTokenService.Object);

            // Act
            var result = await controller.Login(loginDto);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

    }
}
