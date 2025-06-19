using HarmonyLib;
using Verse;

namespace AchtungMod
{
    [StaticConstructorOnStartup]
    public static class AchtungLoader
    {
        static AchtungLoader()
        {
            var harmony = new Harmony("net.pardeike.rimworld.mods.achtung");
            harmony.PatchAll();
        }
    }
}