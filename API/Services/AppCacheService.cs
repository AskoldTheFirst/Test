using API.Database;
using Microsoft.Extensions.Caching.Memory;

namespace API.Services
{
    public interface IMemoryCacheService
    {
        int[] GetQuestionIds(string technologyName);

        Technology[] GetTechnologies();

        Technology GetTechnologyByName(string name);

        Technology GetTechnologyById(int id);
    }

    public class AppCacheService : IMemoryCacheService
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

        public Technology[] GetTechnologies()
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

        public Technology GetTechnologyByName(string name)
        {
            Technology[] technologies = GetTechnologies();
            for(int i = 0 ; i < technologies.Length; ++i)
                if (String.Compare(technologies[i].Name, name, true) == 0)
                    return technologies[i];
            
            return null;
        }

        public Technology GetTechnologyById(int id)
        {
            Technology[] technologies = GetTechnologies();
            for(int i = 0 ; i < technologies.Length; ++i)
                if (technologies[i].Id == id)
                    return technologies[i];
            
            return null;
        }
    }
}