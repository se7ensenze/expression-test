using System.Collections.Concurrent;
using System.Reflection;

namespace App;

static class PropertyInfoCache
{
    private static readonly ConcurrentDictionary<Type, IEnumerable<PropertyInfo>> _cache = new();

    public static IEnumerable<PropertyInfo> GetCached(Type type)
    {
        return _cache.GetOrAdd(type, valueFactory: (t) =>
        {
            return t.GetProperties().Where(p => p.CanRead);
        });
    }
}
