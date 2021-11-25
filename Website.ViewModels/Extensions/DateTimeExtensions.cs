using System;

namespace Website.ViewModels.Extensions
{
    public static class DateTimeExtensions
    {

        /// <summary>
        /// Конвертер времени в надпись последнего добавления
        /// </summary>
        /// <param name="now"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToLastMessageReceive(this DateTime source)
        {
            DateTime now = DateTime.Now;
            TimeSpan difference = now.Subtract(source);
            string commentary = difference.TotalMinutes switch
            {
                < 1 => "Только что",
                1 => "Минуту назад",
                2 => "2 минуты назад",
                3 => "3 минуты назад",
                4 => "4 минуты назад",
                5 => "5 минут назад",
                <= 1440 => now.TimeOfDay.Ticks - difference.Ticks<0
                ? "Вчера в " + source.ToString("HH:mm")
                : "Сегодня в " + source.ToString("HH:mm"),
                _ => source.ToString("dd.MM в HH:mm"),
            };
            return commentary;
        }
        public static string ToHoursAndMinutes(this DateTime source)
        {
            DateTime now = DateTime.Now;
            //TimeSpan difference = now.Subtract(source);
            //string commentary = difference.TotalMinutes switch
            //{
            //    < 1 => "Только что",
            //    1 => "Минуту назад",
            //    2 => "2 минуты назад",
            //    3 => "3 минуты назад",
            //    4 => "4 минуты назад",
            //    5 => "5 минут назад",
            //    _ => source.ToString("HH:mm"),
            //};
            return source.ToString("HH:mm");
        }

        public static DateTime ConvertBack(string source)
        {
            throw new System.NotImplementedException();
        }    
        public static string ToAgeString(this DateTime dob)    
        {
            DateTime today = DateTime.Today;
            int months = today.Month - dob.Month;
            int years = today.Year - dob.Year;
            if (today.Day < dob.Day) months--;
            if (months < 0)
            {
                years--;
                months += 12;
            }
            int days = (today - dob.AddMonths((years * 12) + months)).Days;
            return years + " лет";
            //$"{years} year{((years == 1) ? "" : "s")}, {months} month{((months == 1) ? "" : "s")} and {days} day{((days == 1) ? "" : "s")}";

        }
    }
}
