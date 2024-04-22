using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationDbContext _context;

        public JobRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Job>> GetAllAsync() => await _context.Jobs.ToListAsync();

        public async Task<Job> GetByIdAsync(int id) => await _context.Jobs.FindAsync(id);

        public async Task AddAsync(Job job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Job job)
        {
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job != null)
            {
                _context.Jobs.Remove(job);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Job>> GetJobsByKeywordAsync(string keyword)
        {
            return await _context.Jobs
                .Where(job => job.JobKeywords.Any(jk => jk.Keyword.KName.Contains(keyword)))
                .ToListAsync();
        }

        public async Task<List<Job>> GetJobsByIndustryAsync(string industry)
        {
            return await _context.Jobs
                .Where(job => job.JobIndustries.Any(ji => ji.Industry.IName.Contains(industry)))
                .ToListAsync();
        }

        public async Task<List<Job>> GetJobsByTitleAsync(string title)
        {
            return await _context.Jobs
                .Where(job => job.Title.Contains(title))
                .ToListAsync();
        }
    }
}
