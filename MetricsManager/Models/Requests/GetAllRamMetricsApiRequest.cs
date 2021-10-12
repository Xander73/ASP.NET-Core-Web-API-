using System;

namespace MetricsManager.Requests
{
    public class GetAllRamMetricsApiRequest
    {
        public string ClientBaseAddres { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }
    }
}
