using Microsoft.Extensions.Diagnostics.HealthChecks;
using MySqlConnector;

namespace Dummy.Api.HealtCheck
{
    public class DbHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(
           HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using (MySqlConnection pgSqlConnection =
                     new MySqlConnection("server=localhost;port=3306;database=dummy-audit-db;user=root;password=p455w0rd"))
                {
                    if (pgSqlConnection.State !=
                        System.Data.ConnectionState.Open)
                        pgSqlConnection.Open();

                    if (pgSqlConnection.State == System.Data.ConnectionState.Open)
                    {
                        pgSqlConnection.Close();
                        return Task.FromResult(
                        HealthCheckResult.Healthy("The database is up and running."));
                    }
                }

                return Task.FromResult(
                      new HealthCheckResult(
                      context.Registration.FailureStatus, "The database is down."));
            }
            catch (Exception ex)
            {
                return Task.FromResult(
                    new HealthCheckResult(
                        context.Registration.FailureStatus, $"The database is down. {ex}"));
            }
        }
    }
}
