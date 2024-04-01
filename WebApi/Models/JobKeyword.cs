using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class JobKeyword
    {
        public int JId { get; set; }
        [ForeignKey("JId")]
        public Job Job { get; set; }

        public int KId { get; set; }
        [ForeignKey("KId")]
        public Keyword Keyword { get; set; }
    }

}
