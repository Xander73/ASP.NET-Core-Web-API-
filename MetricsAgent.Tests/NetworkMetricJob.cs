using MetricsAgent.DAL;
using MetricsAgent.Jobs;
using Moq;
using Quartz;
using System.Threading.Tasks;
using Xunit;

namespace MetricsAgent.Tests
{
    public class NetworkMetricJobTests
    {
        private Mock<IJobExecutionContext> mockContext;
        private Mock<INetworkMetricsRepository> mockRepository;
        private IJob job;

        public NetworkMetricJobTests()
        {
            mockContext = new Mock<IJobExecutionContext>();
            mockRepository = new Mock<INetworkMetricsRepository>();
            job = new NetworkMetricJob(mockRepository.Object);
        }


        [Fact]
        public void Execute_OkReturned()
        {
            var result = job.Execute(mockContext.Object);

            Assert.IsAssignableFrom<Task>(result);
        }
    }
}
