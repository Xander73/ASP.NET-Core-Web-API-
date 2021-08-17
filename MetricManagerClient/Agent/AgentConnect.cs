using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System;
using MetricsManagerClient.Models;
using MetricsManagerClient.Responses;
using MetricsManagerClient.Requests;
using MetricsManagerClient.Client.Interfaces;

namespace MetricManagerClient.Agent
{
    public class AgentConnect
    {
        private static AgentMetric _agent = new AgentMetric(); 
        public AgentConnect(string[] args)
        {
            _agent.AgentUrl = "https://localhost:44346" /*ConfigurationManager.ConnectionStrings["BaseUrl"].ConnectionString*/;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               });

        public static AllCpuMetricsApiResponse GetCpuMetrics (double fromTime, IMetricsAgentClient _metricsAgentClient)
        {
            var requestCpu = new GetAllCpuMetricsApiRequest
            {
                ClientBaseAddres = _agent.AgentUrl,
                FromTime = TimeSpan.FromSeconds(fromTime),
                ToTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            };

            return _metricsAgentClient.GetAllCpuMetrics(requestCpu); //получили метрики от агента
        }


        public static AllDotNetMetricsApiResponse GetDotNetMetrics(double fromTime, IMetricsAgentClient _metricsAgentClient)
        {
            var requestDotNet = new GetAllDotNetMetricsApiRequest
            {
                ClientBaseAddres = _agent.AgentUrl,
                FromTime = TimeSpan.FromSeconds(fromTime),
                ToTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            };

            return _metricsAgentClient.GetAllDotNetMetrics(requestDotNet); //получили метрики от агента
        }


        public static AllHddMetricsApiResponse GetHddMetrics(double fromTime, IMetricsAgentClient _metricsAgentClient)
        {
            var requestHdd = new GetAllHddMetricsApiRequest
            {
                ClientBaseAddres = _agent.AgentUrl,
                FromTime = TimeSpan.FromSeconds(fromTime),
                ToTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            };

            return _metricsAgentClient.GetAllHddMetrics(requestHdd); //получили метрики от агента
        }


        public static AllNetworkMetricsApiResponse GetNetworkMetrics(double fromTime, IMetricsAgentClient _metricsAgentClient)
        {
            var requestNetwork = new GetAllNetworkMetricsApiRequest
            {
                ClientBaseAddres = _agent.AgentUrl,
                FromTime = TimeSpan.FromSeconds(fromTime),
                ToTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            };

            return _metricsAgentClient.GetAllNetworkMetrics(requestNetwork); //получили метрики от агента
        }


        public static AllRamMetricsApiResponse GetRamMetrics(double fromTime, IMetricsAgentClient _metricsAgentClient)
        {
            var requestRam = new GetAllRamMetricsApiRequest
            {
                ClientBaseAddres = _agent.AgentUrl,
                FromTime = TimeSpan.FromSeconds(fromTime),
                ToTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            };

            return _metricsAgentClient.GetAllRamMetrics(requestRam); //получили метрики от агента
        }
    }
}
