namespace DDDFW
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public sealed record ResultError(string Code, string Message, Exception Exception = null);

    public class Result
    {
        private readonly List<ResultError> _errors = new();

        public bool Success => _errors.Count == 0;

        public IReadOnlyCollection<ResultError> Errors
            => _errors;

        protected Result()
        {
        }

        protected Result(ResultError error)
        {
            _errors.Add(error);
        }

        protected Result(IEnumerable<ResultError> errors)
        {
            _errors.AddRange(errors);
        }

        public static Result Ok()
            => new();

        public static Result Fail(ResultError error) => new(error);

        public static Result Fail(IEnumerable<ResultError> errors)  => new(errors);
    }

    public sealed class Result<T> : Result
    {
        public T Value { get; }

        private Result(T value)
        {
            Value = value;
        }

        private Result(ResultError error) : base(error)
        {
        }

        private Result(IEnumerable<ResultError> errors) : base(errors)
        {
        }

        public static Result<T> Ok(T value)
            => new(value);

        public static new Result<T> Fail(ResultError error) => new(error);

        public static new Result<T> Fail(IEnumerable<ResultError> errors) => new(errors);
    }
}
