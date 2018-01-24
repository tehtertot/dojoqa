using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dojoQA.Models
{
    [NotMapped]
    public class LoginUser
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}