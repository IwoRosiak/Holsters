using Holsters.Utility.ModSettings;
using Holsters.Utility.ModSettings.PresetsLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Holsters
{
    public class IR_HolstersSettings : ModSettings
    {
        public IR_HolstersSettings()
        {
        }
        private bool _isFirstLoad = true;


        private static PresetsContainer _presets;

        
        private const float forwardPos = 0.026f;
        private const float middlePos = 0.0128957527f;
        private const float backPos = -0.0128957527f;

        public static float backLayerOffset = 0;
        public static float frontLayerOffset = 0;

        public static bool displayIndoors = true;

        public static void Initialise()
        {

            if (_presets == null)
            {
                Log.Message("Holsters: initialised presets");
                List<IPresetable> presetables = PresetDefLoader.LoadPresets().ToList();

                _presets = new PresetsContainer(presetables);
            } else
            {
                List<HolsterDefPresetSetting> presetables = PresetDefLoader.LoadPresets().Cast<HolsterDefPresetSetting>().ToList();

                _presets.AddLoadedDefs(presetables);
            }


        }

        public static IEnumerable<IPresetable> Holsters()
        {
            foreach(IPresetable configuration in _presets.Presets())
            {
                yield return configuration;
            }
        }  

        public override void ExposeData()
        {

            Scribe_Values.Look(ref backLayerOffset, "backLayerOffset", 0);
            Scribe_Values.Look(ref frontLayerOffset, "frontLayerOffset", 0);
            Scribe_Values.Look(ref displayIndoors, "displayIndoors", true);
            Scribe_Deep.Look(ref _presets, "presets2");

            base.ExposeData();
        }


        private static HolsterPresetDef GetConfiguration(ThingDef def)
        {
            return EquipmentCategorySorter.SortWeaponsIntoGroups(def);
        }

        public static HolsterConfiguration GetHolsterConfigurationFor(ThingDef def, Rot4 rot)
        {
            return GetConfiguration(def).Configuration[rot];
        }

        public static float FrontPos
        {
            get { return forwardPos + frontLayerOffset; }
        }

        public static float BackPos
        {
            get { return backPos + backLayerOffset; }
        }
    }
}



