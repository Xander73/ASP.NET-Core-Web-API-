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
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private ILogger<HddMetricsController> _logger;
        private IHddMetricsRepository _repository;
        private IMapper _mapper;

        public HddMetricsController(
            ILogger<HddMetricsController> logger, 
            IHddMetricsRepository repository,
            IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в HddMetricsController.");

            _repository = repository;

            _mapper = mapper;
        }


        [HttpGet("left")]
        public IActionResult GetLeftMemoryMegabyte()
        {
            _logger.LogInformation($"Вызван метод HddMetricsController.GetLeftMemoryMegabyte без аргументов.");
            return Ok();
        }


        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            _logger.LogInformation($"Вызван метод HddMetricsController.Create с аргументом {request}.");
            _repository.Create(new HddMetric
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }


        [HttpGet("all")]
        public IActionResult GetAll()
        {
            _logger.LogInformation($"Вызван метод HddMetricsController.GetAll без аргументов.");
            var metrics = _repository.GetAll();

            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };

            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
                }
            }
            return Ok(response);
        }


        [HttpGet("metricsController/from/{fromTime}/to/{toTime}")]
        public IActionResult GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            _logger.LogInformation($"Вызван метод HddMetricsController.GetByTimePeriod с аргументами {fromTime} и {toTime}.");
            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };

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
