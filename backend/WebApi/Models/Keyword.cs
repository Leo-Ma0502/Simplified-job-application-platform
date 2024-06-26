using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Keyword
    {
        [Key]
        public int KId { get; set; }
        public string KName { get; set; }
        public virtual ICollection<JobKeyword> JobKeywords { get; set; } = new List<JobKeyword>();
    }

}
