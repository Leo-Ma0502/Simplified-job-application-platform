using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JobKeywordController : ControllerBase
    {
        private readonly IJobKeywordService _jobKeywordService;

        public JobKeywordController(IJobKeywordService jobKeywordService)
        {
            _jobKeywordService = jobKeywordService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Keyword>>> GetIndustries(int id)
        {
            var keywords = await _jobKeywordService.GetKeywordByJobId(id);
            return keywords == null ? NotFound() : Ok(keywords);
        }
    }
}
