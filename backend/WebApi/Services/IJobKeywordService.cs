using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IJobKeywordService
    {
        Task<List<Keyword>> GetKeywordByJobId(int id);
    }
}
