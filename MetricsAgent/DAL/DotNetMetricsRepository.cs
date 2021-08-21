using Dapper;
using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.DAL
{
    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private const string ConnectionString = "DataSource=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        public DotNetMetricsRepository ()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }


        public void Create(DotNetMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("INSERT INTO dotnetmetrics (value, time) VALUES (@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.TotalSeconds
                    });
            };
        }


        public void Delete(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("DELETE FROM dotnetmetrics WHERE id=@id",
                    new { id = id });
            }
        }


        public void Update(DotNetMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE dotnetmetrics SET value = @value, time = @time WHERE id=@id",
                    new
                    {
                        value = item.Value,
                        time = item.Time.TotalSeconds,
                        id = item.Id
                    });
            }
        }


        public IList<DotNetMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<DotNetMetric>("SELECT * from dotnetmetrics").ToList();
            }
        }


        public DotNetMetric GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<DotNetMetric>("SELECT * FROM dotnetmetrics WHERE id=@id",
                    new { id = id });
            }
        }


        public IList<DotNetMetric> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<DotNetMetric>("SELECT * FROM dotnetmetrics WHERE time BETWEEN @fromTime AND @toTime",
                    new
                    {
                        fromTime = fromTime,
                        toTime = toTime
                    }).ToList();
            }
        }
    }
}
