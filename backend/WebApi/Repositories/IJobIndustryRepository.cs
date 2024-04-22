using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IJobIndustryRepository
    {
        Task<List<Industry>> GetIndustryByJobIdAsync(int jobId);
    }
}