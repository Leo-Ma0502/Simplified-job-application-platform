using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Industry
    {
        [Key]
        public int IId { get; set; }
        public string IName { get; set; }
        public virtual ICollection<JobIndustry> JobIndustries { get; set; } = new List<JobIndustry>();
    }

}
