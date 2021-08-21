using Xunit;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MetricsAgent.DAL;
using AutoMapper;
using System;
using MetricsAgent.Requests;

namespace MetricsAgent.Tests
{
    public class RamMetricsControllerTests
    {
        private RamMetricsController controller;
        private Mock<ILogger<RamMetricsController>> mockLogger;
        private Mock<IRamMetricsRepository> mockRepository;
        private Mock<IMapper> mockMapper;

        public RamMetricsControllerTests()
        {
            mockLogger = new Mock<ILogger<RamMetricsController>>();
            mockRepository = new Mock<IRamMetricsRepository>();
            mockMapper = new Mock<IMapper>();
            controller = new RamMetricsController(mockLogger.Object, mockRepository.Object, mockMapper.Object);
        }

        [Fact]
        public void GetByTimePeriod_OkReturned()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(1600000000);

            TimeSpan toTime = TimeSpan.FromSeconds(1630000000);

            var result = controller.GetByTimePeriod(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void GetRamAvailable_OkReturned()
        {
            var result = controller.GetRamAvailable();

            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void Create_OkReturned()
        {
            var request = new RamMetricCreateRequest();

            var result = controller.Create(request);

            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void GetAll_OkReturned()
        {
            var result = controller.GetAll();

            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
