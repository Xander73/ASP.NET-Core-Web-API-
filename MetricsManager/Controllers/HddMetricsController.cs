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
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private ILogger<HddMetricsController> _logger;
        private IHddMetricsRepository _repository;
        private IMapper _mapper;

        public HddMetricsController(
            ILogger<HddMetricsController> loger, 
            IHddMetricsRepository repository,
            IMapper mapper)
        {
            _logger = loger;
            _logger.LogDebug(1, "NLog встроен в HddMetricsController");
            
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetHddMetricsByTimePeriod(
            [FromRoute] string fromTime, 
            [FromRoute] string toTime
            )
        {
            _logger.LogInformation($"Вызван метод HddMetricsController.GetMetricsFromAgent с аргументами {fromTime} и {toTime}");

            var response = new AllHddMetricsApiResponse()
            {
                Metrics = new List<HddMetricDto>()
            };

            var metrics = (from metric in
                          _repository.GetByTimePeriod(
                          TimeSpan.FromSeconds(double.Parse(fromTime)),
                          TimeSpan.FromSeconds(double.Parse(toTime))
                          )
                           select metric)
                          .ToList<HddMetric>();

            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
                }
            }

            return Ok(response);
        }
    }
}
