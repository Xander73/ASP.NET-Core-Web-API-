using Xunit;
using MetricsManager.Controllers;
using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;

namespace Lesson2.Tests
{
    public class DotNetMetricsControllerTests
    {
        private DotNetMetricsController controller;
        private Mock<ILogger<DotNetMetricsController>> mock;

        public DotNetMetricsControllerTests ()
        {
            mock = new Mock<ILogger<DotNetMetricsController>>();
            controller = new DotNetMetricsController(mock.Object);
        }


        [Fact]
        public void DotNetMetricsController_OkReturned()
        {
            var agentId = 1;

            var fromTime = TimeSpan.FromSeconds(0);

            var toTime = TimeSpan.FromSeconds(100);


            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);


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
