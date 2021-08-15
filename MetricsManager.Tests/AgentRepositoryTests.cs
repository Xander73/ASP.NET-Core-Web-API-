using MetricsManager.DAL;
using MetricsManager.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MetricsManager.Tests
{
    public class AgentRepositoryTests
    {
        private Mock<IAgentRepository> mock;
        private IAgentRepository repository;
        Mock<IConfiguration> mockConfiguration;

        public AgentRepositoryTests()
        {
            mock = new Mock<IAgentRepository>();
            mockConfiguration = new Mock<IConfiguration>();

            repository = new AgentRepository(mockConfiguration.Object);
        }


        [Fact]
        public void Create_ShouldCall_Create()
        {
            mock.Setup(repository => repository.Create(It.IsAny<AgentMetric>())).Verifiable();
            mock.Verify(repository => repository.Create(It.IsAny<AgentMetric>()), Times.AtMostOnce());
        }


        [Fact]
        public void GetAll_ShouldCall_Create()
        {
            mock.Setup(repository => repository.GetAll()).Verifiable();
            mock.Verify(repository => repository.GetAll(), Times.AtMostOnce());
        }


        [Fact]
        void GetLastTime_NotImplementedExeptionReturned()
        {
            var expect = new NotImplementedException();

            try
            {
                repository.GetLastTime();
            }
            catch (Exception ex)
            {

                Assert.True(ex is NotImplementedException);
            }
            

        }
    }
}
