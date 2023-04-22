using Holsters.Settings.Drawing.Tabs.Presets;
using ModSettingsTools;
using ModSettingsTools.Selection;
using ModSettingsTools.Selection.Builders;
using ModSettingsTools.Selection.Selectors;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Holsters.Settings.Drawing.Tabs.PresetsTab
{
    internal sealed class PresetEquipmentChoice : Operation
    {
        private readonly ClickSelector<ThingDef> _listSelector;

        public PresetEquipmentChoice(Rect area) : base(area)
        {
            _listSelector = new ClickSelector<ThingDef>(BUTTON_WIDTH, new VerticalBuilder<SelectorPair<ThingDef>>());
        }

        public override void ExecuteOperation()
        {
            IPresetable currentPreset = PresetChoiceTracker.CurrentPreset;

            List<SelectorPair<ThingDef>> selectorPairs = currentPreset.AssocciatedEquipment
            .Select(preset => new SelectorPair<ThingDef>(preset, preset.defName))
            .ToList();

            _listSelector.DrawSelection(area, selectorPairs);

            _listSelector.OnSelected = null;
            _listSelector.OnSelected += PresetEquipmentChoiceTracker.UpdateChoice;

        }
    }
}