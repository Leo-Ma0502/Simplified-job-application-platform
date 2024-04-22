using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IJobKeywordRepository
    {
        Task<List<Keyword>> GetKeywordByJobIdAsync(int jobId);
    }
}