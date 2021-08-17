using System.Collections.Generic;

namespace MetricsManagerClient.Responses
{
    public class AllNetworkMetricsApiResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }


    public class NetworkMetricDto
    {
        public double Time { get; set; }

        public int Value { get; set; }

        public int Id { get; set; }
    }
}
