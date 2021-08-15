using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using MetricsManager.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using MetricsManager.DAL;
using MetricsManager.Models;
using System.Collections.Generic;

namespace Lesson2.Tests
{
    public class NetworkMetricsControllerTests
    {
        private NetworkMetricsController controller;
        private Mock<ILogger<NetworkMetricsController>> mock;
        private Mock<INetworkMetricsRepository> repositoryMock;

        public NetworkMetricsControllerTests()
        {
            mock = new Mock<ILogger<NetworkMetricsController>>();
            repositoryMock = new Mock<INetworkMetricsRepository>();
            controller = new NetworkMetricsController(mock.Object, repositoryMock.Object);
        }


        [Fact]
        public void GetNetworkMetricsByTimePeriod_OkReturned()
        {
            var agentId = 1;

            var fromTime = TimeSpan.FromSeconds(0);

            var toTime = TimeSpan.FromSeconds(100);

            repositoryMock.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
                .Returns(new List<NetworkMetric> { new NetworkMetric { Id = 1, AgentId = 1, Time = 1, Value = 1 } });

            var result = controller.GetNetworkMetricsByTimePeriod(agentId, fromTime, toTime);


            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
