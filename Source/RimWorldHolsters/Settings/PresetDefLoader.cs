using Holsters.Settings.PresetsLoading;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Holsters.Settings
{
    internal static class PresetDefLoader
    {
        public static IEnumerable<IPresetable> LoadPresets()
        {
            foreach (HolsterPresetDef presetDef in LoadDefs())
            {
                yield return ConvertPresetToSettings(presetDef);
            }
        }

        private static List<HolsterPresetDef> LoadDefs()
        {
            return GenDefDatabase.GetAllDefsInDatabaseForDef(typeof(HolsterPresetDef)).Cast<HolsterPresetDef>().ToList();
        }

        private static IPresetable ConvertPresetToSettings(HolsterPresetDef def)
        {
            return new HolsterDefPresetSetting(def);
        }
    }
}
