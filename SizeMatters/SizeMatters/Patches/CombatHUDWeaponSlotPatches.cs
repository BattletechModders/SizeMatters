﻿using BattleTech;
using BattleTech.UI;
using Harmony;
using SizeMatters.Helper;
using System;

namespace SizeMatters.Patches
{
    [HarmonyPatch(typeof(CombatHUDWeaponSlot), "SetHitChance")]
    [HarmonyPatch(new Type[] { typeof(ICombatant) })]
    [HarmonyBefore("us.frostraptor.CBTBehaviorsEnhanced", "dZ.Zappo.Pilot_Quirks")]
    public static class CombatHUDWeaponSlot_SetHitChance
    {

        private static void Postfix(CombatHUDWeaponSlot __instance, ICombatant target, Weapon ___displayedWeapon, CombatHUD ___HUD)
        {
            if (__instance == null || ___displayedWeapon == null || ___HUD.SelectedActor == null || target == null) { return; }

            Mod.Log.Trace?.Write("CHUDWS:SHC - entered.");

            Traverse AddToolTipDetailMethod = Traverse.Create(__instance).Method("AddToolTipDetail",
                new Type[] { typeof(string), typeof(int) });

            AbstractActor attacker = __instance.DisplayedWeapon.parent;
            string cacheKey = StraightTonnageCalculator.CacheKey(attacker, target);
            bool keyExists = ModState.CachedComparisonMods.TryGetValue(cacheKey, out int modifier);
            if (!keyExists)
            {
                modifier = StraightTonnageCalculator.Modifier(attacker, target);
                ModState.CachedComparisonMods.Add(cacheKey, modifier);
            }

            string localText = new Localize.Text(Mod.LocalizedText.Label[ModText.LT_AttackModSizeDelta]).ToString();
            AddToolTipDetailMethod.GetValue(new object[] { localText, modifier });
        }
    }
}
