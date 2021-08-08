using Dapper;
using MetricsManager.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsManager.DAL
{
    public class AgentRepository : IAgentRepository
    {
        private string _connectionString;

        public AgentRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public void Create(AgentMetric item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute("INSERT INTO agents (agenturl, agentid) VALUES (@agenturl, @agentid)",
                    new
                    {
                        agenturl = item.AgentUrl,
                        agentid = item.AgentId
                    });
            }
        }

                
        public IList<AgentMetric> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            throw new NotImplementedException();
        }


        public IList<AgentMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return connection.Query<AgentMetric>("SELECT * FROM agents").ToList(); ;
            }                
        }


        public double GetLastTime()
        {
            throw new NotImplementedException();
        }
    }   
}
