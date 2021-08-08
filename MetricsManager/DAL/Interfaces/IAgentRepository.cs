using MetricsManager.DAL.Interfaces;
using MetricsManager.Models;
using System.Collections.Generic;

namespace MetricsManager.DAL
{
    public interface IAgentRepository : IRepository<AgentMetric>
    {
        IList<AgentMetric> GetAll();
    }
}
