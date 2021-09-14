using MetricsAgent.DAL;
using MetricsAgent.Jobs;
using Moq;
using Quartz;
using System.Threading.Tasks;
using Xunit;

namespace MetricsAgent.Tests
{
    public class DotNetMetricJobTests
    {
        private Mock<IJobExecutionContext> mockContext;
        private Mock<IDotNetMetricsRepository> mockRepository;
        private IJob job;

        public DotNetMetricJobTests()
        {
            mockContext = new Mock<IJobExecutionContext>();
            mockRepository = new Mock<IDotNetMetricsRepository>();
            job = new DotNetMetricJob(mockRepository.Object);
        }


        [Fact]
        public void Execute_OkReturned()
        {
            var result = job.Execute(mockContext.Object);

            Assert.IsAssignableFrom<Task>(result);
        }
    }
}
