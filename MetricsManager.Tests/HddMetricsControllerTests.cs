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
    public class HddMetricsControllerTests
    {
        private HddMetricsController controller;
        private Mock<ILogger<HddMetricsController>> mock;
        private Mock<IHddMetricsRepository> repositoryMock;

        public HddMetricsControllerTests ()
        {
            mock = new Mock<ILogger<HddMetricsController>>();
            repositoryMock = new Mock<IHddMetricsRepository>();
            controller = new HddMetricsController(mock.Object, repositoryMock.Object);
        }


        [Fact]
        public void GetHddMetricsByTimePeriod_OkReturned()
        {
            var agentId = 1;

            var fromTime = TimeSpan.FromSeconds(0);

            var toTime = TimeSpan.FromSeconds(100);

            repositoryMock.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
                .Returns(new List<HddMetric> { new HddMetric { Id = 1, AgentId = 1, Time = 1, Value = 1 } });

            var result = controller.GetHddMetricsByTimePeriod(agentId, fromTime, toTime);


            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
