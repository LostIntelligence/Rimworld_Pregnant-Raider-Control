using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace RaiderPregnancyControl
{
    // Module B
    [HarmonyPatch(typeof(IncidentWorker_RaidEnemy), "TryExecuteWorker")]
    public static class Patch_RaidEnemyPregnancy
    {
        static void Prefix(IncidentParms parms)
        {
            PregnancyRaidState.Active = true;
            PregnancyRaidState.ForceFemaleRaid = Rand.Value < RPC_Mod.settings.fullPregnantRaidChance;
            if (PregnancyRaidState.ForceFemaleRaid)
            {
                parms.sendLetter = false;
            }
        }

        static void Postfix(bool __result, IncidentParms parms)
        {
            if (__result && PregnancyRaidState.ForceFemaleRaid)
            {
                
                Map map = parms.target as Map;

                if (map != null)
                {
                    Find.LetterStack.ReceiveLetter(
                        "Pregnant Raider Assault",
                        "A group of heavily pregnant raiders is approaching your colony. Despite their condition, they seem determined to attack.",
                        LetterDefOf.ThreatBig,
                         new LookTargets(new GlobalTargetInfo(map.Center, map))
                    );
                }
            }

            PregnancyRaidState.Active = false;
            PregnancyRaidState.ForceFemaleRaid = false;
        }
    }
}