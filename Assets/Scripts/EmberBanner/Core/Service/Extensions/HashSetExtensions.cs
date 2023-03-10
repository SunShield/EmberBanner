using System.Collections.Generic;

namespace EmberBanner.Core.Service.Extensions
{
    public static class HashSetExtensions
    {
        public static void SafeAdd<T>(this HashSet<T> hs, T value)
        {
            if (hs.Contains(value)) return;
            hs.Add(value);
        }
    }
}