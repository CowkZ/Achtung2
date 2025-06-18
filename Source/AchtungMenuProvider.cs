using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;
using UnityEngine;

namespace AchtungMod
{
    public class AchtungMenuProvider : FloatMenuOptionProvider
    {
        public override bool Drafted => true;
        public override bool Undrafted => true;
        public override bool Multiselect => true;

        public override IEnumerable<FloatMenuOption> GetOptionsFor(Pawn clickedPawn, FloatMenuContext context)
        {
            var map = context.map;
            var clickedCell = context.ClickedCell;

            foreach (var option in GenerateAchtungWorkOptions(clickedPawn, map, clickedCell))
            {
                yield return option;
            }
        }

        private IEnumerable<FloatMenuOption> GenerateAchtungWorkOptions(Pawn pawn, Map map, IntVec3 cell)
        {
            if (!pawn.Spawned || pawn.Dead || pawn.Downed || pawn.InMentalState)
                yield break;

            var things = cell.GetThingList(map);

            foreach (WorkTypeDef workType in DefDatabase<WorkTypeDef>.AllDefsListForReading)
            {
                if (pawn.workSettings.GetPriority(workType) == 0)
                    continue;

                foreach (WorkGiverDef workGiverDef in workType.workGiversByPriority)
                {
                    var worker = workGiverDef.Worker;
                    if (worker == null)
                        continue;

                    Job job = null;

                    if (worker is WorkGiver_Scanner scanner)
                    {
                        foreach (Thing thing in things)
                        {
                            if (workGiverDef.scanThings)
                            {
                                job = scanner.JobOnThing(pawn, thing, forced: true);
                                if (job != null)
                                {
                                    yield return MakeOption(pawn, job, workGiverDef, thing.LabelCap);
                                }
                            }
                        }

                        if (workGiverDef.scanCells)
                        {
                            job = scanner.JobOnCell(pawn, cell, forced: true);
                            if (job != null)
                            {
                                yield return MakeOption(pawn, job, workGiverDef, $"célula {cell}");
                            }
                        }
                    }
                    else
                    {
                        job = worker.NonScanJob(pawn);
                        if (job != null)
                        {
                            yield return MakeOption(pawn, job, workGiverDef, workGiverDef.label);
                        }
                    }
                }
            }
        }

        private FloatMenuOption MakeOption(Pawn pawn, Job job, WorkGiverDef workGiverDef, string targetLabel)
        {
            Texture2D icon = ContentFinder<Texture2D>.Get("UI/Commands/ForceAttack", true);

            return new FloatMenuOption(
                $"⚡ Forçar {pawn.LabelShort} → {workGiverDef.label} em {targetLabel}",
                () => pawn.jobs.TryTakeOrderedJob(job),
                MenuOptionPriority.High,
                null,
                null,
                0f,
                null,
                null,
                icon
            );
        }
    }
}
