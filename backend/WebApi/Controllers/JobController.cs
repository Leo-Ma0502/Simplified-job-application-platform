using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs(int page, int pageSize = 10) => Ok(await _jobService.GetJobsAsync(page, pageSize));

        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await _jobService.GetJobByIdAsync(id);
            return job == null ? NotFound() : Ok(job);
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob([FromBody] Job job)
        {
            await _jobService.CreateJobAsync(job);
            return CreatedAtAction(nameof(GetJob), new { id = job.JId }, job);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(int id, [FromBody] Job job)
        {
            if (id != job.JId)
            {
                return BadRequest();
            }

            await _jobService.UpdateJobAsync(job);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _jobService.GetJobByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            await _jobService.DeleteJobAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchJobs([FromQuery] int page, [FromQuery] string keyword = null, [FromQuery] string industry = null, [FromQuery] string title = null, int pageSize = 10)
        {
            var job = await _jobService.SearchJobsAsync(page, keyword, industry, title, pageSize);
            return Ok(job);
        }
    }
}
