using System;
using Xunit;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MetricsAgent.DAL;
using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using System.Collections.Generic;

namespace MetricsAgent.Tests
{
    public class DotNetMetricsControllerTests
    {
        private DotNetMetricsController controller;
        private Mock<ILogger<DotNetMetricsController>> mockLogger;
        private Mock<IDotNetMetricsRepository> mockRepository;
        private Mock<IMapper> mockMapper;

        public DotNetMetricsControllerTests()
        {
            mockLogger = new Mock<ILogger<DotNetMetricsController>>();
            mockRepository = new Mock<IDotNetMetricsRepository>(); 
            mockMapper = new Mock<IMapper>();
            controller = new DotNetMetricsController(mockLogger.Object, mockRepository.Object, mockMapper.Object);
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
        public void GetErrorsCount_OkReturned()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            var result = controller.GetErrorsCount(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void Create_OkReturned()
        {
            var request = new DotNetMetricCreateRequest();

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
