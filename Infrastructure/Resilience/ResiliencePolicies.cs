using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Polly;

namespace Infrastructure.Resilience
{
    // Infrastructure/Resilience/ResiliencePolicies.cs
    public static class ResiliencePolicies
    {
        public static IAsyncPolicy CreateSqlRetryPolicy(ILogger logger) =>
            Policy
                .Handle<SqlException>()
                .Or<TimeoutException>()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, delay, retry, context) =>
                        logger.LogWarning(exception,
                            "Retry {Retry} after {Delay}s due to {Message}",
                            retry, delay.TotalSeconds, exception.Message)
                );
    }

}
