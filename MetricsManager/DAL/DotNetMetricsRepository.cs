using Dapper;
using MetricsManager.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsManager.DAL
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private string _connectionString;

        public DotNetMetricsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }


        public void Create(DotNetMetric item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute("INSERT INTO dotnetmetrics (value, time, agentid) VALUES (@value, @time, @agentid)",
                    new
                    {
                        value = item.Value,
                        time = item.Time,
                        agentid = item.AgentId
                    });
            };
        }


        public IList<DotNetMetric> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return connection.Query<DotNetMetric>("SELECT * FROM dotnetmetrics WHERE Time BETWEEN @fromTime AND @toTime",
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
                var item = connection.QuerySingle<double>("SELECT MAX(Time) FROM dotnetmetrics");
                return item;
            }
            catch (Exception)
            {

            }

            return TimeSpan.FromDays(1).TotalSeconds; ;
        }
    }
}
