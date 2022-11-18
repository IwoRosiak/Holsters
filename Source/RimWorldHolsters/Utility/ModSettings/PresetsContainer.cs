using Holsters;
using RimWorldHolsters.Utility.ModSettings.PresetsLoading;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace RimWorldHolsters.Utility.ModSettings
{
    internal class PresetsContainer : IExposable
    {

        private List<IPresetable> _presetSettings;

        public PresetsContainer()
        {
           
        }

        public PresetsContainer(List<IPresetable> presetSettings)
        {
            _presetSettings = presetSettings;
        }

        public void AddLoadedDefs(List<HolsterDefPresetSetting> presetables)
        {
            if (_presetSettings.NullOrEmpty())
                _presetSettings = new List<IPresetable>();

            List<IPresetable> presetsToAdd = new List<IPresetable>();

            

            presetables.ForEach(presetDef =>
            {
                if (_presetSettings.All(preset => preset.IsAcceptable(presetDef)))
                {
                    presetsToAdd.Add(presetDef);
                }
            });

            _presetSettings.AddRange(presetsToAdd);
        }

        public void ExposeData()
        {
            Scribe_Collections.Look(ref _presetSettings, "presetSettings", LookMode.Deep);
        }

        public IEnumerable<IPresetable> Presets()
        {
            foreach(IPresetable presetSetting in _presetSettings)
            {
                yield return presetSetting;
            }
        }

    }
}
