using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class JobIndustry
    {
        public int JId { get; set; }
        [ForeignKey("JId")]
        public Job Job { get; set; }

        public int IId { get; set; }
        [ForeignKey("IId")]
        public Industry Industry { get; set; }
    }

}
