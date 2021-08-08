using System;

namespace MetricsManager.Requests
{
    public class GetAllCpuMetricsApiRequest
    {
        public string ClientBaseAddres { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }
    }
}
