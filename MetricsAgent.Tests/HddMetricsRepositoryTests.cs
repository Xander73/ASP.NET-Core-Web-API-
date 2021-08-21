using MetricsAgent.DAL;
using MetricsAgent.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using Xunit;

namespace MetricsAgent.Tests
{
    public class HddMetricsRepositoryTests
    {
        private Mock<IHddMetricsRepository> mock;

        public HddMetricsRepositoryTests()
        {
            mock = new Mock<IHddMetricsRepository>();
        }


        [Fact]
        public void Create_ShouldCall_Create()
        {
            mock.Setup(repository => repository.Create(It.IsAny<HddMetric>())).Verifiable();
            mock.Verify(repository => repository.Create(It.IsAny<HddMetric>()), Times.AtMostOnce());
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
            mock.Setup(repository => repository.Update(It.IsAny<HddMetric>())).Verifiable();
            mock.Verify(repository => repository.Update(It.IsAny<HddMetric>()), Times.AtMostOnce());
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
            mock.Setup(repository => repository.Create(It.IsAny<HddMetric>())).Verifiable();
            mock.Verify(repository => repository.Create(It.IsAny<HddMetric>()), Times.AtMostOnce());
        }


        [Fact]
        public void GetByTimePeriod_ShouldCall_Create()
        {
            mock.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>())).Verifiable();
            mock.Verify(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.AtMostOnce());
        }
    }


    
}
