using System;

namespace Website.Infrastructure.Converters
{
    public static class TimeOfAdditionConverter // : IConverter<Datetime>
    {
        public static string Convert(DateTime source)
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
                _ => source.ToString("dd:MM в HH:mm"),
            };
            return commentary;
        }

        public static DateTime ConvertBack(string source)
        {
            throw new System.NotImplementedException();
        }
    }
}
