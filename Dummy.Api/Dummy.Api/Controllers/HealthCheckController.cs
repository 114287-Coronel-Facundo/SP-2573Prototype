using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Dummy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly ILogger<HealthCheckController> _logger;
        private readonly HealthCheckService _service;
        public HealthCheckController(ILogger<HealthCheckController> logger,
        HealthCheckService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var report = await _service.CheckHealthAsync();
                string json = System.Text.Json.JsonSerializer.Serialize(report);
                _logger.LogError(json);
                if (report.Status == HealthStatus.Healthy)
                    return Ok(json);
                return NotFound("Service unavailable");
            }catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
