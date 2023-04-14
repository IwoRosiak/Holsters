using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace Holsters.Utility.ModSettings.PresetsLoading
{
    internal class HolsterDefPresetSetting : IPresetable, IExposable
    {
        private HolsterPreset _customPreset;

        private string _basedOn;

        private string _presetName;

        private HolsterPresetDef Def => (HolsterPresetDef)GenDefDatabase.GetDef(typeof(HolsterPresetDef), _basedOn); 

        public HolsterDefPresetSetting()
        {

        }

        public HolsterDefPresetSetting(HolsterPresetDef presetDef)
        {
            _basedOn = presetDef.defName;
            _presetName = presetDef.label;
        }

        public HolsterPreset Configuration
        {
            get
            {
                if (_customPreset == null || _customPreset.Configuration.NullOrEmpty())
                {
                    return Def.Preset;
                }
                else return _customPreset;
            }
        } 

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

        public bool IsAcceptable(IPresetable preset)
        {
            if (preset is HolsterDefPresetSetting defPreset)
            {
                return _basedOn != defPreset._basedOn;
            }

            return true;
        }
    }
}
