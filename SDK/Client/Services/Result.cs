namespace AzureDevops.Client.Services
{
    public class Result
    {
        public bool HasError { get; set; }
        public string ErrorDescription { get; set; }

        public Result(bool hasError, string errorDescription)
        {
            HasError = hasError;
            ErrorDescription = errorDescription;
        }

        public Result()
        {
        }
    }

    public class Result<T> : Result
    {
        public T Data { get; set; }

        public Result(T data)
            => Data = data;

        public Result(bool hasError, string errorDescription)
            : base(hasError, errorDescription) { }

        public Result()
        {
        }
    }
}