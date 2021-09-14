using MetricsAgent.DAL;
using MetricsAgent.Jobs;
using Moq;
using Quartz;
using System.Threading.Tasks;
using Xunit;

namespace MetricsAgent.Tests
{
    public class RamMetricJobTests
    {
        private Mock<IJobExecutionContext> mockContext;
        private Mock<IRamMetricsRepository> mockRepository;
        private IJob job;

        public RamMetricJobTests()
        {
            mockContext = new Mock<IJobExecutionContext>();
            mockRepository = new Mock<IRamMetricsRepository>();
            job = new RamMetricJob(mockRepository.Object);
        }


        [Fact]
        public void Execute_OkReturned()
        {
            var result = job.Execute(mockContext.Object);

            Assert.IsAssignableFrom<Task>(result);
        }
    }
}
