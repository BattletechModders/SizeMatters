using BattleTech;
using Harmony;
using SizeMatters.Helper;
using UnityEngine;

namespace SizeMatters.Patches
{
    [HarmonyPatch(typeof(ToHit), "GetAllModifiers")]
    public static class ToHit_GetAllModifiers
    {

        [HarmonyBefore(new string[] { "Sheepy.BattleTechMod.AttackImprovementMod" })]
        [HarmonyAfter(new string[] {
            "io.mission.modrepuation", "dZ.Zappo.Pilot_Quirks", "us.frostraptor.IRTweaks", "us.frostraptor.CBTBehaviorsEnhanced", "us.frostraptor.LowVisibility"
        })]
        private static void Postfix(ToHit __instance, ref float __result, AbstractActor attacker, Weapon weapon, ICombatant target,
            Vector3 attackPosition, Vector3 targetPosition, LineOfFireLevel lofLevel)
        {
            string cacheKey = StraightTonnageCalculator.CacheKey(attacker, target);
            bool keyExists = ModState.CachedComparisonMods.TryGetValue(cacheKey, out int modifier);
            if (!keyExists)
            {
                modifier = StraightTonnageCalculator.Modifier(attacker, target);
                ModState.CachedComparisonMods.Add(cacheKey, modifier);
            }
            
            __result += modifier;
        }
    }
}
