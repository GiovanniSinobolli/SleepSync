using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SleepSync.Models.Entities;

namespace SleepSync.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        //constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<SleepRecord> SleepRecords { get; set; }

       
    }
}
