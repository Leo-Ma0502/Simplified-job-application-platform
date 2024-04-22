using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IJobService
    {
        Task<IEnumerable<Job>> GetAllJobsAsync();
        Task<IEnumerable<Job>> GetJobsAsync(int page, int pageSize);
        Task<Job> GetJobByIdAsync(int id);
        Task CreateJobAsync(Job job);
        Task UpdateJobAsync(Job job);
        Task DeleteJobAsync(int id);
        Task<List<Job>> SearchJobsAsync(int page, string keyword, string industry, string title, int pageSize);
    }
}
