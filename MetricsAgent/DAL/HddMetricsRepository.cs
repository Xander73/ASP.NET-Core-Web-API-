using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.DAL
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
            if (_connectionString != null)
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Execute("INSERT INTO hddmetrics (value, time) VALUES (@value, @time)",
                        new
                        {
                            value = item.Value,
                            time = item.Time
                        });
                };
            }
        }


        public IList<HddMetric> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            if (_connectionString != null)
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    return connection.Query<HddMetric>("SELECT * FROM hddmetrics WHERE time BETWEEN @fromTime AND @toTime",
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
