using System;
using Verse;

namespace Holsters.Utility.ModSettings.PresetsLoading
{
    public class HolsterPresetSetting :  IPresetable
    {
        private HolsterPreset _configuration;

        private string _presetName;


        public HolsterPresetSetting()
        {

        }

        public HolsterPresetSetting(HolsterPresetDef def)
        {
            _configuration = def.Preset;
            _presetName = def.label;
        }

        public HolsterPresetSetting(string presetName)
        {
            _presetName = presetName;
        }

        public HolsterPreset Configuration => _configuration;

        public string Name => _presetName;

        public void ExposeData()
        {
            Scribe_Values.Look(ref _presetName, "presetName", "noName");
            Scribe_Deep.Look(ref _configuration, "customPreset");
        }

        public bool IsAcceptable(IPresetable preset)
        {
            return true;
        }

        public void ModifyProperty(Action<HolsterConfiguration> modification, Rot4 rotation)
        {
            HolsterConfiguration holster = _configuration.Configuration[rotation];

            modification.Invoke(holster);
        }
    }
}
