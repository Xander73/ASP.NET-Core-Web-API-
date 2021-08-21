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
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private ILogger<NetworkMetricsController> _logger;
        private INetworkMetricsRepository _repository;
        private IMapper _mapper;

        public NetworkMetricsController(
            ILogger<NetworkMetricsController> logger, 
            INetworkMetricsRepository repository,
            IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в NetworkMetricsController");

            _repository = repository;

            _mapper = mapper;
        }


        [HttpGet("metricsController/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Вызван метод NetworkMetricsController.GetMetrics с аргументами {fromTime} и {toTime}.");
            return Ok();
        }


        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            _logger.LogInformation($"Вызван метод NetworkMetricsController.Create с аргументом {request}.");
            _repository.Create(new NetworkMetric
            {
                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }


        [HttpGet("all")]
        public IActionResult GetAll()
        {
            _logger.LogInformation($"Вызван метод NetworkMetricsController.GetAll без аргументов.");
            var metrics = _repository.GetAll();

            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
                }
            }
            return Ok(response);
        }


        [HttpGet("period")]
        public IActionResult GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            _logger.LogInformation($"Вызван метод NetworkMetricsController.GetByTimePeriod с аргументами {fromTime} и {toTime}.");
            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            if (metrics != null)
            {
                foreach (var metric in metrics)
                {
                    response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
                }
            }
            return Ok(response);
        }
    }
}
