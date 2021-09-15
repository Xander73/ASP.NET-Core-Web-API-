using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using Quartz;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private INetworkMetricsRepository _repository;
        
        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
        }


        /// <remarks>
        /// Метод считывает и зыписывает в БД колиичество отправленных байт
        /// </remarks>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Execute(IJobExecutionContext context)
        {
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            PerformanceCounterCategory performanceCounterCategory = new PerformanceCounterCategory("Network Interface");
            string instance = performanceCounterCategory.GetInstanceNames()[0]; // 1st NIC !
            PerformanceCounter performanceCounterSent = new PerformanceCounter("Network Interface", "Bytes Sent/sec", instance);

            var value = Convert.ToInt32(performanceCounterSent.NextValue());

            _repository.Create(new NetworkMetric { Time = time.TotalSeconds, Value = value });

            return Task.CompletedTask;
        }
    }
}
