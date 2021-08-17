using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class AllHddMetricsApiResponse
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
