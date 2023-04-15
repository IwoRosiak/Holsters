using System;
using System.Collections.Generic;
using Verse;

namespace Holsters.Settings.PresetsLoading
{
    internal sealed class HolsterDefPresetSetting : IPresetable
    {
        private HolsterPreset _customPreset;

        private string _basedOn;

        private string _presetName;

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
                    return Def.Preset;


                return _customPreset;
            }
        }

        private HolsterPresetDef Def => (HolsterPresetDef)GenDefDatabase.GetDef(typeof(HolsterPresetDef), _basedOn);


        public string Name => _presetName;

        public void ExposeData()
        {
            Scribe_Values.Look(ref _presetName, "presetName");
            Scribe_Deep.Look(ref _customPreset, "customPreset");
            Scribe_Values.Look(ref _basedOn, "originalPreset");
        }

        public void ModifyProperty(Action<HolsterConfiguration> modification, Rot4 rotation)
        {
            if (_customPreset == null)
            {
                Log.Message("Refreshing the preset");
                _customPreset = new HolsterPreset()
                {
                    Configuration = new Dictionary<Rot4, HolsterConfiguration>(Def.Configuration),
                    BodyOffsetsModifs = new Dictionary<BodyType, float>(Def.BodyOffsetsModifs)
                };
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
