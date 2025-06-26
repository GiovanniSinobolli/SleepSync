using System.ComponentModel.DataAnnotations;

namespace SleepSync.Models
{
    // Gio - represents a task item in the user's task list
    public class TaskItem
    {
        public int TaskItemId { get; set; } // PK

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; } // Date planned or completed

        public bool IsCompleted { get; set; }

        // current mood tied to the task
        public MoodState? Mood { get; set; }

        // Foreign Key to User
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }

}
