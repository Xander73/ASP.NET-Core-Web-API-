using MetricsAgent.DAL;
using MetricsAgent.Jobs;
using Moq;
using Quartz;
using System.Threading.Tasks;
using Xunit;

namespace MetricsAgent.Tests
{
    public class HddMetricJobTests
    {
        private Mock<IJobExecutionContext> mockContext;
        private Mock<IHddMetricsRepository> mockRepository;
        private IJob job;

        public HddMetricJobTests()
        {
            mockContext = new Mock<IJobExecutionContext>();
            mockRepository = new Mock<IHddMetricsRepository>();
            job = new HddMetricJob(mockRepository.Object);
        }


        [Fact]
        public void Execute_OkReturned()
        {
            var result = job.Execute(mockContext.Object);

            Assert.IsAssignableFrom<Task>(result);
        }
    }
}
