using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using ModSettingsTools;
using ModSettingsTools.Selection;
using ModSettingsTools.Selection.Selectors;
using ModSettingsTools.Selection.Builders;

namespace Holsters.Settings.Drawing.Equipment.Operations
{
    internal sealed class AllEquipmentChoiceOperation : Operation
    {
        private readonly CheckboxSelector<ThingDef> _listSelector;

        private readonly List<SelectorPair<ThingDef>> _selectorPairs;

        public AllEquipmentChoiceOperation(Rect area) : base(area)
        {
            List<ThingDef> loadedEquipment = EquipmentLoader.LoadEquipment()
                .Where(def => EquipmentPresetSorter.SortWeaponsIntoGroups(def) != null)
                .ToList();

            _selectorPairs = loadedEquipment
                .Select(preset => new SelectorPair<ThingDef>(preset, preset.label))
                .ToList();

            _listSelector = new CheckboxSelector<ThingDef>(BUTTON_WIDTH, new VerticalBuilder<SelectorPair<ThingDef>>());
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