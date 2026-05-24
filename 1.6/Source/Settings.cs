using UnityEngine;
using Verse;

namespace RaiderPregnancyControl
{
    public class RPC_Settings : ModSettings
    {
        public float pregnancyChance = 0.15f; // default 15%
        public float fullPregnantRaidChance = 0.0f; // default 0%
        public int minAge = 18; // default 18
        public int maxAge = 40; // default 40

        public override void ExposeData()
        {
            Scribe_Values.Look(ref pregnancyChance, "pregnancyChance", 0.15f);
            Scribe_Values.Look(ref fullPregnantRaidChance, "fullPregnantRaidChance", 0.0f);
            Scribe_Values.Look(ref minAge, "minAge", 18);
            Scribe_Values.Look(ref maxAge, "maxAge", 40);
        }
    }

    public class RPC_Mod : Mod
    {
        public static RPC_Settings settings;

        public RPC_Mod(ModContentPack content) : base(content)
        {
            settings = GetSettings<RPC_Settings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard list = new Listing_Standard();
            list.Begin(inRect);

            // Module A
            list.Label("RPC_PregnancyChanceLabel".Translate() + $" ({(settings.pregnancyChance * 100f):0}%)");
            settings.pregnancyChance = list.Slider(settings.pregnancyChance, 0f, 1f);

            list.Gap();

            // Module A
            list.Label("RPC_MinAgeLabel".Translate() +settings.minAge);
            settings.minAge = (int)list.Slider(settings.minAge,0,999);

            list.Gap();

            // Module A
            list.Label("RPC_MaxAgeLabel".Translate() + settings.maxAge);
            settings.maxAge = (int)list.Slider(settings.maxAge, 0, 999);

            list.Gap();

            //Module B
            list.Label("RPC_FullPregnantRaidChanceLabel".Translate() + $" ({(settings.fullPregnantRaidChance * 100f):0}%)");
            settings.fullPregnantRaidChance = list.Slider(settings.fullPregnantRaidChance, 0f, 1f);


            list.End();
        }

        public override string SettingsCategory() => "Raider Pregnancy Control";
    }
}
