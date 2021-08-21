using MetricsAgent.DAL;
using MetricsAgent.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private IDotNetMetricsRepository _repository;

        public DotNetMetricJob (IDotNetMetricsRepository repository)
        {
            _repository = repository;
        }


        public Task Execute(IJobExecutionContext context)
        {
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            var value = Convert.ToInt32(GC.GetAllocatedBytesForCurrentThread());

            _repository.Create(new DotNetMetric { Value = value, Time = time });

            return Task.CompletedTask;
        }
    }
}
