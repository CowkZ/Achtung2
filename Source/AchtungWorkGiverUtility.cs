using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;
using UnityEngine;

namespace AchtungMod
{
    public static class AchtungWorkGiverUtility
    {
        public static List<FloatMenuOption> GetWorkOptionsFor(Pawn pawn, IntVec3 cell, Map map)
        {
            var options = new List<FloatMenuOption>();
            var things = cell.GetThingList(map);

            foreach (WorkTypeDef workType in DefDatabase<WorkTypeDef>.AllDefsListForReading)
            {
                if (!Achtung.Settings.ignoreAssignments && pawn.workSettings.GetPriority(workType) == 0)
                    continue;

                if (pawn.WorkTypeIsDisabled(workType))
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
                                if (Achtung.Settings.ignoreForbidden == false && thing.IsForbidden(pawn))
                                    continue;

                                job = scanner.JobOnThing(pawn, thing, forced: true);
                                if (job != null)
                                {
                                    options.Add(CreateMenuOption(pawn, job, workGiverDef, thing.LabelCap));
                                }
                            }
                        }

                        if (workGiverDef.scanCells)
                        {
                            job = scanner.JobOnCell(pawn, cell, forced: true);
                            if (job != null)
                            {
                                options.Add(CreateMenuOption(pawn, job, workGiverDef, $"célula {cell}"));
                            }
                        }
                    }
                    else
                    {
                        job = worker.NonScanJob(pawn);
                        if (job != null)
                        {
                            options.Add(CreateMenuOption(pawn, job, workGiverDef, workGiverDef.label));
                        }
                    }
                }
            }

            return options;
        }

        private static FloatMenuOption CreateMenuOption(Pawn pawn, Job job, WorkGiverDef workGiverDef, string targetLabel)
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