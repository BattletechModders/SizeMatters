
using System.Collections.Generic;

namespace SizeMatters
{

    internal record struct SizeComparisionCacheKey(AbstractActor actor, ICombatant target);

    public static class ModState
    {
        internal static Dictionary<SizeComparisionCacheKey, int> CachedComparisonMods = new Dictionary<SizeComparisionCacheKey, int>() { };

        public static void Reset()
        {
            // Reinitialize state
            CachedComparisonMods.Clear();
        }
    }

}


