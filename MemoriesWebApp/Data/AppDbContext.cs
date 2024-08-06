using MemoriesWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MemoriesWebApp.ViewModels;
using MemoriesWebApp.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Data;

namespace MemoriesWebApp.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImportantDate> ImportantDates { get; set; }
        public StatisticsViewModel GetStatistics()
        {
            var statistics = new StatisticsViewModel();

            using (var connection = this.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "GetStatistics";
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var reader = command.ExecuteReader())
                    {
                        // LongestMeeting, FirstMeeting, LatestMeeting
                        var meetings = new List<Meeting>();
                        while (reader.Read())
                        {
                            meetings.Add(new Meeting
                            {
                                Id = reader.GetInt32(0),
                                DateStart = reader.GetDateTime(1),
                                Realized = reader.GetBoolean(2),
                                DateEnd = reader.GetDateTime(3),
                                MeetingCity = (MeetingCity)reader.GetInt32(4),
                                ImageUrl = reader.IsDBNull(5) ? null : reader.GetString(5),
                                AppUserId = reader.IsDBNull(6) ? null : reader.GetString(6)
                            });
                        }
                        reader.NextResult();

                        // Time Spent in Meetings
                        var timeSpent = new List<MeetingTimeSpent>();
                        while (reader.Read())
                        {
                            timeSpent.Add(new MeetingTimeSpent
                            {
                                MeetingId = reader.GetInt32(0),
                                Date = reader.GetDateTime(1),
                                MeetingCity = (MeetingCity)reader.GetInt32(2),
                                ImageUrl = reader.GetString(3),
                                Days = reader.GetInt32(4),
                                Hours = reader.GetInt32(5),
                            });
                        }
                        reader.NextResult();

                        // Process FirstImage, LatestImage
                        var images = new List<Image>();
                        while (reader.Read())
                        {
                            images.Add(new Image
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                                Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Url = reader.GetString(3),
                                MeetingId = reader.GetInt32(4),
                                City = reader.IsDBNull(5) ? null : reader.GetString(5),
                                Date = DateOnly.FromDateTime(reader.GetDateTime(6)),  // Convert DateTime to DateOnly
                                AppUserId = reader.IsDBNull(7) ? null : reader.GetString(7),
                                IsVisableForUser = reader.GetBoolean(8)
                            });
                        }
                        reader.NextResult();

                        // Pictures taken per meeting
                        var picturesTaken = new List<PicturesTakenPerMeeting>();
                        while (reader.Read())
                        {
                            picturesTaken.Add(new PicturesTakenPerMeeting
                            {
                                MeetingId = reader.GetInt32(0),
                                MeetingDate = reader.GetDateTime(1),
                                MeetingCity = (MeetingCity)reader.GetInt32(2),
                                ImageUrl = reader.GetString(3),
                                PicturesTaken = reader.GetInt32(4)
                            });
                        }
                        reader.NextResult();

                        // ImportantDates
                        var daysPassed = new List<DayPassed>();
                        while (reader.Read())
                        {
                            daysPassed.Add(new DayPassed
                            {
                                Title = reader.GetString(0),
                                Description = reader.IsDBNull(1) ? null : reader.GetString(1),
                                Date = reader.GetDateTime(2),
                                DaysPassed = reader.GetInt32(3)
                            });
                        }
                        reader.NextResult();

                        // Average Statistics
                        if (reader.Read())
                        {
                            statistics = new StatisticsViewModel
                            {
                                AvgMeetingTime = reader.GetDecimal(0),
                                AvgPicturesTaken = reader.GetDecimal(1),
                                MeetingMostPictures = reader.GetInt32(2),
                                Meetings = meetings,
                                TimeSpent = timeSpent,
                                Images = images,
                                PicturesTakenPerMeeting = picturesTaken,
                                DaysPassed = daysPassed
                            };
                        }
                    }
                }
            }
            return statistics;
        }
    }
}
