using HarmonyLib;
using System.Linq;

namespace AchtungMod
{
    public static class Achtung
    {
        public static Harmony harmony;
        public static AchtungSettings Settings;
        public static bool IsSameSpotInstalled = false;

        static Achtung()
        {
            Settings = new AchtungSettings();

            harmony = new Harmony("net.pardeike.rimworld.mods.achtung");
            harmony.PatchAll();

            const string sameSpotId = "net.pardeike.rimworld.mod.samespot";
            IsSameSpotInstalled = harmony.GetPatchedMethods()
                .Any(method => Harmony.GetPatchInfo(method)?.Transpilers.Any(t => t.owner == sameSpotId) ?? false);
        }
    }
}