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
        public void Delete_ShouldCall_Create()
        {
            mock.Setup(repository => repository.Delete(It.IsAny<int>())).Verifiable();
            mock.Verify(repository => repository.Delete(It.IsAny<int>()), Times.AtMostOnce());
        }


        [Fact]
        public void Update_ShouldCall_Create()
        {
            mock.Setup(repository => repository.Update(It.IsAny<CpuMetric>())).Verifiable();
            mock.Verify(repository => repository.Update(It.IsAny<CpuMetric>()), Times.AtMostOnce());
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
            mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();
            mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }


        [Fact]
        public void GetByTimePeriod_ShouldCall_Create()
        {
            mock.Setup(repository => repository.GetById(It.IsAny<int>())).Verifiable();
            mock.Verify(repository => repository.GetById(It.IsAny<int>()), Times.AtMostOnce());
        }
    }
}
