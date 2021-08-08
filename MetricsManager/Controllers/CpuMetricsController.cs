using MetricsManager.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private ILogger<CpuMetricsController> _logger;
        private ICpuMetricsRepository _repository;

        public CpuMetricsController (ILogger<CpuMetricsController> logger, ICpuMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CPUMericsController");

            _repository = repository;
        }


        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetCpuMetricsByTimePeriod(
            [FromRoute] int agentId, 
            [FromRoute] TimeSpan fromTime, 
            [FromRoute] TimeSpan toTime
            )
        {
            _logger.LogInformation($"Вызван метод CPUMetricsController.GetMetricsFromAgent с аргументами {agentId}, {fromTime} и {toTime}");
                        
            var metrics = (from metric in _repository.GetByTimePeriod(fromTime, toTime)
                          where metric.AgentId == agentId 
                           select metric)
                           .ToList();

            return Ok(metrics);
        }
    }
}
