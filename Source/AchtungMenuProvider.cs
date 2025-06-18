using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace AchtungMod
{
    public class AchtungMenuProvider : FloatMenuOptionProvider
    {
        public override bool ShouldShowFor(Pawn pawn)
        {
            return pawn != null && pawn.Map != null && !pawn.Drafted;
        }

        public override IEnumerable<FloatMenuOption> GetOptionsFor(Vector3 clickPos, Pawn pawn)
        {
            foreach (var option in Controller.AchtungChoicesAtFor(clickPos, pawn))
                yield return option;
        }
    }
}