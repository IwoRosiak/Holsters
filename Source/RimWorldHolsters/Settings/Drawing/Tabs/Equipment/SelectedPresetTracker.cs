using Holsters.Settings.Drawing.Tabs.Presets;

namespace Holsters.Settings.Drawing.Tabs.Equipment
{
    internal static class SelectedPresetTracker
    {
        public static IPresetable CurrentPreset { get; private set; }

        public static void UpdateChoice(IPresetable presetable)
        {
            CurrentPreset = presetable;
        }
    }
}