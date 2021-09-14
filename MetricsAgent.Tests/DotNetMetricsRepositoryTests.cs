using MetricsAgent.DAL;
using MetricsAgent.Models;
using Moq;
using System;
using Xunit;

namespace MetricsAgent.Tests
{
    public class DotNetMetricsRepositoryTests
    {
        private Mock<IDotNetMetricsRepository> mock;

        public DotNetMetricsRepositoryTests ()
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
        public void Delete_ShouldCall_Create()
        {
            mock.Setup(repository => repository.Delete(It.IsAny<int>())).Verifiable();
            mock.Verify(repository => repository.Delete(It.IsAny<int>()), Times.AtMostOnce());
        }


        [Fact]
        public void Update_ShouldCall_Create()
        {
            mock.Setup(repository => repository.Update(It.IsAny<DotNetMetric>())).Verifiable();
            mock.Verify(repository => repository.Update(It.IsAny<DotNetMetric>()), Times.AtMostOnce());
        }


        [Fact]
        public void GetAll_ShouldCall_Create()
        {
            mock.Setup(repository => repository.GetAll());
            mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
        }


        [Fact]
        public void GetById_ShouldCall_Create()
        {
            mock.Setup(repository => repository.Create(It.IsAny<DotNetMetric>())).Verifiable();
            mock.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()), Times.AtMostOnce());
        }


        [Fact]
        public void GetByTimePeriod_ShouldCall_Create()
        {
            mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>())).Verifiable();
            mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.AtMostOnce());
        }
    }
}
