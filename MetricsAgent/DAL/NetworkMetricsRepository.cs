using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.DAL
{


    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private string _connectionString = "DataSource=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        public NetworkMetricsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }


        public void Create(NetworkMetric item)
        {
            if (_connectionString != null)
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Execute("INSERT INTO networkmetrics (value, time) VALUES (@value, @time)",
                        new
                        {
                            value = item.Value,
                            time = item.Time
                        });
                };
            }
        }


        public IList<NetworkMetric> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            if (_connectionString != null)
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

            return null;            
        }
    }
}
