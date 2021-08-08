using MetricsManager.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private ILogger<AgentsController> _logger;

        public AgentsController(ILogger<AgentsController> loger)
        {
            _logger = loger;
            _logger.LogDebug(1, "NLog встроен в AgentsController");
        }


        [HttpPost("register")]
        public IActionResult RegisterAgent ([FromBody] AgentInfo agentInfo)
        {
            _logger.LogInformation($"Вызван метод AgentsController.RegisterAgent с аргументом {agentInfo}");
            return Ok();
        }


        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById ([FromRoute] int agentId)
        {
            _logger.LogInformation($"Вызван метод AgentsController.EnableAgentById с аргументом {agentId}");
            return Ok();
        }


        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById ([FromRoute] int agentId)
        {
            _logger.LogInformation($"Вызван метод AgentsController.DisableAgentById с аргументом {agentId}");
            return Ok();
        }


        [HttpGet("agents")]
        public IActionResult GetAllAgents()
        {
            _logger.LogInformation($"Вызван метод AgentsController.GetAllAgents без аргументов");
            return Ok();
        }
    }
}
