using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsManager.DAL;
using MetricsManager.Models;
using System.Collections.Generic;

namespace Lesson2.Tests
{
    public class CpuMetricsControllerTests
    {
        private CpuMetricsController controller;
        private Mock<ILogger<CpuMetricsController>> mock;
        private Mock<ICpuMetricsRepository> repositoryMock;


        public CpuMetricsControllerTests ()
        {
            mock = new Mock<ILogger<CpuMetricsController>>();

            repositoryMock = new Mock<ICpuMetricsRepository>();

            controller = new CpuMetricsController(mock.Object, repositoryMock.Object);

        }


        [Fact]
        public void GetMetricsFromAgent_OkReturned()
        {
            var agentId = 1;

            var fromTime = TimeSpan.FromSeconds(0);

            var toTime = TimeSpan.FromSeconds(100);

            repositoryMock.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
                .Returns(new List<CpuMetric> { new CpuMetric { Id =1, AgentId = 1, Time = 1, Value = 1 } });


            var result = controller.GetCpuMetricsByTimePeriod(agentId, fromTime, toTime);


            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
