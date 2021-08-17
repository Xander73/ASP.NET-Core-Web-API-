using AutoMapper;
using MetricsAgent.DAL;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/dotnetmetrics")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private ILogger<DotNetMetricsController> _logger;
        private IDotNetMetricsRepository _repository;

        private IMapper _mapper;

        public DotNetMetricsController(
            ILogger<DotNetMetricsController> logger, 
            IDotNetMetricsRepository repository, 
            IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в DotNetMetricsController");

            _repository = repository;

            _mapper = mapper;
        }


        [HttpGet("from/{fromParameter}/to/{toParameter}")]
        public IActionResult GetByTimePeriod([FromRoute] string fromParameter, [FromRoute] string toParameter)
        {
            _logger.LogDebug($"Запущен метод DotNetMetricsController.GetByTimePeriod с параметрами {fromParameter} и {toParameter}");
            //обратно привожу строки к double
            TimeSpan fromTime = TimeSpan.FromSeconds(Convert.ToDouble(fromParameter));
            TimeSpan toTime = TimeSpan.FromSeconds(Convert.ToDouble(toParameter));

            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };

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
