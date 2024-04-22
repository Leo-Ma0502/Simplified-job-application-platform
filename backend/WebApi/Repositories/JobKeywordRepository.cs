using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class JobKeywordRepository : IJobKeywordRepository
    {
        private readonly ApplicationDbContext _context;

        public JobKeywordRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Keyword>> GetKeywordByJobIdAsync(int jobId)
        {
            return await _context.JobKeywords
                .Where(jk => jk.JId == jobId)
                .Select(jk => jk.Keyword)
                .ToListAsync();
        }
    }
}
