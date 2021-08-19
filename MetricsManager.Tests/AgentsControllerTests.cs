using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using MetricsManager.Controllers;
using MetricsManager.Core;
using Moq;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Tests
{
    public class AgentsControllerTests
    {
        private AgentsController controller;
        private Mock<ILogger<AgentsController>> mock;


        public AgentsControllerTests()
        {
            mock = new Mock<ILogger<AgentsController>>();
            controller = new AgentsController(mock.Object);
        }

        [Fact]
        public void RegisterAgent_OkReturned()
        {
            var agentInfo = new AgentInfo();


            var result = controller.RegisterAgent(agentInfo);


            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void EnableAgentById_OkReturned()
        {
            var agentId = 1;

            var result = controller.EnableAgentById(agentId);

            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void DisableAgentById()
        {
            var agentId = 1;

            var result = controller.DisableAgentById(agentId);

            Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void GetAllAgents_OkReturned()
        {
            var result = controller.GetAllAgents();


            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
