using MetricsManager.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private ILogger<NetworkMetricsController> _logger;
        private INetworkMetricsRepository _repository;

        public NetworkMetricsController(ILogger<NetworkMetricsController> loger, INetworkMetricsRepository repository)
        {
            _logger = loger;
            _logger.LogDebug(1, "NLog встроен в NetworkMetricsController");

            _repository = repository;
        }


        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetNetworkMetricsByTimePeriod(
            [FromRoute] int agentId, 
            [FromRoute] TimeSpan fromTime, 
            [FromRoute] TimeSpan toTime
            )
        {
            _logger.LogInformation($"Вызван метод NetworkMetricsController.GetMetricsFromAgent с аргументами {agentId}, {fromTime} и {toTime}");
            var metrics = (from metric in _repository.GetByTimePeriod(fromTime, toTime)
                           where metric.AgentId == agentId
                           select metric)
                           .ToList();

            return Ok(metrics);
        }
    }
}
