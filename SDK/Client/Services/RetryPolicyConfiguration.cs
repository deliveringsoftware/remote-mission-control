namespace AzureDevops.Client.Services
{
    public class RetryPolicyConfiguration
    {
        public static RetryPolicyConfiguration Default = new RetryPolicyConfiguration(3, 1);

        public int RetryCount { get; }
        public int RetryAttemptFactor { get; }

        public RetryPolicyConfiguration(int retryCount, int retryAttemptFactor)
        {
            this.RetryCount = retryCount;
            this.RetryAttemptFactor = retryAttemptFactor;
        }
    }
}
