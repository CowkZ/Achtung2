using System.Collections.Generic;
using RimWorld;
using Verse;

namespace AchtungMod
{
    public class AchtungMenuProvider : FloatMenuOptionProvider
    {
        public override bool Drafted => true;
        public override bool Undrafted => true;
        public override bool Multiselect => true;

        public override IEnumerable<FloatMenuOption> GetOptionsFor(Pawn clickedPawn, FloatMenuContext context)
        {
            // LINHA DE TESTE ADICIONADA PARA DEBUG
            yield return new FloatMenuOption("--- TESTE ACHTUNG PROVIDER ---", () => { });

            // Seu c�digo original para carregar as a��es
            var multiActions = new MultiActions(context.ClickedCell, context.map, clickedPawn);
            foreach (FloatMenuOption option in multiActions.GetFloatMenuOptions())
            {
                yield return option;
            }
        }
    }
}