namespace CarRental.Application.Common.Results
{
    public class Result<T> : Result, IResultValue
    {
        private Result(T value)
            : base(true, null)
        {
            Value = value;
        }


        private Result(Error? error)
            : base(false, error)
        {
            
        }

        public T? Value { get; }

        object? IResultValue.Value => Value;

        public static Result<T> Success(T value)
            => new(value);

        public static new Result<T> Failure(Error? error)
            => new(error);

        public static implicit operator Result<T>(Error error) =>
            Failure(error);
    }
}
