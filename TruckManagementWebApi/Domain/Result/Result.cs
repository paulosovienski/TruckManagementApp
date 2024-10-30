using System;
using TruckManagement.Domain.Interfaces;

namespace TruckManagement.Domain
{
    public class Result : IResult
    {
        public string Message { get; }
        public bool Success { get; }

        public DateTime? Updated { get; set; }

        public Result()
        {

        }
        public Result(string message, bool success)
        {
            Message = message;
            Success = success;
        }

        public Result(string message, bool success, DateTime updated)
        {
            Message = message;
            Success = success;
            Updated = updated;
        }

        public Result(bool success) : this(null, success) { }
    }

    public class Result<T> : Result, IResult<T>
    {
        public T Data { get; set; }

        public Result() : base() { }

        public Result(bool success) : base(null, success) { }

        public Result(string message, bool success) : base(message, success) { }

        public Result(string message, bool success, DateTime updated) : base(message, success, updated) { }

        public Result(T data, bool success) : base(null, success) =>
            Data = data;

        public Result(T data, bool success, DateTime updated) : base(null, success, updated) =>
            Data = data;

        public Result(bool success, T data, string message) : base(message, success) =>
            Data = data;

        public Result(bool success, T data, string message, DateTime updated) : base(message, success, updated) =>
            Data = data;

        public static Result<T> Successful(T data)
        {
            return new Result<T>(data, true);
        }

        public static Result<T> Successful(T data, DateTime updated)
        {
            return new Result<T>(data, true, updated);
        }

        public static Result<T> Successful(string message, T data)
        {
            return new Result<T>(true, data, message);
        }

        public static Result<T> Successful(string message, T data, DateTime updated)
        {
            return new Result<T>(true, data, message, updated);
        }

        public static Result<T> Failed()
        {
            return new Result<T>(default(T), false);
        }

        public static Result<T> Failed(string message)
        {
            return new Result<T>(false, default, message);
        }
    }
}
