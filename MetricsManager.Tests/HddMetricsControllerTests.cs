using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using MetricsManager.Controllers;
using Microsoft.Extensions.Logging;
using Moq;

namespace Lesson2.Tests
{
    public class HddMetricsControllerTests
    {
        private HddMetricsController controller;
        private Mock<ILogger<HddMetricsController>> mock;

        public HddMetricsControllerTests ()
        {
            mock = new Mock<ILogger<HddMetricsController>>();
            controller = new HddMetricsController(mock.Object);
        }


        [Fact]
        public void GetMetricFromAgent_OkReturned ()
        {
            var agentId = 1;

            var fromTime = TimeSpan.FromSeconds(0);

            var toTime = TimeSpan.FromSeconds(100);


            var result = controller.GetMetricFromAgent(agentId, fromTime, toTime);


            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void GetMetricsFromAllCluster_OkReturned()
        {
            var fromTime = TimeSpan.FromSeconds(0);

            var toTime = TimeSpan.FromSeconds(100);


            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);


            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
