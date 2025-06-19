using System.Collections.Generic;
using RimWorld;
using Verse;

namespace AchtungMod
{
    public class MultiActions
    {
        private readonly List<FloatMenuOption> options = new List<FloatMenuOption>();

        public MultiActions(IntVec3 clickPos, Map map, Pawn singlePawn)
        {
            if (singlePawn != null)
            {
                GenerateOptionsForPawn(singlePawn, clickPos, map);
            }
        }

        public List<FloatMenuOption> GetFloatMenuOptions()
        {
            return options;
        }

        private void GenerateOptionsForPawn(Pawn pawn, IntVec3 clickPos, Map map)
        {
            foreach (FloatMenuOption option in AchtungWorkGiverUtility.GetWorkOptionsFor(pawn, clickPos, map))
            {
                options.Add(option);
            }
        }
    }
}