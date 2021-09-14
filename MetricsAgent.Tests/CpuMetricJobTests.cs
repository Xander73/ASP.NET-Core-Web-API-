using MetricsAgent.DAL;
using MetricsAgent.Jobs;
using Moq;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MetricsAgent.Tests
{    
    public class CpuMetricJobTests
    {
        private Mock<IJobExecutionContext> mockContext;
        private Mock<ICpuMetricsRepository> mockRepository;
        private IJob job;

        public CpuMetricJobTests ()
        {
            mockContext = new Mock<IJobExecutionContext>();
            mockRepository = new Mock<ICpuMetricsRepository>();
            job = new CpuMetricJob(mockRepository.Object);
        }

        
        [Fact]
        public void Execute_OkReturned()
        {
            var result = job.Execute(mockContext.Object);

            Assert.IsAssignableFrom<Task>(result);
        }
    }
}
