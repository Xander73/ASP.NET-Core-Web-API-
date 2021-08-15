using System;

namespace MetricsManager.Requests
{
    public class GetAllDotNetMetricsApiRequest
    {
        public string ClientBaseAddres { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }
    }
}
