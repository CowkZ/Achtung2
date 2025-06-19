using HarmonyLib;
using RimWorld;
using Verse;

namespace AchtungMod
{
    [HarmonyPatch(typeof(ForbidUtility), nameof(ForbidUtility.IsForbidden))]
    public static class ForbidUtility_IsForbidden_Patch
    {
        public static void Postfix(ref bool __result, Thing t, Pawn pawn)
        {
            if (Achtung.Settings.ignoreForbidden && pawn != null && t != null)
            {
                __result = false;
            }
        }
        public static void FixPatch()
        {
            if (Achtung.Settings.ignoreForbidden)
            {
                Achtung.harmony.Patch(
                    AccessTools.Method(typeof(ForbidUtility), nameof(ForbidUtility.IsForbidden)),
                    postfix: new HarmonyMethod(typeof(ForbidUtility_IsForbidden_Patch), nameof(Postfix))
                );
            }
            else
            {
                Achtung.harmony.Unpatch(
                    AccessTools.Method(typeof(ForbidUtility), nameof(ForbidUtility.IsForbidden)),
                    HarmonyPatchType.Postfix,
                    Achtung.harmony.Id
                );
            }
        }

    }
}