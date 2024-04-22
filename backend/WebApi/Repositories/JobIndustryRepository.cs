using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class JobIndustryRepository : IJobIndustryRepository
    {
        private readonly ApplicationDbContext _context;

        public JobIndustryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Industry>> GetIndustryByJobIdAsync(int jobId)
        {
            return await _context.JobIndustries
                .Where(ji => ji.JId == jobId)
                .Select(ji => ji.Industry)
                .ToListAsync();
        }
    }
}
