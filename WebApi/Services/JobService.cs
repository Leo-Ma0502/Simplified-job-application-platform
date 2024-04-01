using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Data.Repositories;
using WebApi.Models;

namespace WebApi.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<IEnumerable<Job>> GetAllJobsAsync() => await _jobRepository.GetAllAsync();

        public async Task<Job> GetJobByIdAsync(int id) => await _jobRepository.GetByIdAsync(id);

        public async Task CreateJobAsync(Job job) => await _jobRepository.AddAsync(job);

        public async Task UpdateJobAsync(Job job) => await _jobRepository.UpdateAsync(job);

        public async Task DeleteJobAsync(int id) => await _jobRepository.DeleteAsync(id);
    }
}
