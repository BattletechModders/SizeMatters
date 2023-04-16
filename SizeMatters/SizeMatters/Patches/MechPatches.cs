using SizeMatters.Helper;
using System.Collections.Generic;

namespace SizeMatters.Patches
{
    [HarmonyPatch(typeof(Mech), "InitEffectStats")]
    public static class Mech_InitEffectStats
    {
        private static void Postfix(Mech __instance)
        {
            foreach (KeyValuePair<string, float> entry in Mod.Config.StatisticsToAddPerTon)
            {
                float tonnage = StraightTonnageCalculator.GetTonnage(__instance);
                float stat = 1 + entry.Value * tonnage;
                __instance.StatCollection.Set(entry.Key, stat);

                Mod.Log.Info?.Write($"Applying {entry.Key} {stat} to {__instance.UnitName}");
            }
        }
    }
}
