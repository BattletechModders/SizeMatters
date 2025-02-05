using SizeMatters.Helper;
using UnityEngine;

namespace SizeMatters.Patches
{

    [HarmonyPatch(typeof(Weapon), "InitStats")]
    public static class Weapon_InitStats
    {
        private static void Postfix(Weapon __instance)
        {
            var ignoresSizeMods = __instance.weaponDef != null && __instance.weaponDef.ComponentTags != null &&
                                  __instance.weaponDef.ComponentTags.Contains(Mod.Config.IgnoreSizeModifierTag);

            __instance.statCollection.AddStatistic<bool>(ModStats.WeaponIgnoresSizeStat, ignoresSizeMods);
            __instance.IgnoresSizeModifiers = ignoresSizeMods;
        }
    }
    
    [HarmonyPatch(typeof(Weapon), "Hydrate")]
    public static class Weapon_Hydrate
    {
        public static void Postfix(Weapon __instance)
        {
            __instance.IgnoresSizeModifiers = __instance.StatCollection.GetValue<bool>(ModStats.WeaponIgnoresSizeStat);
        }
    }
        
    
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
            if (weapon.IgnoresSizeModifiers)
            {
                return;
            }
            var cacheKey = new SizeComparisionCacheKey(attacker, target);
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
