using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IJobRepository
    {
        Task<IEnumerable<Job>> GetAllAsync();
        Task<Job> GetByIdAsync(int id);
        Task AddAsync(Job job);
        Task UpdateAsync(Job job);
        Task DeleteAsync(int id);
        Task<List<Job>> GetJobsByKeywordAsync(string keywords);
        Task<List<Job>> GetJobsByIndustryAsync(string keywords);
        Task<List<Job>> GetJobsByTitleAsync(string keywords);
    }
}
