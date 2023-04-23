using Holsters.Settings;
using Holsters.Settings.Drawing.Tabs.Presets;
using Holsters.Settings.PresetsLoading;
using ModSettingsTools;
using ModSettingsTools.Operations;
using System.Linq;
using UnityEngine;

namespace Holsters.Utility.ModSettings.Settings_Drawing.Tabs.PresetsTab
{
    internal class PresetDelete : Operation
    {
        public PresetDelete(Rect area) : base(area)
        {
        }
        public override void ExecuteOperation()
        {
            Section section = new Section(area, 1, 1);

            Button button = new Button(new Rect(0, 0, 1, 1), ChooseWording(), ButtonClick);
            section.AddOperation(button);

            section.DrawOperations();
        }

        private void ButtonClick()
        {
            IPresetable presetable = PresetChoiceTracker.CurrentPreset;

            IR_HolstersSettings.RemoveSetting(presetable);
            
            if (IR_HolstersSettings.Holsters().Contains(presetable) == false)
                PresetChoiceTracker.UpdateChoice(IR_HolstersSettings.Holsters().ToList()[0]);
        }

        private string ChooseWording()
        {
            IPresetable presetable = PresetChoiceTracker.CurrentPreset;

            if (presetable is HolsterDefPresetSetting)
            {
                return "Reset";
            }
            else return "Delete";
        }
    }
}
