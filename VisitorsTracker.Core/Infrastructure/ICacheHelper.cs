using System;
using VisitorsTracker.Core.DTOs;

namespace VisitorsTracker.Core.Infrastructure
{
    public interface ICacheHelper
    {
        CacheDTO GetValue(Guid userId);

        bool Add(CacheDTO value);

        void Update(CacheDTO value);

        void Delete(Guid userId);
    }
}
