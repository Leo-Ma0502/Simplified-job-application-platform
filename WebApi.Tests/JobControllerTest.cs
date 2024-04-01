using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Services;
using Xunit;

namespace WebApi.Tests
{
    public class JobControllerTests
    {
        private readonly Mock<IJobService> _mockJobService;
        private readonly JobController _controller;

        public JobControllerTests()
        {
            _mockJobService = new Mock<IJobService>();
            _controller = new JobController(_mockJobService.Object);
        }

        [Fact]
        public async Task GetJob_WhenJobExists_ReturnsOkObjectResultWithJob()
        {
            // Arrange
            var jobId = 1;
            var mockJob = new Job { JId = jobId, Title = "Software Developer" };
            _mockJobService.Setup(s => s.GetJobByIdAsync(jobId)).ReturnsAsync(mockJob);

            // Act
            var result = await _controller.GetJob(jobId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Job>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedJob = Assert.IsType<Job>(okResult.Value);
            Assert.Equal(jobId, returnedJob.JId);
            Assert.Equal("Software Developer", returnedJob.Title);
        }

        [Fact]
        public async Task GetJob_WhenJobDoesNotExist_ReturnsNotFoundResult()
        {
            // Arrange
            var jobId = 1;
            _mockJobService.Setup(s => s.GetJobByIdAsync(jobId)).ReturnsAsync((Job)null);

            // Act
            var result = await _controller.GetJob(jobId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Job>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
