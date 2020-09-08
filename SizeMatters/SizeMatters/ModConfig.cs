
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
        public float TonnageMulti = 0.5f;

        public VirtualTonnage VirtualTonnage = new VirtualTonnage();

        public void LogConfig()
        {
            Mod.Log.Info?.Write("=== MOD CONFIG BEGIN ===");
            Mod.Log.Info?.Write($"  DEBUG: {this.Debug} Trace: {this.Trace}");
            Mod.Log.Info?.Write("=== MOD CONFIG END ===");
        }

        public void Init()
        {

        }
    }
}

