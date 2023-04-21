using Holsters.Settings.Drawing.Tabs.Equipment.Operations;
using Holsters.Settings.Drawing.Tabs.Presets;
using Holsters.Settings.Drawing.Utilities;
using Holsters.Settings.ModSettingsTools.Utilities.Selectors.Builders;
using Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie;
using Holsters.Utility.ModSettings.Settings_Drawing.TableDrawer;
using Holsters.Utility.ModSettings.Settings_Drawing.Tabs.PresetsTab;
using RimWorld;
using SettingsDrawer.Sections;
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