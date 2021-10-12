using System;
using System.Collections.Generic;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IRepository<T> where T:class
    {
        IList<T> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime);


        void Create(T item);
    }
}
