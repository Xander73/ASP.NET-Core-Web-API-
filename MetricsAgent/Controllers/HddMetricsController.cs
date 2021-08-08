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
    [Route("api/hddmetrics")]
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
            _logger.LogDebug(1, "NLog встроен в HddMetricsController");

            _repository = repository;

            _mapper = mapper;
        }


        [HttpGet("from/{fromParameter}/to/{toParameter}")]
        public IActionResult GetByTimePeriod([FromRoute] string fromParameter, [FromRoute] string toParameter)
        {
            _logger.LogDebug($"Запущен метод HddMetricsController.GetByTimePeriod с параметрами {fromParameter} и {toParameter}");
            //обратно привожу строки к double
            TimeSpan fromTime = TimeSpan.FromSeconds(Convert.ToDouble(fromParameter));
            TimeSpan toTime = TimeSpan.FromSeconds(Convert.ToDouble(toParameter));

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
