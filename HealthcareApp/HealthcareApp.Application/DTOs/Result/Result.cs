using Microsoft.AspNetCore.Identity;

namespace HealthcareApp.Application.DTOs.Result
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public string[] Error { get; }
        public T? Body { get; }

        private Result(bool isSuccess, IEnumerable<string> errors, T? body)
        {
            IsSuccess = isSuccess;
            Error = errors?.ToArray() ?? Array.Empty<string>();
            Body = body;
        }

        public static Result<T> Failure(string error) => new Result<T>(false, new[] { error }, default);
        public static Result<T> Failure(IEnumerable<string> errors) => new Result<T>(false, errors, default);
        public static Result<T> Success(T? body = default) => new Result<T>(true, Array.Empty<string>(), body);

        public static implicit operator Result<T>(string error) => Failure(error);
        public static implicit operator Result<T>(T body) => Success(body);
    }
}