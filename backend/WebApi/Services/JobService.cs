using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Repositories;
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

        public async Task<IEnumerable<Job>> GetJobsAsync(int page, int pageSize)
        {
            var _jobs = await GetAllJobsAsync();
            if (page < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(page), "Page number must be greater than or equal to 1.");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than or equal to 1.");
            }

            var jobs = _jobs.Skip((page - 1) * pageSize).Take(pageSize);
            return await Task.FromResult(jobs);
        }

        public async Task<Job> GetJobByIdAsync(int id) => await _jobRepository.GetByIdAsync(id);

        public async Task CreateJobAsync(Job job) => await _jobRepository.AddAsync(job);

        public async Task UpdateJobAsync(Job job) => await _jobRepository.UpdateAsync(job);

        public async Task DeleteJobAsync(int id) => await _jobRepository.DeleteAsync(id);

        public async Task<List<Job>> SearchJobsAsync(int page, string keyword, string industry, string title, int pageSize)
        {
            List<Job> result = new List<Job>();

            if (page < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(page), "Page number must be greater than or equal to 1.");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than or equal to 1.");
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                var jobsByKeyword = await _jobRepository.GetJobsByKeywordAsync(keyword);
                result.AddRange(jobsByKeyword);
            }

            if (!string.IsNullOrEmpty(industry))
            {
                var jobsByIndustry = await _jobRepository.GetJobsByIndustryAsync(industry);
                result.AddRange(jobsByIndustry);
            }

            if (!string.IsNullOrEmpty(title))
            {
                var jobsByTitle = await _jobRepository.GetJobsByTitleAsync(title);
                result.AddRange(jobsByTitle);
            }

            return result.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

    }
}
