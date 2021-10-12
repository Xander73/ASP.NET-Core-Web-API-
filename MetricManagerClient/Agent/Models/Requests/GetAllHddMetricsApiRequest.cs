using System;

namespace MetricsManagerClient.Requests
{
    public class GetAllHddMetricsApiRequest
    {
        public string ClientBaseAddres { get; set; }

        public TimeSpan FromTime { get; set; }

        public TimeSpan ToTime { get; set; }
    }
}
