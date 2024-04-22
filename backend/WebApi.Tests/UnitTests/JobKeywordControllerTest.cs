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
    public class JobKeywordControllerTests
    {
        private readonly Mock<IJobKeywordService> _mockJobKeywordService;
        private readonly JobKeywordController _controller;

        public JobKeywordControllerTests()
        {
            _mockJobKeywordService = new Mock<IJobKeywordService>();
            _controller = new JobKeywordController(_mockJobKeywordService.Object);
        }

        [Fact]
        public async Task GetIndustries_WhenKeywordExists_ReturnsOkObjectResultWithKeywords()
        {
            // Arrange
            int jobId = 1;
            var expectedKeywords = new List<Keyword>
            {
                new Keyword { KId = 1, KName = "Keyword1" },
                new Keyword { KId = 2, KName = "Keyword2" }
            };
            _mockJobKeywordService.Setup(x => x.GetKeywordByJobId(jobId)).ReturnsAsync(expectedKeywords);

            // Act
            var result = await _controller.GetIndustries(jobId);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedKeywords = Assert.IsAssignableFrom<List<Keyword>>(okResult.Value);
            Assert.Equal(expectedKeywords, returnedKeywords);
        }

        [Fact]
        public async Task GetIndustries_WhenJobDoesNotExist_ReturnsNotFoundResult()
        {
            // Arrange
            int jobId = 999; // Non-existent job ID
            _mockJobKeywordService.Setup(x => x.GetKeywordByJobId(jobId)).ReturnsAsync((List<Keyword>)null);

            // Act
            var result = await _controller.GetIndustries(jobId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
