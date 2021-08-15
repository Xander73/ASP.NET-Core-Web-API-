using System;
using Xunit;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MetricsAgent.DAL;
using MetricsAgent.Requests;
using AutoMapper;

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
            var fromTime = "1600000000";

            var toTime = "1650000000";

            var result = controller.GetByTimePeriod(fromTime, toTime);


            Assert.IsAssignableFrom<IActionResult>(result);
        }


        
    }
}
