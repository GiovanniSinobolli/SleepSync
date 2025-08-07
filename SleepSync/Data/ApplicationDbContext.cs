using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SleepSync.Models;

namespace SleepSync.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<MoodInsight> MoodInsights { get; set; }

        public DbSet<TaskItem> TaskItems { get; set; }

    }
}
