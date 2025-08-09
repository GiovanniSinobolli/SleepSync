using Microsoft.AspNetCore.Identity;
using static SleepSync.Models.Entities.SupportEnums;

namespace SleepSync.Models.Entities
{
    public class UserTask
    {
        public int Id { get; set; }
        public string UserId { get; set; } // Foreign key to User
        public string Title { get; set; } // "task" column
        public string Description { get; set; } // "task description" column
        public Priority Priority { get; set; } // "priority" column
        public bool IsCompleted { get; set; } // "iscompleted" column
        public MoodLevel? Mood { get; set; } // "mood" column (nullable since user might not set it initially)
        public DateTime? CompletedAt { get; set; }

    }
}
