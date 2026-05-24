using HarmonyLib;
using RimWorld;
using Verse;

namespace RaiderPregnancyControl
{
    //Module B
    [HarmonyPatch(typeof(PawnGenerator), nameof(PawnGenerator.GeneratePawn), new[] { typeof(PawnGenerationRequest) })]
    public static class Patch_PawnGenerator_GeneratePawn
    {
        public static void Prefix(ref PawnGenerationRequest request)
        {
            if (!PregnancyRaidState.Active)
                return;

            if (!PregnancyRaidState.ForceFemaleRaid)
                return;

            if (request.Faction == null)
                return;

            if (Find.FactionManager == null)
                return;

            if (Find.FactionManager.OfPlayer == null)
                return;

            if (!request.Faction.HostileTo(Faction.OfPlayer))
                return;

            request.FixedGender = Gender.Female;
            request.AllowPregnant = true;
            request.CanGeneratePawnRelations = false;
        }

        public static void Postfix(ref Pawn __result)
        {
            if (!PregnancyRaidState.Active)
                return;

            if (!PregnancyRaidState.ForceFemaleRaid)
                return;

            if (Find.FactionManager == null)
                return;

            if (Find.FactionManager.OfPlayer == null)
                return;

            Pawn pawn = __result;

            if (pawn == null)
                return;

            if (!pawn.RaceProps.Humanlike)
                return;

            if (!pawn.health.hediffSet.HasHediff(HediffDefOf.PregnantHuman))
            {
                Hediff h = HediffMaker.MakeHediff(HediffDefOf.PregnantHuman, pawn);

                h.Severity = Rand.Range(0.01f, 0.8f);

                pawn.health.AddHediff(h);
            }
        }
    }
}
