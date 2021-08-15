using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class AllAgentMetricsApiResponse
    {
        public List<AgentMetricDto> Metrics { get; set; }
    }


    public class AgentMetricDto
    {
        public string AgentUrl { get; set; }

        public int AgentId { get; set; }

        public int Id { get; set; }
    }
}
