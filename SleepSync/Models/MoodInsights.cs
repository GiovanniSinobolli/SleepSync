namespace SleepSync.Models
{
    public class MoodInsight
    {
        public int MoodInsightId { get; set; }  // primary key

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
