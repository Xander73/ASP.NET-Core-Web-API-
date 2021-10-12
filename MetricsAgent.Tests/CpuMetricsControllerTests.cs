using Xunit;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MetricsAgent.DAL;
using AutoMapper;

namespace MetricsAgent.Tests
{
    public class CpuMetricsControllerTests
    {

        private CpuMetricsController controller;
        private Mock<ILogger<CpuMetricsController>> mockLogger;
        private Mock<ICpuMetricsRepository> mockRepository;
        private Mock<IMapper> mockMapper;

        public CpuMetricsControllerTests()
        {
            mockLogger = new Mock<ILogger<CpuMetricsController>>();
            mockRepository = new Mock<ICpuMetricsRepository>();
            mockMapper = new Mock<IMapper>();
            controller = new CpuMetricsController(mockLogger.Object, mockRepository.Object, mockMapper.Object);
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
