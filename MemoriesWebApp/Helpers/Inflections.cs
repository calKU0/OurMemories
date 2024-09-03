namespace MemoriesWebApp.Helpers
{
    public class Inflections
    {
        public static string GetPolishInflection(int value, string unit, bool shortFormat = false)
        {
            if (shortFormat)
            {
                if (unit == "year")
                    return "y";
                else if (unit == "month")
                    return "m";
                else if (unit == "day")
                    return "d";
                else if (unit == "hour")
                    return "h";
            }

            if (unit == "year")
            {
                if (value == 1) return "rok";
                if (value >= 2 && value <= 4) return "lata";
                return "lat";
            }
            else if (unit == "month")
            {
                if (value == 1) return "miesiąc";
                if (value >= 2 && value <= 4) return "miesiące";
                return "miesięcy";
            }
            else if (unit == "day")
            {
                if (value == 1) return "dzień";
                if (value >= 2 && value <= 4) return "dni";
                return "dni";
            }
            else if (unit == "hour")
            {
                if (value == 1) return "godzina";
                if (value >= 2 && value <= 4) return "godziny";
                return "godzin";
            }

            return unit;
        }
    }
}
