using BattleTech;
using Harmony;
using IRBTModUtils;

namespace SizeMatters.Patches
{
    [HarmonyPatch(typeof(CombatGameState), "OnCombatGameDestroyed")]
    static class CombatGameState_OnCombatGameDestroyed
    {
        static void Postfix()
        {
            Mod.Log.Trace?.Write("CGS:OCGD - entered.");

            ModState.Reset();
        }
    }
}
