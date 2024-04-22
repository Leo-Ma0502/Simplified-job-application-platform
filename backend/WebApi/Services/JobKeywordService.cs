using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Repositories;
using WebApi.Models;

namespace WebApi.Services
{
    public class JobKeywordService : IJobKeywordService
    {
        private readonly IJobKeywordRepository _jobKeywordRepository;

        public JobKeywordService(IJobKeywordRepository jobKeywordRepository)
        {
            _jobKeywordRepository = jobKeywordRepository;
        }

        public async Task<List<Keyword>> GetKeywordByJobId(int id)
        {
            return await _jobKeywordRepository.GetKeywordByJobIdAsync(id);
        }
    }
}
