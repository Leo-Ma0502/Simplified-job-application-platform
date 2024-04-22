using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IJobIndustryService
    {
        Task<List<Industry>> GetIndustryByJobId(int id);
    }
}
