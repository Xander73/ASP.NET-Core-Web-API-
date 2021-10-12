using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.DAL
{


    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private string _connectionString;

        public CpuMetricsRepository(IConfiguration connectionString)
        {
            
            _connectionString = connectionString.GetConnectionString("DefaultConnection");
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }


        public void Create (CpuMetric item)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                if (_connectionString != null)
                {
                    connection.Execute("INSERT INTO cpumetrics (value, time) VALUES (@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time
                    });
                }       
            }            
        }


        public IList<CpuMetric> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            if (_connectionString != null)
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    var list = connection.Query<CpuMetric>("SELECT * FROM cpumetrics WHERE Time BETWEEN @fromTime AND @toTime",
                        new
                        {
                            fromTime = fromTime.TotalSeconds,
                            toTime = toTime.TotalSeconds
                        }).ToList(); ;
                    return list;
                }
            }

            return null;
        }
    }
}
