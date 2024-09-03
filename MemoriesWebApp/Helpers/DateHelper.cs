namespace MemoriesWebApp.Helpers
{
    public class DateHelper
    {
        public static string ConvertDaysToYearsMonthsDays(int days, bool shortFormat = false)
        {
            int years = days / 365;
            days %= 365;
            int months = days / 30;
            days %= 30;

            string result = "";

            if (years > 0)
            {
                result += $"{years} {Inflections.GetPolishInflection(years, "year", shortFormat)}";
            }

            if (months > 0)
            {
                if (result != "") result += ", ";
                result += $"{months} {Inflections.GetPolishInflection(months, "month", shortFormat)}";
            }

            if (days > 0)
            {
                if (result != "") result += ", ";
                result += $"{days} {Inflections.GetPolishInflection(days, "day", shortFormat)}";
            }

            return result;
        }
        public static string FormatMeetingTime(TimeSpan duration, bool isMobileDevice)
        {
            int totalDays = (int)Math.Floor(duration.TotalDays);
            int remainingHours = (int)(duration.TotalHours - totalDays * 24);

            if (isMobileDevice) 
                return $"{totalDays} d {remainingHours} h";
            else
                return $"{totalDays} {Inflections.GetPolishInflection(totalDays, "day")} {remainingHours} {Inflections.GetPolishInflection(remainingHours, "hour")}";
        }
    }
}
