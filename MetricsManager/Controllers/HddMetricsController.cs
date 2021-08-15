using MetricsManager.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private ILogger<HddMetricsController> _logger;
        private IHddMetricsRepository _repository;

        public HddMetricsController(ILogger<HddMetricsController> loger, IHddMetricsRepository repository)
        {
            _logger = loger;
            _logger.LogDebug(1, "NLog встроен в HddMetricsController");
            
            _repository = repository;
        }


        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetHddMetricsByTimePeriod(
            [FromRoute] int agentId, 
            [FromRoute] TimeSpan fromTime, 
            [FromRoute] TimeSpan toTime
            )
        {
            _logger.LogInformation($"Вызван метод HddMetricsController.GetMetricsFromAgent с аргументами {agentId}, {fromTime} и {toTime}");
            var metrics = (from metric in _repository.GetByTimePeriod(fromTime, toTime)
                           where metric.AgentId == agentId
                           select metric)
                           .ToList();

            return Ok(metrics);
        }
    }
}
