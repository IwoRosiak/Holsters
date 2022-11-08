using RimWorldHolsters.Utility.ModSettings.PresetsLoading;
using System.Collections.Generic;

namespace RimWorldHolsters.Utility.ModSettings
{
    internal class PresetsContainer
    {

        private List<IPresetable> _presetSettings;

        public PresetsContainer(List<IPresetable> presetSettings)
        {
            _presetSettings = presetSettings;
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
