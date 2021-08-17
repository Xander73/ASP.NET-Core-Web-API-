using System;

namespace MetricsManagerClient.Requests
{
    public class GetAllNetworkMetricsApiRequest
    {
        public string ClientBaseAddres { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }
    }
}
