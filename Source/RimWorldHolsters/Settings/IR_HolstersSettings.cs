using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Holsters.Settings
{
    public sealed class IR_HolstersSettings : ModSettings
    {
        private static PresetsContainer _presets;

        private const float FORWARD_DRAW_POSITION = 0.026f;
        private const float MIDDLE_DRAW_POSITION = 0.0128957527f;
        private const float BACK_DRAW_POSITION = -0.0128957527f;

        public static float BackLayerOffset = 0;
        public static float FrontLayerOffset = 0;

        public static bool DisplayIndoors = true;

        public IR_HolstersSettings() { }

        public static float FrontDrawPosition => FORWARD_DRAW_POSITION + FrontLayerOffset;

        public static float BackDrawPosition => BACK_DRAW_POSITION + BackLayerOffset;

        public static void Initialise()
        {
            List<IPresetable> presetables = PresetDefLoader.LoadPresets().ToList();

            if (_presets == null)
            {
                _presets = new PresetsContainer(presetables);
                Log.Message("Holsters: initialised presets");
            }
            else
            {
                _presets.AddLoadedDefs(presetables);
                Log.Message("Holsters: loaded presets");
            }
        }

        public static IEnumerable<IPresetable> Holsters()
        {
            foreach (IPresetable configuration in _presets.GetPresets())
            {
                yield return configuration;
            }
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref BackLayerOffset, "backLayerOffset", 0);
            Scribe_Values.Look(ref FrontLayerOffset, "frontLayerOffset", 0);
            Scribe_Values.Look(ref DisplayIndoors, "displayIndoors", true);
            Scribe_Deep.Look(ref _presets, "presets2");

            base.ExposeData();
        }

        public static HolsterConfiguration GetHolsterConfigurationFor(ThingDef def, Rot4 rot)
        {
            return GetConfiguration(def).Configuration[rot];
        }

        private static HolsterPresetDef GetConfiguration(ThingDef def)
        {
            return EquipmentPresetSorter.SortWeaponsIntoGroups(def);
        }


    }
}



