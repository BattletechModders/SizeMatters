using System.Collections.Generic;

namespace SizeMatters
{
    public class VirtualTonnage
    {
        public float LightTurret = 40f;
        public float MediumTurret = 60f;
        public float HeavyTurret = 80f;
        public float DefaultTurret = 100f;

        public float Building = 60f;
    }

    public class ModConfig
    {
        public bool Debug = false;
        public bool Trace = false;

        public float TonnageDivisor = 5.0f;
        public int ModifierCap = 5;

        public float TonnageCapMin = 20f;
        public float TonnageCapMax = 150f;

        public VirtualTonnage VirtualTonnage = new VirtualTonnage();

        public Dictionary<string, float> StatisticsToAddPerTon = new Dictionary<string, float>();
        public string IgnoreSizeModifierTag = "IgnoreSizeMatters";

        public void LogConfig()
        {
            Mod.Log.Info?.Write("=== MOD CONFIG BEGIN ===");
            Mod.Log.Info?.Write($"  DEBUG: {this.Debug} Trace: {this.Trace}");
            Mod.Log.Info?.Write($"");
            Mod.Log.Info?.Write($"  TonnageDivisor: {this.TonnageDivisor}  TonnageCap - Min: {this.TonnageCapMin}  Max: {this.TonnageCapMax}  ModifierCap: {this.ModifierCap} IgnoreTag: {this.IgnoreSizeModifierTag}");
            Mod.Log.Info?.Write($"  Virtual Tonnage - Turrets => light: {this.VirtualTonnage.LightTurret}  medium: {this.VirtualTonnage.MediumTurret}  heavy: {this.VirtualTonnage.HeavyTurret}");
            Mod.Log.Info?.Write($"  Virtual Tonnage - Building: {this.VirtualTonnage.Building}");
            Mod.Log.Info?.Write($"  StatisticsToAddPerTon:");
            foreach (string stat in this.StatisticsToAddPerTon.Keys)
            {
                Mod.Log.Info?.Write($"    {stat}: {this.StatisticsToAddPerTon[stat]}");
            }

            Mod.Log.Info?.Write("=== MOD CONFIG END ===");
        }

        public void Init()
        {

        }
    }
}

