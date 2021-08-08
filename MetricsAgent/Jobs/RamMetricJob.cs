using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob : IJob
    {
        private IRamMetricsRepository _repository;
        private PerformanceCounter _ramCounter;

        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }


        public Task Execute(IJobExecutionContext context)
        {
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            var value = Convert.ToInt32(_ramCounter.NextValue());

            _repository.Create(new RamMetric { Time = time.TotalSeconds, Value = value });

            return Task.CompletedTask;
        }
    }
}
