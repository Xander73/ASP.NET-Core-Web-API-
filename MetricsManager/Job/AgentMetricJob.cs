using MetricsManager.Client.Interfaces;
using MetricsManager.DAL;
using MetricsManager.Models;
using MetricsManager.Requests;
using MyNamespace;
using Quartz;
using System;
using System.Threading.Tasks;

namespace MetricsManager.Job
{
    public class AgentMetricJob : IJob
    {
        private IMetricsAgentClient _metricsAgentClient;
        //private IClient _metricsAgentClient;
        private IAgentRepository _agentRepository;
        private ICpuMetricsRepository _cpuMetricsRepository;
        private IDotNetMetricsRepository _dotNetMetricsRepository;
        private IHddMetricsRepository _hddMetricsRepository;
        private INetworkMetricsRepository _networkMetricsRepository;
        private IRamMetricsRepository _ramMetricsRepository;

        public AgentMetricJob(
            IAgentRepository agentRepository,
            IMetricsAgentClient metricsAgentClient,
            //IClient metricsAgentClient,
            ICpuMetricsRepository cpuMetricsRepository,
            IDotNetMetricsRepository dotNetMetricsRepository,
            IHddMetricsRepository hddMetricsRepository,
            INetworkMetricsRepository networkMetricsRepository,
            IRamMetricsRepository ramMetricsRepository
            )
        {
            _metricsAgentClient = metricsAgentClient;
            _agentRepository = agentRepository;
            _cpuMetricsRepository = cpuMetricsRepository;
            _dotNetMetricsRepository = dotNetMetricsRepository;
            _hddMetricsRepository = hddMetricsRepository;
            _networkMetricsRepository = networkMetricsRepository;
            _ramMetricsRepository = ramMetricsRepository;
        }


        public Task Execute(IJobExecutionContext context)
        {
            
            foreach (var agent in _agentRepository.GetAll()) 
            {
                TransferMetricsFromAgentToManager(agent);
            }

            return Task.CompletedTask;
        }


        private void TransferMetricsFromAgentToManager(AgentMetric agent)
        {
            var requestCpu = new GetAllCpuMetricsApiRequest
            {
                ClientBaseAddres = agent.AgentUrl,
                FromTime = TimeSpan.FromSeconds(_cpuMetricsRepository.GetLastTime()),
                ToTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            };

            //_metricsAgentClient.ApiCpumetricsFromTo(requestCpu.FromTime.TotalSeconds.ToString(), requestCpu.ToTime.TotalSeconds.ToString());

            var cpuMetricsResponses = _metricsAgentClient.GetAllCpuMetrics(requestCpu); //получили метрики от агента

            if (cpuMetricsResponses?.Metrics != null)
            {
                foreach (var response in cpuMetricsResponses.Metrics)
                {
                    _cpuMetricsRepository.Create(new CpuMetric
                    {
                        Time = response.Time,
                        Value = response.Value,
                        AgentId = agent.Id,
                    });
                }
            }

            var requestDotNet = new GetAllDotNetMetricsApiRequest
            {
                ClientBaseAddres = agent.AgentUrl,
                FromTime = TimeSpan.FromSeconds(_dotNetMetricsRepository.GetLastTime()),
                ToTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            };

            //_metricsAgentClient.ApiDotnetmetricsFromTo(requestDotNet.FromTime.TotalSeconds.ToString(), requestDotNet.ToTime.TotalSeconds.ToString());

            var dotNetMetricsResponses = _metricsAgentClient.GetAllDotNetMetrics(requestDotNet); //получили метрики от агента

            if (dotNetMetricsResponses?.Metrics != null)
            {
                foreach (var response in dotNetMetricsResponses.Metrics)
                {
                    _dotNetMetricsRepository.Create(new DotNetMetric
                    {
                        Time = response.Time,
                        Value = response.Value,
                        AgentId = agent.Id,
                    });
                }
            }

            var requestHdd = new GetAllHddMetricsApiRequest
            {
                ClientBaseAddres = agent.AgentUrl,
                FromTime = TimeSpan.FromSeconds(_hddMetricsRepository.GetLastTime()),
                ToTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            };

            //_metricsAgentClient.ApiDotnetmetricsFromTo(requestHdd.FromTime.TotalSeconds.ToString(), requestHdd.ToTime.TotalSeconds.ToString());

            var hddMetricsResponses = _metricsAgentClient.GetAllHddMetrics(requestHdd); //получили метрики от агента

            if (hddMetricsResponses?.Metrics != null)
            {
                foreach (var response in hddMetricsResponses.Metrics)
                {
                    _hddMetricsRepository.Create(new HddMetric
                    {
                        Time = response.Time,
                        Value = response.Value,
                        AgentId = agent.Id,
                    });
                }
            }

            var requestNetwork = new GetAllNetworkMetricsApiRequest
            {
                ClientBaseAddres = agent.AgentUrl,
                FromTime = TimeSpan.FromSeconds(_networkMetricsRepository.GetLastTime()),
                ToTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            };

            //_metricsAgentClient.ApiDotnetmetricsFromTo(requestNetwork.FromTime.TotalSeconds.ToString(), requestNetwork.ToTime.TotalSeconds.ToString());

            var networkMetricsResponses = _metricsAgentClient.GetAllNetworkMetrics(requestNetwork); //получили метрики от агента

            if (networkMetricsResponses?.Metrics != null)
            {
                foreach (var response in networkMetricsResponses.Metrics)
                {
                    _networkMetricsRepository.Create(new NetworkMetric
                    {
                        Time = response.Time,
                        Value = response.Value,
                        AgentId = agent.Id,
                    });
                }
            }

            var requestRam = new GetAllRamMetricsApiRequest
            {
                ClientBaseAddres = agent.AgentUrl,
                FromTime = TimeSpan.FromSeconds(_ramMetricsRepository.GetLastTime()),
                ToTime = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            };

            //_metricsAgentClient.ApiDotnetmetricsFromTo(requestRam.FromTime.TotalSeconds.ToString(), requestRam.ToTime.TotalSeconds.ToString());

            var ramMetricsResponses = _metricsAgentClient.GetAllRamMetrics(requestRam); //получили метрики от агента

            if (ramMetricsResponses?.Metrics != null)
            {
                foreach (var response in ramMetricsResponses.Metrics)
                {
                    _ramMetricsRepository.Create(new RamMetric
                    {
                        Time = response.Time,
                        Value = response.Value,
                        AgentId = agent.Id,
                    });
                }
            }
        }
    }
}
