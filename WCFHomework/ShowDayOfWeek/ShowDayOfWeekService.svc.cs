namespace ShowDayOfWeek
{
    using System;

    public class ShowDayOfWeekService : IShowDayOfWeekService
    {
        public string ShowDayOfWeek(DateTime date)
        {
            var culture = new System.Globalization.CultureInfo("bg-BG");
            var day = culture.DateTimeFormat.GetDayName(date.DayOfWeek);
            return day;
        }
    }
}
