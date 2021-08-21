using Xunit;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MetricsAgent.DAL;
using AutoMapper;
using System;
using System.Collections.Generic;
using MetricsAgent.Models;
using MetricsAgent.Requests;

namespace MetricsAgent.Tests
{
    public class CpuMetricsControllerTests
    {

        private Mock<CpuMetricsController> mockController;
        CpuMetricsController controller;
        private Mock<ILogger<CpuMetricsController>> mockLogger;
        private Mock<ICpuMetricsRepository> mockRepository;
        private Mock<IMapper> mockMapper;

        public CpuMetricsControllerTests()
        {
            mockLogger = new Mock<ILogger<CpuMetricsController>>();
            mockRepository = new Mock<ICpuMetricsRepository>();
            mockMapper = new Mock<IMapper>();
            mockController = new Mock<CpuMetricsController>(); ;
            controller = new CpuMetricsController(mockLogger.Object, mockRepository.Object, mockMapper.Object);
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
        public void GetMetrics_OkReturned()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            mockRepository.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()))
                .Returns(new List<CpuMetric> { new CpuMetric { Id = 1, Time = TimeSpan.FromSeconds(1), Value = 1 } });

            var result = controller.GetMetrics(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void Create_OkReturned()
        {
            var request = new CpuMetricCreateRequest();

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
