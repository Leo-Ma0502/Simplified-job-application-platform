using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class User
    {
        [Key]
        public int UId { get; set; }
        public string Email { get; set; }
        public string EncryptedPwd { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
