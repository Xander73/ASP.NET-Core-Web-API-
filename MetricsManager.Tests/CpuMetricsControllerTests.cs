using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;

namespace Lesson2.Tests
{
    public class CpuMetricsControllerTests
    {
        private CPUMetricsController controller;
        private Mock<ILogger<CPUMetricsController>> mock;


        public CpuMetricsControllerTests ()
        {
            mock = new Mock<ILogger<CPUMetricsController>>();
            controller = new CPUMetricsController(mock.Object);
        }


        [Fact]
        public void GetMetricsFromAgent_OkReturned()
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
