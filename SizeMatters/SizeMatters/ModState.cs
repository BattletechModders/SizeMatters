
using System.Collections.Generic;

namespace SizeMatters
{

    public static class ModState
    {
        public static Dictionary<string, int> CachedComparisonMods = new Dictionary<string, int>() { };

        public static void Reset()
        {
            // Reinitialize state
            CachedComparisonMods.Clear();
        }
    }

}


