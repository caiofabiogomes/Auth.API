namespace Auth.API.Application
{
    namespace OrderService.Application.Abstractions
    {
        public abstract class ResultBase
        {
            protected ResultBase(bool isSuccess, string message)
            {
                IsSuccess = isSuccess;
                Message = message;
            }
            public bool IsSuccess { get; }
            public string Message { get; }
        }

        public class Result<T> : ResultBase
        {
            public Result(bool isSuccess, string message, T data = default) : base(isSuccess, message)
            {
                Data = data;
            }

            public T Data { get; }

            public static Result<T> Success(T data, string message = "Success Operation")
            {
                return new Result<T>(true, message, data: data);
            }

            public static Result<T> Failure(string message = "Failed Operation")
            {
                return new Result<T>(false, message);
            }

        }

        public class Result : ResultBase
        {
            private Result(bool isSuccess, string message) : base(isSuccess, message)
            {
            }

            public static Result Success(string message = "Success Operation")
                => new Result(true, message);

            public static Result Failure(string message = "Failed Operation")
                => new Result(false, message);
        }

    }
}
