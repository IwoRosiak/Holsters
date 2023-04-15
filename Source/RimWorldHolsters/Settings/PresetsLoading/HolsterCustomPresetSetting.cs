using Holsters.Settings;
using System;
using Verse;

namespace Holsters.Settings.PresetsLoading
{
    internal sealed class HolsterCustomPresetSetting : IPresetable
    {
        private HolsterPreset _preset;

        private string _presetName;

        public HolsterCustomPresetSetting() { }

        public HolsterCustomPresetSetting(HolsterPresetDef def)
        {
            _preset = def.Preset;
            _presetName = def.label;
        }

        public HolsterCustomPresetSetting(string presetName)
        {
            _presetName = presetName;
        }

        public HolsterPreset Preset => _preset;

        public string Name => _presetName;

        public void ExposeData()
        {
            Scribe_Values.Look(ref _presetName, "presetName", "noName");
            Scribe_Deep.Look(ref _preset, "customPreset");
        }

        public bool IsNotTheSameAs(IPresetable preset)
        {
            return true;
        }

        public void ModifyProperty(Action<HolsterConfiguration> modification, Rot4 rotation)
        {
            HolsterConfiguration holster = _preset.Configuration[rotation];

            modification.Invoke(holster);
        }
    }
}
