using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private ILogger<NetworkMetricsController> _logger;

        public NetworkMetricsController(ILogger<NetworkMetricsController> loger)
        {
            _logger = loger;
            _logger.LogDebug(1, "NLog встроен в NetworkMetricsController");
        }


        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Вызван метод NetworkMetricsController.GetMetricsFromAgent с аргументами {agentId}, {fromTime} и {toTime}");
            return Ok();
        }


        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster ([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Вызван метод NetworkMetricsController.GetMetricsFromAllCluster с аргументами {fromTime} и {toTime}");
            return Ok();
        }
    }
}
