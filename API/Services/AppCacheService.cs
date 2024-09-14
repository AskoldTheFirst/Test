using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database;
using API.Database.Entities;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;

namespace API.Services
{
    public class AppCacheService
    {
        TestDbContext _ctx;
        IMemoryCache _cache;

        public AppCacheService(TestDbContext context, IMemoryCache memoryCache)
        {
            _ctx = context;
            _cache = memoryCache;
        }

        public int[] GetQuestionIds(string technologyName)
        {
            technologyName = technologyName.ToLower();
            string key = $"ids-{technologyName}";
            int[] ids;

            if (!_cache.TryGetValue<int[]>(key, out ids))
            {
                ids = (from t in _ctx.Technologies
                       join q in _ctx.Questions on t.Id equals q.TechnologyId
                       where t.Name.ToLower() == technologyName
                       select q.Id).ToArray();

                _cache.Set(key, ids);
            }

            return ids;
        }

        public Technology[] GetEntiryTechnology()
        {
            string key = $"technologyDbTable";
            Technology[] technologies;

            if (!_cache.TryGetValue(key, out technologies))
            {
                technologies = (from t in _ctx.Technologies where t.IsActive select t).ToArray();

                _cache.Set(key, technologies);
            }

            return technologies;
        }
    }
}