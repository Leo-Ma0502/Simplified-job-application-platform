using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<JobIndustry> JobIndustries { get; set; }
        public DbSet<JobKeyword> JobKeywords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JobIndustry>()
                .HasKey(ji => new { ji.JId, ji.IId });

            modelBuilder.Entity<JobKeyword>()
                .HasKey(jk => new { jk.JId, jk.KId });
        }


    }
}
