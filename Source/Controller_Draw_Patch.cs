using HarmonyLib;
using RimWorld;
using Verse;

namespace AchtungMod
{
    [HarmonyPatch(typeof(MapInterface), "DrawSelectionOverlays")]
    static class Controller_Draw_Patch
    {
        static void Prefix()
        {
            //talvez seja util
        }
    }
}