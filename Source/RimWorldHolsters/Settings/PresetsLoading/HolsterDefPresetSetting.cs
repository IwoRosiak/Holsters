﻿using System;
using System.Collections.Generic;
using Verse;

namespace Holsters.Settings.PresetsLoading
{
    internal sealed class HolsterDefPresetSetting : IPresetable
    {
        private HolsterPreset _customPreset;

        private string _basedOn;

        private string _presetName;

        private List<ThingDef> _assocciatedEquipment = new List<ThingDef>();

        public HolsterDefPresetSetting() { }

        public HolsterDefPresetSetting(HolsterPresetDef presetToBaseOn)
        {
            _basedOn = presetToBaseOn.defName;
            _presetName = presetToBaseOn.label;
        }

        public HolsterPreset Preset
        {
            get
            {
                if (_customPreset == null || _customPreset.Configuration.NullOrEmpty())
                {
                    return Def.Preset;
                }

                return _customPreset;
            }
        }

        public string BasedOn => _basedOn;

        private HolsterPresetDef Def => (HolsterPresetDef)GenDefDatabase.GetDef(typeof(HolsterPresetDef), _basedOn);

        public string Name { get => _presetName; set => _presetName = value; }
        public List<ThingDef> AssocciatedEquipment { get => _assocciatedEquipment; set => _assocciatedEquipment = value; }

        private HolsterPreset PresetOld;

        public void Reset()
        {
            PresetOld = _customPreset;
            _customPreset = null;
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref _presetName, "presetName");
            Scribe_Deep.Look(ref _customPreset, "customPreset");
            Scribe_Values.Look(ref _basedOn, "originalPreset");
        }

        public void ModifyProperty(Action<HolsterConfiguration> modification, Rot4 rotation)
        {
            if (_customPreset == null)
            {// The configuration is not deep copied but shallow only
                _customPreset = new HolsterPreset(Def.Preset);
            }

            HolsterConfiguration holster = _customPreset.Configuration[rotation];
            modification.Invoke(holster);
        }

        public bool IsNotTheSameAs(IPresetable preset)
        {
            if (preset is HolsterDefPresetSetting defPreset)
            {
                return _basedOn != defPreset._basedOn;
            }

            return true;
        }
    }
}
