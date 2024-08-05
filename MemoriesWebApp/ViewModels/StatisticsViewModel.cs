using MemoriesWebApp.Models;

namespace MemoriesWebApp.ViewModels
{
    public class StatisticsViewModel
    {
        public decimal AvgMeetingTime { get; set; }
        public decimal AvgPicturesTaken { get; set; }
        public int MeetingMostPictures { get; set; }

        public List<Meeting> Meetings { get; set; }
        public List<MeetingTimeSpent> TimeSpent { get; set; }
        public List<Image> Images { get; set; }
        public List<PicturesTakenPerMeeting> PicturesTakenPerMeeting { get; set; }
    }
    public class MeetingTimeSpent
    {
        public int MeetingId { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
    }

    public class PicturesTakenPerMeeting
    {
        public int MeetingId { get; set; }
        public int PicturesTaken { get; set; }
    }
}