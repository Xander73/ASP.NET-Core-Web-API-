using System;
using Xunit;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MetricsAgent.DAL;
using AutoMapper;
using MetricsAgent.Models;
using System.Collections.Generic;
using MetricsAgent.Requests;

namespace MetricsAgent.Tests
{
    public class NetworkMetricsControllerTests
    {
        private NetworkMetricsController controller;
        private Mock<ILogger<NetworkMetricsController>> mockLogger;
        private Mock<INetworkMetricsRepository> mockRepository;
        private Mock<IMapper> mockMapper;

        public NetworkMetricsControllerTests()
        {
            mockLogger = new Mock<ILogger<NetworkMetricsController>>();
            mockRepository = new Mock<INetworkMetricsRepository>(); 
            mockMapper = new Mock<IMapper>();
            controller = new NetworkMetricsController(mockLogger.Object, mockRepository.Object, mockMapper.Object);
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

            var result = controller.GetMetrics(fromTime, toTime);

            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void Create_OkReturned()
        {
            var request = new NetworkMetricCreateRequest();

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
