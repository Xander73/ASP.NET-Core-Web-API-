using MetricsManager.DAL;
using MetricsManager.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MetricsManager.Tests
{
    public class DotNetMetricsRepositoryTests
    {
        private Mock<IDotNetMetricsRepository> mock;

        public DotNetMetricsRepositoryTests()
        {
            mock = new Mock<IDotNetMetricsRepository>();
        }


        [Fact]
        public void Create_ShouldCall_Create()
        {
            mock.Setup(repository => repository.Create(It.IsAny<DotNetMetric>())).Verifiable();
            mock.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()), Times.AtMostOnce());
        }


        [Fact]
        public void GetByTimePeriod_ShouldCall_GetByTimePeriod()
        {
            mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>())).Verifiable();
            mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.AtMostOnce());
        }


        [Fact]
        public void GetLastTime_ShouldCall_GetLastTime()
        {
            mock.Setup(repository => repository.GetLastTime()).Verifiable();
            mock.Verify(repository => repository.GetLastTime(), Times.AtMostOnce());
        }
    }
}
