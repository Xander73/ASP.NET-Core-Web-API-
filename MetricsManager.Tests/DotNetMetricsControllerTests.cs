using Xunit;
using MetricsManager.Controllers;
using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;
using MetricsManager.DAL;
using MetricsManager.Models;
using System.Collections.Generic;
using AutoMapper;

namespace Lesson2.Tests
{
    public class DotNetMetricsControllerTests
    {
        private DotNetMetricsController controller;
        private Mock<ILogger<DotNetMetricsController>> mock;
        private Mock<IDotNetMetricsRepository> repositoryMock;
        private Mock<IMapper> mapperMock;

        public DotNetMetricsControllerTests ()
        {
            mock = new Mock<ILogger<DotNetMetricsController>>();
            repositoryMock = new Mock<IDotNetMetricsRepository>();
            mapperMock = new Mock<IMapper>();
            controller = new DotNetMetricsController(mock.Object, repositoryMock.Object, mapperMock.Object);
        }


        [Fact]
        public void GetDotNetMetricsByTimePeriod_OkReturned()
        {
            var fromTime = "0";

            var toTime = "100";

            repositoryMock.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
                .Returns(new List<DotNetMetric> { new DotNetMetric { Id = 1, AgentId = 1, Time = 1, Value = 1 } });

            var result = controller.GetDotNetMetricsByTimePeriod(fromTime, toTime);


            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
