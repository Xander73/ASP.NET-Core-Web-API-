using AutoMapper;
using MetricsManager.DAL;
using MetricsManager.Models;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private ILogger<RamMetricsController> _logger;
        private IRamMetricsRepository _repository;
        private IMapper _mapper;

        public RamMetricsController(
            ILogger<RamMetricsController> loger, 
            IRamMetricsRepository repository,
            IMapper mapper)
        {
            _logger = loger;
            _logger.LogDebug(1, "NLog встроен в RamMetricsController");

            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetRamMetricsByTimePeriod(
            [FromRoute] string fromTime, 
            [FromRoute] string toTime
            )
        {
            _logger.LogInformation($"Вызван метод RamMetricsController.GetMetricsFromAgent с аргументами {fromTime} и {toTime}");

            var response = new AllRamMetricsApiResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

            var metrics = (from metric in 
                           _repository.GetByTimePeriod(
                           TimeSpan.FromSeconds(double.Parse(fromTime)),
                           TimeSpan.FromSeconds(double.Parse(toTime))
                           )
                           select metric)
                           .ToList<RamMetric>();

            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
                }
            }

            return Ok(response);
        }
    }
}
