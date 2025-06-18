using System.Collections.Generic;
using RimWorld;
using Verse;

namespace AchtungMod
{
    public class AchtungMenuProvider : FloatMenuOptionProvider
    {
        // Indica se o provider deve aparecer para pawns Drafted
        public override bool Drafted => true;

        // Indica se o provider deve aparecer para pawns Undrafted
        public override bool Undrafted => true;

        // Indica se suporta seleção múltipla
        public override bool Multiselect => true;

        // Decide quando deve fornecer opções (exemplo: só para colonistas humanos)
        public override bool ShouldProvideOptions(Pawn pawn, LocalTargetInfo target, bool drafted)
        {
            return pawn != null && pawn.IsColonistPlayerControlled;
        }

        // Define as opções que o Float Menu vai exibir
        public override IEnumerable<FloatMenuOption> GetOptions(Pawn pawn, LocalTargetInfo target, bool drafted)
        {
            yield return new FloatMenuOption(
                "Achtung Custom Action!",
                () =>
                {
                    Messages.Message($"Achtung: Opção clicada no pawn {pawn.LabelShort}.", MessageTypeDefOf.NeutralEvent);
                }
            );
        }
    }
}
