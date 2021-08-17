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
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private ILogger<DotNetMetricsController> _logger;
        private IDotNetMetricsRepository _repository;
        private IMapper _mapper;

        public DotNetMetricsController (
            ILogger<DotNetMetricsController> loger, 
            IDotNetMetricsRepository repository,
            IMapper mapper)
        {
            _logger = loger;
            _logger.LogDebug(1, "NLog встроен в DotNetMetricsController");

            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetDotNetMetricsByTimePeriod(
            [FromRoute] string fromTime, 
            [FromRoute] string toTime
            )
        {
            _logger.LogInformation($"Вызван метод DotNetMetricsController.GetMetricsFromAgent с аргументами { fromTime} и { toTime}");

            var response = new AllDotNetMetricsApiResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };

            var metrics = (from metric in 
                           _repository.GetByTimePeriod(
                           TimeSpan.FromSeconds(double.Parse(fromTime)),
                           TimeSpan.FromSeconds(double.Parse(toTime))
                           )
                           select metric)
                           .ToList<DotNetMetric>();
            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
                }
            }

            return Ok(response);
        }
    }
}
