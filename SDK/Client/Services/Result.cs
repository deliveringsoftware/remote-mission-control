namespace AzureDevops.Client.Services
{
    public class Result
    {
        public bool HasError { get; set; }
        public string ErrorDescription { get; set; }

        public Result(bool hasError, string errorDescription)
        {
            this.HasError = hasError;
            this.ErrorDescription = errorDescription;
        }

        public Result()
        {
        }
    }

    public class Result<T> : Result
    {
        public T Data { get; set; }

        public Result(T data)
            => this.Data = data;

        public Result(bool hasError, string errorDescription)
            : base(hasError, errorDescription) { }

        public Result()
        {
        }
    }
}