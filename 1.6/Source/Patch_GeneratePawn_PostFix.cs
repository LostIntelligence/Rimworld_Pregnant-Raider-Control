using HarmonyLib;
using RimWorld;
using Verse;

namespace RaiderPregnancyControl
{
    // Module A
    [HarmonyPatch( typeof(PawnGenerator), nameof(PawnGenerator.GeneratePawn),new[] { typeof(PawnGenerationRequest) } )]
    public static class Patch_GeneratePawn_PostFix
    {
        public static void Postfix(ref Pawn __result)
        {
            // World Gen
            if (Find.FactionManager == null)
                return;

            if (Find.FactionManager.OfPlayer == null)
                return;

            Pawn pawn = __result;

            // Only hostile raiders
            if (pawn?.Faction == null || !pawn.Faction.HostileTo(Faction.OfPlayer))
                return;

            // Only humanlike females
            if (!pawn.RaceProps.Humanlike || pawn.gender != Gender.Female)
                return;

            // Fertile age range
            int age = pawn.ageTracker.AgeBiologicalYears;
            if (age < RPC_Mod.settings.minAge || age > RPC_Mod.settings.maxAge)
                return;

            if (RPC_Mod.settings.pregnancyChance == 0.0f) 
            {
                if (pawn.health.hediffSet.HasHediff(HediffDefOf.PregnantHuman))
                {
                    Hediff pregnancy = HediffMaker.MakeHediff(HediffDefOf.PregnantHuman, pawn);
                    pawn.health.RemoveHediff(pregnancy);
                }
            }
            else
            {
                // Roll chance from settings
                if (Rand.Value > RPC_Mod.settings.pregnancyChance)
                    return;

                // Apply pregnancy
                if (!pawn.health.hediffSet.HasHediff(HediffDefOf.PregnantHuman))
                {
                    Hediff pregnancy = HediffMaker.MakeHediff(HediffDefOf.PregnantHuman, pawn);
                    pawn.health.AddHediff(pregnancy);
                }
            }
        }
    }
}
