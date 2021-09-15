using System;
using System.Collections.Generic;

namespace MetricsManager.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetByTimePeriod(TimeSpan fromTime, TimeSpan toTime);




        void Create(T item);

                
        double GetLastTime();
    }
}
