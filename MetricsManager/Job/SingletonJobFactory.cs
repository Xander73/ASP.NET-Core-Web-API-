using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;

namespace MetricsManager.Job
{
    public class SingletonJobFactory : IJobFactory
    {
        private readonly IServiceProvider _servicePprovider;

        public SingletonJobFactory(IServiceProvider servicePprovider)
        {
            _servicePprovider = servicePprovider;
        }


        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _servicePprovider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        }


        public void ReturnJob(IJob job)
        {

        }
    }
}

