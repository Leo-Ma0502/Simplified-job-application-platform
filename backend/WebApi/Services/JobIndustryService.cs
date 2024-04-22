using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Repositories;
using WebApi.Models;

namespace WebApi.Services
{
    public class JobIndustryService : IJobIndustryService
    {
        private readonly IJobIndustryRepository _jobIndustryRepository;

        public JobIndustryService(IJobIndustryRepository jobIndustryRepository)
        {
            _jobIndustryRepository = jobIndustryRepository;
        }

        public async Task<List<Industry>> GetIndustryByJobId(int id)
        {
            return await _jobIndustryRepository.GetIndustryByJobIdAsync(id);
        }
    }
}
