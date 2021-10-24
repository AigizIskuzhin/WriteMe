using WriteMe.Database.Builder.Interfaces;
using static System.Guid;

namespace WriteMe.Database.Builder
{
    public class DefaultOperation :
        ITransientOperation,
        IScopedOperation,
        ISingletonOperation
    {
        public string OperationId { get; } = NewGuid().ToString()[^4..];
    }
}
