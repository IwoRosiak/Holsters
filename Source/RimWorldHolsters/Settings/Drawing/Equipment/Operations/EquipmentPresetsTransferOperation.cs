using Holsters.Settings.Drawing.Tabs.Presets;
using ModSettingsTools;
using ModSettingsTools.Selection;
using ModSettingsTools.Selection.Builders;
using ModSettingsTools.Selection.Selectors;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Holsters.Settings.Drawing.Equipment.Operations
{
    internal sealed class EquipmentPresetsTransferOperation : Operation
    {
        private readonly ClickSelector<IPresetable> _listSelector;
        private readonly ClickSelector<IPresetable> _transferSelector;


        internal EquipmentPresetsTransferOperation(Rect area) : base(area)
        {
            IPresetable defaultChoice = IR_HolstersSettings.Holsters().ToList()[0];

            _listSelector = new ClickSelector<IPresetable>(BUTTON_WIDTH, new VerticalBuilder<SelectorPair<IPresetable>>(), defaultChoice);

            _transferSelector = new ClickSelector<IPresetable>(BUTTON_WIDTH, new VerticalBuilder<SelectorPair<IPresetable>>(), defaultChoice);
        }

        public override void ExecuteOperation()
        {
            if (SelectedEquipmentTracker.SelectedEquipment?.FirstOrDefault() == null)
                return;

            DrawTransferList();
            DrawAvailableList();
        }

        private List<IPresetable> _presetOfCurrentSelection;

        private void DrawTransferList()
        {
            _presetOfCurrentSelection = IR_HolstersSettings.Holsters()
                .Where(preset => preset.AssocciatedEquipment.Contains(SelectedEquipmentTracker.SelectedEquipment.First()))
                .ToList();

            List<SelectorPair<IPresetable>> selectorPairs = _presetOfCurrentSelection
                .Select(preset => new SelectorPair<IPresetable>(preset, preset.Name))
                .ToList();

            Rect halfRect = area.RightHalf();

            _transferSelector.DrawSelection(halfRect, selectorPairs);

            _transferSelector.OnSelected = null;

            _transferSelector.OnSelected += Remove;
        }
        private void DrawAvailableList()
        {
            List<SelectorPair<IPresetable>> selectorPairs = IR_HolstersSettings.Holsters()
                .Where(preset => _presetOfCurrentSelection.Contains(preset) == false)
                .Select(preset => new SelectorPair<IPresetable>(preset, preset.Name))
                .ToList();

            Rect halfRect = area.LeftHalf();

            _listSelector.DrawSelection(halfRect, selectorPairs);

            _listSelector.OnSelected = null;
            _listSelector.OnSelected += Add;
        }

        private void Remove(IPresetable presetable)
        {
            ThingDef thing = SelectedEquipmentTracker.SelectedEquipment?.FirstOrDefault();
            
            presetable.AssocciatedEquipment.Remove(thing);
        }

        private void Add(IPresetable presetable)
        {
            ThingDef thing = SelectedEquipmentTracker.SelectedEquipment?.FirstOrDefault();

            presetable.AssocciatedEquipment.Add(thing);
        }


    }
}