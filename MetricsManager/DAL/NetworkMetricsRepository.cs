using Dapper;
using MetricsManager.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsManager.DAL
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private string _connectionString;

        public NetworkMetricsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }


        public void Create(NetworkMetric item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute("INSERT INTO networkmetrics (value, time, agentid) VALUES (@value, @time, @agentid)",
                    new
                    {
                        value = item.Value,
                        time = item.Time,
                        agentid = item.AgentId
                    });
            };
        }


        public IList<NetworkMetric> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return connection.Query<NetworkMetric>("SELECT * FROM networkmetrics WHERE time BETWEEN @fromTime AND @toTime",
                    new
                    {
                        fromTime = fromTime.TotalSeconds,
                        toTime = toTime.TotalSeconds
                    }).ToList();
            }
        }


        public double GetLastTime()
        {
            using var connection = new SQLiteConnection(_connectionString);
            try
            {
                var item = connection.QuerySingle<double>("SELECT MAX(Time) FROM networkmetrics");
                return item;
            }
            catch (Exception)
            {

            }

            return TimeSpan.FromDays(1).TotalSeconds;
        }
    }
}
