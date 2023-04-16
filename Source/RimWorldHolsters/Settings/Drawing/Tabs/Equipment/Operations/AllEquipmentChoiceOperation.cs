using Holsters.Settings.Drawing.Tabs.Presets;
using Holsters.Settings.Drawing.Utilities;
using Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie;
using SettingsDrawer.Sections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Holsters.Settings.Drawing.Tabs.Equipment.Operations
{
    internal sealed class AllEquipmentChoiceOperation : Operation
    {
        private readonly ScrollListSelector<ThingDef> _listSelector;
        private readonly List<ThingDef> _loadedEquipment;
        public AllEquipmentChoiceOperation(Rect area) : base(area)
        {
            _loadedEquipment = EquipmentLoader.LoadEquipment()
                .Where(def => EquipmentPresetSorter.SortWeaponsIntoGroups(def) != null)
                .ToList();

            _listSelector = new ScrollListSelector<ThingDef>(BUTTON_WIDTH);
        }

        public override void ExecuteOperation()
        {
            List<SelectorPair<ThingDef>> selectorPairs = _loadedEquipment
            .Select(preset => new SelectorPair<ThingDef>(preset, preset.defName))
            .ToList();

            _listSelector.DrawSelection(area, selectorPairs);
        }
    }
}