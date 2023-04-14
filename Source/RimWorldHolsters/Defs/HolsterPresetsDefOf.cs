using RimWorld;

namespace Holsters.Defs
{
    [DefOf]
    public static class HolsterPresetsDefOf
    {
        public static HolsterPresetDef Holster_HolsteredOnBeltPreset;
        public static HolsterPresetDef Holster_HolsteredOnBeltAltPreset;
        public static HolsterPresetDef Holster_HangedOnBackPreset;
        public static HolsterPresetDef Holster_HangedOnArmPreset;
        public static HolsterPresetDef Holster_SheathedOnBeltAltPreset;
        public static HolsterPresetDef Holster_SheathedAtBeltPreset;


        public static HolsterPresetDef Holster_HangedOnAbdomenPreset;
        public static HolsterPresetDef Holster_OnChestPreset;
        public static HolsterPresetDef Holster_BowSheathedOnBackPreset;
        public static HolsterPresetDef Holster_BowHangedAroundPreset;
        public static HolsterPresetDef Holster_SheathedOnBackPreset;
        public static HolsterPresetDef Holster_SheathedOnBackAltPreset;

        static HolsterPresetsDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(HolsterPresetsDefOf));
        }
    }
}
