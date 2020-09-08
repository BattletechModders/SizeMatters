using BattleTech;
using HBS.Collections;
using IRBTModUtils.Extension;
using System;

namespace SizeMatters.Helper
{
    public static class StraightTonnageCalculator
    {
        public static string CacheKey(AbstractActor attacker, ICombatant target)
        {
            return $"{attacker.DistinctId()}-{target.DistinctId()}";
        }

        public static int Modifier(AbstractActor attacker, ICombatant target)
        {
            int modifier = 0;

            try
            {
                float attackerTonnage = GetTonnage(attacker);                
                float targetTonnage = GetTonnage(target);
                float tonnageDelta = attackerTonnage - targetTonnage;
                Mod.Log.Debug?.Write($"TonnageDelta: {tonnageDelta} from attackerTonnage: {attackerTonnage} - targetTonnage: {targetTonnage}");

                float tonnageFraction = tonnageDelta / Mod.Config.TonnageDivisor;
                Mod.Log.Debug?.Write($"Tonnage fraction: {tonnageFraction} = tonnageDelta: {tonnageDelta} / tonnageDiv: {Mod.Config.TonnageDivisor}");

                // a 20 vs t 100 => 20 - 100 = -80
                // a 100 vs t 20 => 100 - 20 = 80
                // a 50 vs t 50 => 0

                float rawMod = tonnageFraction * Mod.Config.TonnageMulti;
                if (rawMod < 0)
                {
                    // Lighter unit attacking a heavier unit
                    modifier = (int) Math.Ceiling(rawMod);
                    Mod.Log.Debug?.Write($"RawMod: {rawMod} => ceiling = {modifier}");
                }
                else if (rawMod > 0)
                {
                    // Heavier unit attacking a lighter unit
                    modifier = (int) Math.Floor(rawMod);
                    Mod.Log.Debug?.Write($"RawMod: {rawMod} => floor = {modifier}");
                }
                else
                {
                    modifier = 0;
                }
            }
            catch (Exception e)
            {
                Mod.Log.Warn?.Write(e, "Failed to calculate tonnage delta modifier!");
            }

            return modifier;
        }

        private static float GetTonnage(ICombatant combatant)
        {
            float tonnage = 0f;
            if (combatant == null)
            {
                Mod.Log.Debug?.Write($"Combatant is null, using tonnage of 0!");
            }
            else if (combatant is BattleTech.Building)
            {
                Mod.Log.Debug?.Write($"Using virtual tonnage: {Mod.Config.VirtualTonnage.Building} for building: {combatant.DistinctId()}");
                return Mod.Config.VirtualTonnage.Building;
            }
            else if (combatant is Turret turret)
            {
                TagSet actorTags = turret.GetTags();
                if (actorTags != null && actorTags.Contains("unit_light"))
                {
                    tonnage = Mod.Config.VirtualTonnage.LightTurret;
                }
                else if (actorTags != null && actorTags.Contains("unit_medium"))
                {
                    tonnage = Mod.Config.VirtualTonnage.MediumTurret;
                }
                else if (actorTags != null && actorTags.Contains("unit_heavy"))
                {
                    tonnage = Mod.Config.VirtualTonnage.HeavyTurret;
                }
                else
                {
                    tonnage = Mod.Config.VirtualTonnage.DefaultTurret;
                }
                Mod.Log.Debug?.Write($"Using virtual tonnage: {tonnage} for turret: {turret.DistinctId()}");
                return tonnage;
            }
            else if (combatant is Mech mech)
            {
                Mod.Log.Debug?.Write($"Using tonnage: {mech.tonnage} for mech: {mech.DistinctId()}");
                return mech.tonnage;
            }
            else if (combatant is Vehicle vehicle)
            {
                Mod.Log.Debug?.Write($"Using tonnage: {vehicle.tonnage} for vehicle: {vehicle.DistinctId()}");
                return vehicle.tonnage;
            }

            return tonnage;
        }
    }
}
