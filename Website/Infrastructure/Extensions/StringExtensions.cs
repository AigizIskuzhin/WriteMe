namespace Website.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string ShortName(this string t,string name, string surname)
        {
            if (string.IsNullOrWhiteSpace(surname))
                return name;
            return name + " " + surname[0] + ".";
        }
    }
}
