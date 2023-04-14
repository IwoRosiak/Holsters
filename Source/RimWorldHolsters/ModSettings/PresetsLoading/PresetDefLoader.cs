using System.Collections.Generic;
using Verse;

namespace Holsters.Utility.ModSettings.PresetsLoading
{
    internal static class PresetDefLoader
    {
        public static IEnumerable<IPresetable> LoadPresets()
        {
            foreach(HolsterPresetDef presetDef in LoadDefs())
            {
                yield return Wrap(presetDef);
            }
        }

        private static List<HolsterPresetDef> LoadDefs()
        {
            List<HolsterPresetDef> allDefs = new List<HolsterPresetDef>();

            foreach (HolsterPresetDef def in GenDefDatabase.GetAllDefsInDatabaseForDef(typeof(HolsterPresetDef)))
            {
                allDefs.Add(def);
            }

            return allDefs;
        }

        private static IPresetable Wrap(HolsterPresetDef def)
        {
            HolsterDefPresetSetting setting = new HolsterDefPresetSetting(def);

            return setting;
        }
    }
}
