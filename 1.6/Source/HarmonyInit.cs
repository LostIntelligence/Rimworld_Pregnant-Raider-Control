using HarmonyLib;
using Verse;

namespace RaiderPregnancyControl
{
    [StaticConstructorOnStartup]
    public static class HarmonyInit
    {
        static HarmonyInit()
        {
            Harmony harmony = new Harmony(
                "RaiderPregnancyControl"
            );

            harmony.PatchAll();
        }
    }
}
