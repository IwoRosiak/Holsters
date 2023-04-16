using Holsters.Settings;
using Holsters.Settings.Drawing.Tabs.Presets;
using Holsters.Settings.Drawing.Utilities;
using Holsters.Settings.PresetsLoading;
using Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie;
using SettingsDrawer.Sections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Holsters.Utility.ModSettings.Settings_Drawing.Tabs.PresetsTab
{
    internal class PresetChoice : Operation
    {
        private readonly ScrollListSelector<IPresetable> _listSelector;


        internal PresetChoice(Rect area) : base(area)
        {
            _listSelector = new HorizontalSpreadListSelector<IPresetable>(IR_HolstersSettings.Holsters().ToList()[0], 4, BUTTON_WIDTH);
        }

        public IPresetable Current => _listSelector.GetSelected();

        public override void ExecuteOperation()
        {
            List<SelectorPair<IPresetable>> selectorPairs = IR_HolstersSettings.Holsters()
                .Select(preset => new SelectorPair<IPresetable>(preset, preset.Name))
                .ToList();

            _listSelector.DrawSelection(area, selectorPairs);

            _listSelector.OnSelected += PresetChoiceTracker.UpdateChoice;
        }

    }
}
