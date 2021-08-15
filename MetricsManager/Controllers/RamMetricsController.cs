using MetricsManager.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private ILogger<RamMetricsController> _logger;
        private IRamMetricsRepository _repository;

        public RamMetricsController(ILogger<RamMetricsController> loger, IRamMetricsRepository repository)
        {
            _logger = loger;
            _logger.LogDebug(1, "NLog встроен в RamMetricsController");

            _repository = repository;
        }


        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetRamMetricsByTimePeriod(
            [FromRoute] int agentId, 
            [FromRoute] TimeSpan fromTime, 
            [FromRoute] TimeSpan toTime
            )
        {
            _logger.LogInformation($"Вызван метод RamMetricsController.GetMetricsFromAgent с аргументами {agentId}, {fromTime} и {toTime}");
            var metrics = (from metric in _repository.GetByTimePeriod(fromTime, toTime)
                           where metric.AgentId == agentId
                           select metric)
                           .ToList();

            return Ok();
        }
    }
}
