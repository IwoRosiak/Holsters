using Holsters.Settings.Drawing.Tabs.Presets;
using Holsters.Settings.Drawing.Utilities;
using Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie;
using SettingsDrawer.Sections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Holsters.Settings.Drawing.Tabs.PresetsTab
{
    internal sealed class PresetEquipmentChoice : Operation
    {
        private readonly ScrollListSelector<ThingDef> _listSelector;

        private readonly List<ThingDef> _defs;
        public PresetEquipmentChoice(Rect area) : base(area)
        {
            _defs = EquipmentLoader.LoadEquipment();
            _listSelector = new ScrollListSelector<ThingDef>(_defs[0], 50);
        }

        public override void ExecuteOperation()
        {
            List<SelectorPair<ThingDef>> selectorPairs = _defs
            .Select(preset => new SelectorPair<ThingDef>(preset, preset.defName))
            .ToList();

            _listSelector.DrawSelection(area, selectorPairs);

            _listSelector.OnSelected += EquipmentChoiceTracker.UpdateChoice;

        }




    }
}