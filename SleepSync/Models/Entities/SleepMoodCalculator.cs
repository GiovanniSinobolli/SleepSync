using static SleepSync.Models.Entities.SupportEnums;

namespace SleepSync.Models.Entities
{
    public static class SleepMoodCalculator
    {
        public static MoodLevel CalculateMoodFromSleep(int sleepHours)
        {
            return sleepHours switch
            {
                >= 0 and <= 3 => MoodLevel.Terrible,      // Poor mood
                >= 4 and <= 6 => MoodLevel.Neutral,   // Neutral mood  
                >= 7 => MoodLevel.Happy,              // Good mood
                _ => MoodLevel.Neutral                 // Default fallback
            };
        }
    }
}
