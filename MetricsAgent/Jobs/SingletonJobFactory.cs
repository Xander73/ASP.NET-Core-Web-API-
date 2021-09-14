using Quartz.Spi;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MetricsAgent.Jobs
{
    public class SingletonJobFactory : IJobFactory
    {
        private readonly IServiceProvider _servicePprovider;

        public SingletonJobFactory (IServiceProvider servicePprovider)
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
