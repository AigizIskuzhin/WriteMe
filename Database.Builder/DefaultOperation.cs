using Database.Builder.Interfaces;
using static System.Guid;

namespace Database.Builder
{
    public class DefaultOperation :
        ITransientOperation,
        IScopedOperation,
        ISingletonOperation
    {
        public string OperationId { get; } = NewGuid().ToString()[^4..];
    }
}
