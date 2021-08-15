using MetricsManager.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private ILogger<DotNetMetricsController> _logger;
        private IDotNetMetricsRepository _repository;

        public DotNetMetricsController (ILogger<DotNetMetricsController> loger, IDotNetMetricsRepository repository)
        {
            _logger = loger;
            _logger.LogDebug(1, "NLog встроен в DotNetMetricsController");

            _repository = repository;
        }


        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetDotNetMetricsByTimePeriod(
            [FromRoute] int agentId, 
            [FromRoute] TimeSpan fromTime, 
            [FromRoute] TimeSpan toTime
            )
        {
            _logger.LogInformation($"Вызван метод DotNetMetricsController.GetMetricsFromAgent с аргументами { agentId}, { fromTime} и { toTime}");

            var metrics = (from metric in _repository.GetByTimePeriod(fromTime, toTime)
                           where metric.AgentId == agentId
                           select metric)              
                           .ToList();                            

            return Ok(metrics);
        }
    }
}
