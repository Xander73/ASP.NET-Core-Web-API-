using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CPUMetricsController : ControllerBase
    {
        private ILogger<CPUMetricsController> _logger;

        public CPUMetricsController (ILogger<CPUMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CPUMericsController");
        }


        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Вызван метод CPUMetricsController.GetMetricsFromAgent с аргументами {agentId}, {fromTime} и {toTime}");
            return Ok();
        }


        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Вызван метод CPUMetricsController.GetMetricsFromAllCluster с аргументами {fromTime} и {toTime}");
            return Ok();
        }
    }
}
