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
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private ILogger<CpuMetricsController> _logger;
        private ICpuMetricsRepository _repository;
        private IMapper _mapper;

        public CpuMetricsController(
            ILogger<CpuMetricsController> logger, 
            ICpuMetricsRepository repository, 
            IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CPUMericsController");

            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetCpuMetricsByTimePeriod(
            [FromRoute] string fromTime, 
            [FromRoute] string toTime
            )
        {
            _logger.LogInformation($"Вызван метод CPUMetricsController.GetMetricsFromAgent с аргументами {fromTime} и {toTime}");

            var response = new AllCpuMetricsApiResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };

            var metrics = (from metric in
                          _repository
                          .GetByTimePeriod(
                          TimeSpan.FromSeconds(double.Parse(fromTime)),
                          TimeSpan.FromSeconds(double.Parse(toTime))
                          )                          
                          select metric)
                          .ToList<CpuMetric>();

            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
                }
            }

            return Ok(response);
        }
    }
}
