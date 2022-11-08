using Holsters;
using RimWorldHolsters.Core.Presets;
using System;

namespace RimWorldHolsters.Utility.ModSettings.PresetsLoading
{
    public class HolsterPresetSetting :  IPresetable
    {
        private HolsterPreset _configuration;

        private HolsterPresetDef _basedOn;

        private string _presetName;

        public HolsterPresetSetting(HolsterPresetDef presetDef)
        {
            _configuration = presetDef.Preset;
            _presetName = presetDef.label;
        }


        public HolsterPreset Configuration => _configuration;

        public HolsterPresetDef BasedOnDef => _basedOn;

        public string Name => _presetName;

        public void ModifyProperty(Action<HolsterConfiguration> procedure)
        {
            throw new NotImplementedException();
        }
    }
}
