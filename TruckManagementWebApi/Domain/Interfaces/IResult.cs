namespace TruckManagement.Domain.Interfaces
{
    public interface IResult
    {
        string Message { get; }
        bool Success { get; }
    }

    public interface IResult<T> : IResult
    {
        T Data { get; set; }
    }
}
