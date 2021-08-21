using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/harddrive")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private ILogger<HddMetricsController> _logger;

        public HddMetricsController(ILogger<HddMetricsController> loger)
        {
            _logger = loger;
            _logger.LogDebug(1, "NLog встроен в HddMetricsController");
        }


        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Вызван метод HddMetricsController.GetMetricsFromAgent с аргументами {agentId}, {fromTime} и {toTime}");
            return Ok();
        }


        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Вызван метод HddMetricsController.GetMetricsFromAllCluster с аргументами {fromTime} и {toTime}");
            return Ok();
        }
    }
}
