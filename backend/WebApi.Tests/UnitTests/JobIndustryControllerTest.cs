using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Services;
using Xunit;

namespace WebApi.Tests.Unit
{
    public class JobIndustryControllerTests
    {
        private readonly Mock<IJobIndustryService> _mockJobIndustryService;
        private readonly JobIndustryController _controller;

        public JobIndustryControllerTests()
        {
            _mockJobIndustryService = new Mock<IJobIndustryService>();
            _controller = new JobIndustryController(_mockJobIndustryService.Object);
        }

        [Fact]
        public async Task GetIndustries_WhenIndustryExists_ReturnsOkObjectResultWithIndustrys()
        {
            // Arrange
            int jobId = 1;
            var expectedIndustrys = new List<Industry>
            {
                new Industry { IId = 1, IName = "Industry1" },
                new Industry { IId = 2, IName = "Industry2" }
            };
            _mockJobIndustryService.Setup(x => x.GetIndustryByJobId(jobId)).ReturnsAsync(expectedIndustrys);

            // Act
            var result = await _controller.GetIndustries(jobId);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedIndustrys = Assert.IsAssignableFrom<List<Industry>>(okResult.Value);
            Assert.Equal(expectedIndustrys, returnedIndustrys);
        }

        [Fact]
        public async Task GetIndustries_WhenJobDoesNotExist_ReturnsNotFoundResult()
        {
            // Arrange
            int jobId = 999; // Non-existent job ID
            _mockJobIndustryService.Setup(x => x.GetIndustryByJobId(jobId)).ReturnsAsync((List<Industry>)null);

            // Act
            var result = await _controller.GetIndustries(jobId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
