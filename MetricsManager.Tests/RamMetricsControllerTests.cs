using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using MetricsManager.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsManager.DAL;
using MetricsManager.Models;
using System.Collections.Generic;

namespace Lesson2.Tests
{
    public class RamMetricsControllerTests
    {
        private RamMetricsController controller;
        private Mock<ILogger<RamMetricsController>> mock;
        private Mock<IRamMetricsRepository> repositoryMock;

        public RamMetricsControllerTests()
        {
            mock = new Mock<ILogger<RamMetricsController>>();
            repositoryMock = new Mock<IRamMetricsRepository>();
            controller = new RamMetricsController(mock.Object, repositoryMock.Object);
        }


        [Fact]
        public void GetRamMetricsByTimePeriod_OkReturned()
        {
            var agentId = 1;

            var fromTime = TimeSpan.FromSeconds(0);

            var toTime = TimeSpan.FromSeconds(100);

            repositoryMock.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
                .Returns(new List<RamMetric> { new RamMetric { Id = 1, AgentId = 1, Time = 1, Value = 1 } });

            var result = controller.GetRamMetricsByTimePeriod(agentId, fromTime, toTime);


            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
