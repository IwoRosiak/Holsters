using Holsters.Settings.Drawing.Tabs.Presets;
using ModSettingsTools;
using ModSettingsTools.Selection;
using ModSettingsTools.Selection.Builders;
using ModSettingsTools.Selection.Selectors;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Holsters.Settings.Drawing.Equipment.Operations
{
    internal sealed class AllPresetsChoiceOperation : Operation
    {
        private readonly ClickSelector<IPresetable> _listSelector;

        internal AllPresetsChoiceOperation(Rect area) : base(area)
        {
            IPresetable defaultChoice = IR_HolstersSettings.Holsters().ToList()[0];
            _listSelector = new ClickSelector<IPresetable>(BUTTON_WIDTH, new VerticalBuilder<SelectorPair<IPresetable>>(), defaultChoice);
        }

        public override void ExecuteOperation()
        {
            if (PresetChoiceTracker.CurrentPreset == null)
                PresetChoiceTracker.UpdateChoice(IR_HolstersSettings.Holsters().ToList()[0]);

            List<SelectorPair<IPresetable>> selectorPairs = IR_HolstersSettings.Holsters()
                .Select(preset => new SelectorPair<IPresetable>(preset, preset.Name))
                .ToList();

            _listSelector.DrawSelection(area, selectorPairs);

            _listSelector.OnSelected = null;
            _listSelector.OnSelected += SelectedPresetTracker.UpdateChoice;
        }
    }
}