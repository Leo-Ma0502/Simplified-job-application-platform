using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Services;
using Xunit;

namespace YourProjectName.Tests
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
    }
}
