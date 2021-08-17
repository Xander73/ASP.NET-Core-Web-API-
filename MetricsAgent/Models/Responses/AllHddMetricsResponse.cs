using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class AllHddMetricsResponse
    {        
        public List<HddMetricDto> Metrics { get; set; }
    }

    public class HddMetricDto
    {
        public double Time { get; set; }

        public int Value { get; set; }

        public int Id { get; set; }
    }
}
