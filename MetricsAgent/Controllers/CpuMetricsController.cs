using AutoMapper;
using MetricsAgent.DAL;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/cpumetrics")]
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
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");

            _repository = repository;

            _mapper = mapper;
        }


        [HttpGet("from/{fromParameter}/to/{toParameter}")]
        public IActionResult GetByTimePeriod([FromRoute]string fromParameter, [FromRoute]string toParameter)
        {
            _logger.LogInformation($"Запущен метод CpuMetricsController.GetByTimePeriod с параметрами {fromParameter} и {toParameter}");
            //обратно привожу строки к double
            TimeSpan fromTime = TimeSpan.FromSeconds(Convert.ToDouble(fromParameter));
            TimeSpan toTime = TimeSpan.FromSeconds(Convert.ToDouble(toParameter));

            var metrics = _repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };

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
