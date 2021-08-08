using Dapper;
using MetricsManager.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsManager.DAL
{
    public class HddMetricsRepository : IHddMetricsRepository
    {
        private string _connectionString;

        public HddMetricsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }


        public void Create(HddMetric item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute("INSERT INTO hddmetrics (value, time, agentid) VALUES (@value, @time, @agentid)",
                    new
                    {
                        value = item.Value,
                        time = item.Time,
                        agentid = item.AgentId
                    });
            };
        }


        public IList<HddMetric> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                return connection.Query<HddMetric>("SELECT * FROM hddmetrics WHERE time BETWEEN @fromTime AND @toTime",
                    new
                    {
                        fromTime = fromTime,
                        toTime = toTime
                    }).ToList();
            }
        }


        public double GetLastTime()
        {
            using var connection = new SQLiteConnection(_connectionString);
            try
            {
                var item = connection.QuerySingle<double>("SELECT MAX(Time) FROM hddmetrics");
                return item;
            }
            catch (Exception)
            {

            }

            return TimeSpan.FromDays(1).TotalSeconds;
        }
    }
}
