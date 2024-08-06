using MemoriesWebApp.Data.Enum;
using MemoriesWebApp.Models;

namespace MemoriesWebApp.ViewModels
{
    public class StatisticsViewModel
    {
        public decimal AvgMeetingTime { get; set; }
        public decimal AvgPicturesTaken { get; set; }
        public int MeetingMostPictures { get; set; }

        public List<Meeting> Meetings { get; set; }
        public List<Image> Images { get; set; }
        public List<MeetingTimeSpent> TimeSpent { get; set; }
        public List<PicturesTakenPerMeeting> PicturesTakenPerMeeting { get; set; }
        public List<DayPassed> DaysPassed { get; set; }
    }
    public class MeetingTimeSpent
    {
        public int MeetingId { get; set; }
        public DateTime Date { get; set; }
        public MeetingCity MeetingCity { get; set; }
        public string ImageUrl { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
    }

    public class PicturesTakenPerMeeting
    {
        public int MeetingId { get; set; }
        public DateTime MeetingDate { get; set; }
        public MeetingCity MeetingCity { get; set; }
        public string ImageUrl { get; set; }
        public int PicturesTaken { get; set; }
    }
    public class DayPassed
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public int DaysPassed { get; set; }
    }
}