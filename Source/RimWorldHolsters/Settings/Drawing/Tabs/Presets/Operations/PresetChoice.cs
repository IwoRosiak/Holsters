using Holsters.Settings;
using Holsters.Settings.Drawing.Tabs.Presets;
using Holsters.Settings.Drawing.Utilities;
using Holsters.Settings.ModSettingsTools.Utilities.Selectors.Builders;
using Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie;
using SettingsDrawer.Sections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Holsters.Utility.ModSettings.Settings_Drawing.Tabs.PresetsTab
{
    internal sealed class PresetChoice : Operation
    {
        private readonly ClickSelector<IPresetable> _listSelector;

        internal PresetChoice(Rect area) : base(area)
        {
            IPresetable defaultChoice = IR_HolstersSettings.Holsters().ToList()[0];

            _listSelector = new ClickSelector<IPresetable>(4, new HorizontalBuilder<SelectorPair<IPresetable>>(4), defaultChoice);
        }

        public IPresetable Current => _listSelector.Selected;

        public override void ExecuteOperation()
        {
            if (PresetChoiceTracker.CurrentPreset == null)
                PresetChoiceTracker.UpdateChoice(IR_HolstersSettings.Holsters().ToList()[0]);

            List<SelectorPair<IPresetable>> selectorPairs = IR_HolstersSettings.Holsters()
                .Select(preset => new SelectorPair<IPresetable>(preset, preset.Name))
                .ToList();

            _listSelector.DrawSelection(area, selectorPairs);

            _listSelector.OnSelected = null;
            _listSelector.OnSelected += PresetChoiceTracker.UpdateChoice;
        }

    }
}
