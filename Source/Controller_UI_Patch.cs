using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace AchtungMod
{
    [HarmonyPatch(typeof(UIRoot_Entry), "UIRootOnGUI")]
    static class Controller_UI_Patch_Entry
    {
        static void Prefix()
        {
            if (Event.current.type == UnityEngine.EventType.MouseDown && Event.current.button == 1)
            {
                Controller.MouseDown();
            }
        }
    }

    [HarmonyPatch(typeof(UIRoot_Play), "UIRootOnGUI")]
    static class Controller_UI_Patch_Play
    {
        static void Prefix()
        {
            if (Event.current.type == UnityEngine.EventType.MouseDown && UnityEngine.Event.current.button == 1)
            {
                Controller.MouseDown();
            }
        }
    }
}