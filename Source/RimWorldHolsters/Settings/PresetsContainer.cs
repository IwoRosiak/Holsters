using System.Collections.Generic;
using System.Linq;
using Verse;

namespace Holsters.Settings
{
    internal class PresetsContainer : IExposable
    {
        private List<IPresetable> _presetSettings;

        public PresetsContainer() { }

        public PresetsContainer(List<IPresetable> presetSettings)
        {
            _presetSettings = presetSettings;
        }

        public void AddLoadedDefs(List<IPresetable> presetables)
        {
            if (_presetSettings.NullOrEmpty() == true)
                _presetSettings = new List<IPresetable>();

            List<IPresetable> presetsToAdd = presetables
                .Where(presetable => _presetSettings.All(presetSetting => presetSetting.IsNotTheSameAs(presetable)))
                .ToList();

            _presetSettings.AddRange(presetsToAdd);
        }

        public void ExposeData()
        {
            Scribe_Collections.Look(ref _presetSettings, "presetSettings", LookMode.Deep);
        }

        public IEnumerable<IPresetable> GetPresets()
        {
            foreach (IPresetable presetSetting in _presetSettings)
            {
                yield return presetSetting;
            }
        }
    }
}
