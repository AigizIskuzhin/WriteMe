namespace System
{
    static class RandomExtenstions
    {
        public static T NextItem<T>(this Random rnd, params T[] items) => items[rnd.Next(items.Length)];
    }
}
