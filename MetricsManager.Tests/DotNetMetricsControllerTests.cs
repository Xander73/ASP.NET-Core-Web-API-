using Xunit;
using MetricsManager.Controllers;
using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsManager.DAL;
using MetricsManager.Models;
using System.Collections.Generic;

namespace Lesson2.Tests
{
    public class DotNetMetricsControllerTests
    {
        private DotNetMetricsController controller;
        private Mock<ILogger<DotNetMetricsController>> mock;
        private Mock<IDotNetMetricsRepository> repositoryMock;

        public DotNetMetricsControllerTests ()
        {
            mock = new Mock<ILogger<DotNetMetricsController>>();
            repositoryMock = new Mock<IDotNetMetricsRepository>();
            controller = new DotNetMetricsController(mock.Object, repositoryMock.Object);
        }


        [Fact]
        public void GetDotNetMetricsByTimePeriod_OkReturned()
        {
            var agentId = 1;

            var fromTime = TimeSpan.FromSeconds(0);

            var toTime = TimeSpan.FromSeconds(100);

            repositoryMock.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
                .Returns(new List<DotNetMetric> { new DotNetMetric { Id = 1, AgentId = 1, Time = 1, Value = 1 } });

            var result = controller.GetDotNetMetricsByTimePeriod(agentId, fromTime, toTime);


            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
