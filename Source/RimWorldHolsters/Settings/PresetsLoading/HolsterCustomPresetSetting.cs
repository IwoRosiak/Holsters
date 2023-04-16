using System;
using System.Collections.Generic;
using Verse;

namespace Holsters.Settings.PresetsLoading
{
    internal sealed class HolsterCustomPresetSetting : IPresetable
    {
        private HolsterPreset _preset;

        private string _presetName;

        private List<ThingDef> _assocciatedEquipment = new List<ThingDef>();

        public HolsterCustomPresetSetting() { }

        public HolsterCustomPresetSetting(HolsterPresetDef def)
        {
            _preset = def.Preset;
            _presetName = def.label;
        }

        public HolsterCustomPresetSetting(IPresetable defPreset, string presetName)
        {
            _preset = new HolsterPreset(defPreset.Preset);
            _presetName = presetName;
        }

        public HolsterCustomPresetSetting(string presetName)
        {
            _presetName = presetName;
            _preset = new HolsterPreset();
            _preset.FillWithEmptyEntries();
        }

        public HolsterPreset Preset => _preset;

        public string Name { get => _presetName; set => _presetName = value; }
        public List<ThingDef> AssocciatedEquipment { get => _assocciatedEquipment; set => _assocciatedEquipment = value; }

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
