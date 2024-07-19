using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Dummy.Api.HealtCheck
{
    public class ApiHealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ApiHealthCheck(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<HealthCheckResult> CheckHealthAsync
        (HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var response = await
                httpClient.GetAsync("https://localhost:7181/WeatherForecast");
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(new HealthCheckResult(
                      status: HealthStatus.Healthy,
                      description: "The API is up and running."));
                }
                return await Task.FromResult(new HealthCheckResult(
                  status: HealthStatus.Unhealthy,
                  description: "The API is down."));
            }
        }
    }
}
