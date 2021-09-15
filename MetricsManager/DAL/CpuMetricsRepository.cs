using Dapper;
using MetricsManager.Models;
using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace MetricsManager.DAL
{
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private string _connectionString;

        public CpuMetricsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }


        public void Create(CpuMetric item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Execute("INSERT INTO cpumetrics (Value, Time, agentid) VALUES (@value, @time, @agentid)",
                    new
                    {
                        value = item.Value,
                        time = item.Time,
                        agentid = item.AgentId                        
                    });
            }
        }


        public IList<CpuMetric> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                var list = connection.Query<CpuMetric>("SELECT * FROM cpumetrics WHERE Time BETWEEN @fromTime AND @toTime",
                    new
                    {
                        fromTime = fromTime.TotalSeconds,
                        toTime = toTime.TotalSeconds
                    }).ToList();

                return list;
            }
        }


        /// <remarks>
        /// Если база агента не обновлялась, то прилетает исклюение. Для отладки соорудили это. 
        /// </remarks>
        /// <returns></returns>
        public double GetLastTime ()
        {
            using var connection = new SQLiteConnection(_connectionString);
            try
            {
                var item = connection.QuerySingle<double>("SELECT MAX(Time) FROM cpumetrics");
                return item;
            }
            catch (Exception)
            {

            }

            return TimeSpan.FromDays(1).TotalSeconds;
        }
    }
}
