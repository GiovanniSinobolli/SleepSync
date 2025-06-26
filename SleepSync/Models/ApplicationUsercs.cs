using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SleepSync.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<TaskItem> Tasks { get; set; }
        public DbSet<MoodInsight> MoodInsights { get; set; }
    }

}
