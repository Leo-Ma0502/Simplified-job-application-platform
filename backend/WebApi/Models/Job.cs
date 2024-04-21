using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Job
    {
        [Key]
        public int JId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public int Opening { get; set; }
        public string Contact { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public string Responsibilities { get; set; }
        public DateTime Postdate { get; set; }
        public DateTime Deadline { get; set; }
        public virtual ICollection<JobIndustry> JobIndustries { get; set; } = new List<JobIndustry>();
        public virtual ICollection<JobKeyword> JobKeywords { get; set; } = new List<JobKeyword>();
    }
}
