using AutoMapper;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private ILogger<RamMetricsController> _logger;
        private IRamMetricsRepository _repository;
        private IMapper _mapper;

        public RamMetricsController(
            ILogger<RamMetricsController> logger, 
            IRamMetricsRepository repository,
            IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamMetricsController");

            _repository = repository;

            _mapper = mapper;
        }


        [HttpGet("available")]
        public IActionResult GetRamAvailable()
        {
            _logger.LogInformation($"Вызван метод RamMetricsController.GetRamAvailable без аргументов");
            return Ok();
        }


        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _logger.LogInformation($"Вызван метод RamMetricsController.Create с аргументом {request}.");
            _repository.Create(new RamMetric
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }


        [HttpGet("all")]
        public IActionResult GetAll()
        {
            _logger.LogInformation($"Вызван метод RamMetricsController.GetAll без аргументов.");
            var metrics = _repository.GetAll();

            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
                }
            }
            return Ok(response);
        }


        [HttpGet("metricsController/from/{fromTime}/to/{toTime}")]
        public IActionResult GetByTimePeriod([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Вызван метод RamMetricsController.GetByTimePeriod с аргументами {fromTime} и {toTime}.");
            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

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
