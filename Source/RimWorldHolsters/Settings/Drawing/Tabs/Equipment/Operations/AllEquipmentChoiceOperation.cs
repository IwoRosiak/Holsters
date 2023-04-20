using Holsters.Settings.Drawing.Utilities;
using Holsters.Settings.ModSettingsTools.Utilities.Selectors.ListTickSelectors;
using Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie;
using RimWorldHolsters.Settings.ModSettingsTools.Utilities.Selectors.ListTickSelectors;
using SettingsDrawer.Sections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Holsters.Settings.Drawing.Tabs.Equipment.Operations
{
    internal sealed class AllEquipmentChoiceOperation : Operation
    {
        private readonly ListTickSelector<ThingDef> _listSelector;

        private readonly List<TickSelectorPair<ThingDef>> _selectorPairs;

        public AllEquipmentChoiceOperation(Rect area) : base(area)
        {
            List<ThingDef> loadedEquipment = EquipmentLoader.LoadEquipment()
                .Where(def => EquipmentPresetSorter.SortWeaponsIntoGroups(def) != null)
                .ToList();

            _selectorPairs = loadedEquipment
                .Select(preset => new TickSelectorPair<ThingDef>(preset, preset.defName))
                .ToList();

            _listSelector = new ListTickSelector<ThingDef>(BUTTON_WIDTH);
        }

        public override void ExecuteOperation()
        {
            _listSelector.DrawSelection(area, _selectorPairs);

            _listSelector.OnSelected = null;
            _listSelector.OnSelected += SelectedEquipmentTracker.UpdateSelection;

            _listSelector.OnDeselected = null;
            _listSelector.OnDeselected += SelectedEquipmentTracker.RemoveSelection;
        }
    }
}