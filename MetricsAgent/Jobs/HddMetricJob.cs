using MetricsAgent.DAL;
using Quartz;
using System;
using System.Threading.Tasks;
using System.IO;
using MetricsAgent.Models;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob : IJob
    {
        private IHddMetricsRepository _repository;

        public HddMetricJob(IHddMetricsRepository repository)
        {            
            _repository = repository;
        }


        public Task Execute(IJobExecutionContext context)
        {
            //PerformanceCounter _counter = new PerformanceCounter("HDD", "Free Megabytes");

            //var value = Convert.ToInt32(_counter.NextValue());

            DriveInfo driveInfo = new DriveInfo("C");
            int value = 0;
            if (driveInfo.IsReady)
            {
                value = Convert.ToInt32(Convert.ToInt64( driveInfo.AvailableFreeSpace)/1000_000);
            }

            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            _repository.Create(new HddMetric { Value = value, Time = time.TotalSeconds });

            return Task.CompletedTask;
        }
    }
}
