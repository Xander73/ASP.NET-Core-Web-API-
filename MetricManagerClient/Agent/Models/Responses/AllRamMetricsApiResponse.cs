using System.Collections.Generic;

namespace MetricsManagerClient.Responses
{
    public class AllRamMetricsApiResponse
    {
        public List<RamMetricDto> Metrics { get; set; }
    }


    public class RamMetricDto
    {
        public double Time { get; set; }

        public int Value { get; set; }

        public int Id { get; set; }
    }
}
