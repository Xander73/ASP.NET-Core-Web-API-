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
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private ILogger<NetworkMetricsController> _logger;
        private INetworkMetricsRepository _repository;
        private IMapper _mapper;

        public NetworkMetricsController(
            ILogger<NetworkMetricsController> loger, 
            INetworkMetricsRepository repository,
            IMapper mapper
            )
        {
            _logger = loger;
            _logger.LogDebug(1, "NLog встроен в NetworkMetricsController");

            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetNetworkMetricsByTimePeriod(
            [FromRoute] string fromTime, 
            [FromRoute] string toTime
            )
        {
            _logger.LogInformation($"Вызван метод NetworkMetricsController.GetMetricsFromAgent с аргументами {fromTime} и {toTime}");

            var response = new AllNetworkMetricsApiResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            var metrics = (from metric in _repository
                           .GetByTimePeriod(
                           TimeSpan.FromSeconds(double.Parse(fromTime)),
                           TimeSpan.FromSeconds(double.Parse(toTime))
                          )
                           select metric)
                           .ToList<NetworkMetric>();

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
