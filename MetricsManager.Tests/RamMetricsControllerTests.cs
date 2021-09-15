using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using MetricsManager.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsManager.DAL;
using MetricsManager.Models;
using System.Collections.Generic;
using AutoMapper;

namespace Lesson2.Tests
{
    public class RamMetricsControllerTests
    {
        private RamMetricsController controller;
        private Mock<ILogger<RamMetricsController>> mock;
        private Mock<IRamMetricsRepository> repositoryMock;
        private Mock<IMapper> mapperMock;

        public RamMetricsControllerTests()
        {
            mock = new Mock<ILogger<RamMetricsController>>();
            repositoryMock = new Mock<IRamMetricsRepository>();
            mapperMock = new Mock<IMapper>();
            controller = new RamMetricsController(mock.Object, repositoryMock.Object, mapperMock.Object);
        }


        [Fact]
        public void GetRamMetricsByTimePeriod_OkReturned()
        {
            var fromTime = "0";

            var toTime = "100";

            repositoryMock.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
                .Returns(new List<RamMetric> { new RamMetric { Id = 1, AgentId = 1, Time = 1, Value = 1 } });

            var result = controller.GetRamMetricsByTimePeriod(fromTime, toTime);


            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
