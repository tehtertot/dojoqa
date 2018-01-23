using System.ComponentModel.DataAnnotations;

namespace dojoQA.Models
{
    public class LoginUser
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}