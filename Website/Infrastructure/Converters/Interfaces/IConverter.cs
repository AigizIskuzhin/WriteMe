namespace Website.Infrastructure.Converters.Interfaces
{
    public interface IConverter<T>
    {
        T Convert<T>(T source);
        T ConvertBack(T source);
    }
}
