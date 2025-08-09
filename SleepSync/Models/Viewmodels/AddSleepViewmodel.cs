using Microsoft.AspNetCore.Mvc;
using SleepSync.Models.Entities;
using static SleepSync.Models.Entities.SupportEnums;

namespace SleepSync.Models.Viewmodels
{
    public class AddSleepViewmodel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime BedTime { get; set; }
        public DateTime WakeTime { get; set; }
        public int SleepHours { get; set; } // Calculated or user input
        public MoodLevel AutoMood { get; set; } // Auto-generated based on sleep
        public string Notes { get; set; }
        public DateTime Date { get; set; }

    }
}
