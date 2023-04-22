using UnityEditor;
using UnityEngine;

namespace Holsters.Settings.Drawing.Tabs.Presets
{
    internal static class PresetChoiceTracker
    {
        public static IPresetable CurrentPreset { get; private set; }

        public static void UpdateChoice(IPresetable presetable)
        {
            CurrentPreset = presetable;
            PresetEquipmentChoiceTracker.UpdateChoice(null);
        }
    }
}