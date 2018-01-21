using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace dojoQA.Models
{
    public class DojOverflowContext : IdentityDbContext<ApplicationUser>
    {
        public DojOverflowContext(DbContextOptions<DojOverflowContext> options) : base(options) { }

        DbSet<ApplicationUser> ApplicationUsers { get; set; }
        DbSet<Question> Questions { get; set; }
        DbSet<Answer> Answers { get; set; }
    }
}