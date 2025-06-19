using RimWorld;
using UnityEngine;
using Verse;

namespace AchtungMod
{
    [StaticConstructorOnStartup]
    public static class Controller
    {
        static Controller()
        {
            LongEventHandler.QueueLongEvent(InstallDefs, "LibraryStartup", false, null);
        }

        public static void InstallDefs()
        {
            Log.Message("Achtung Controller initialized for RimWorld 1.6");
        }

        public static void MouseDown()
        {
            if (Event.current.button == 1)
            {
                TryShowMenu();
            }
        }

        public static void TryShowMenu()
        {
            if (!UI.MouseCell().InBounds(Find.CurrentMap))
                return;

            var clickedCell = UI.MouseCell();
            var clickedMap = Find.CurrentMap;
            var selectedPawn = Find.Selector.SingleSelectedThing as Pawn;

            if (selectedPawn != null)
            {
                var multiActions = new MultiActions(clickedCell, clickedMap, selectedPawn);

                Find.WindowStack.Add(new FloatMenu(multiActions.GetFloatMenuOptions()));
            }
        }
    }
}