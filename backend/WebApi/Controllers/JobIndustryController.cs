using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JobIndustryController : ControllerBase
    {
        private readonly IJobIndustryService _jobIndustryService;

        public JobIndustryController(IJobIndustryService jobIndustryService)
        {
            _jobIndustryService = jobIndustryService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Industry>>> GetIndustries(int id)
        {
            var industries = await _jobIndustryService.GetIndustryByJobId(id);
            return industries == null ? NotFound() : Ok(industries);
        }
    }
}
