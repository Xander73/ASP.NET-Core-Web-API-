using System.Collections.Generic;

namespace MetricsManagerClient.Responses
{
    public class AllCpuMetricsApiResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }


    public class CpuMetricDto
    {
        public double Time { get; set; }

        public int Value { get; set; }

        public int Id { get; set; }
    }
}
