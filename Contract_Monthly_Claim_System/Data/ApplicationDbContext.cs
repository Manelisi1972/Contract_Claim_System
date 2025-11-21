
using Contract_Monthly_Claim_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Contract_Monthly_Claim_System.Data
{
    public class ApplicationDbContext :  DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : base(opts) { }

        public DbSet<Claim> Claims { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public int Lecterers { get; internal set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);
            mb.Entity<Claim>().Property(c => c.Status).HasConversion<string>().HasMaxLength(50);
        }
    }
}
