using Xunit;
using Moq;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using System;

namespace MetricsAgent.Tests
{
    public class CpuMetricsRepositoryTests
    {
        private Mock<ICpuMetricsRepository> mock;

        public CpuMetricsRepositoryTests ()
        {
            mock = new Mock<ICpuMetricsRepository>();
        }


        [Fact]
        public void Create_ShouldCall_Create()
        {
            mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();
            mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }


        [Fact]
        public void GetByTimePeriod_ShouldCall_Create()
        {
            mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>())).Verifiable();
            mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.AtMostOnce());
        }
    }
}
