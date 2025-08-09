using Microsoft.AspNetCore.Identity;

namespace SleepSync.Models.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
