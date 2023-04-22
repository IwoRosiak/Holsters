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

        private List<IPresetable> _presetOfCurrentSelection;
        private List<ThingDef> SelectedEquipment => SelectedEquipmentTracker.SelectedEquipment;

        internal EquipmentPresetsTransferOperation(Rect area) : base(area)
        {
            IPresetable defaultChoice = IR_HolstersSettings.Holsters().ToList()[0];

            _listSelector = new ClickSelector<IPresetable>(BUTTON_WIDTH, new VerticalBuilder<SelectorPair<IPresetable>>(), defaultChoice);

            _transferSelector = new ClickSelector<IPresetable>(BUTTON_WIDTH, new VerticalBuilder<SelectorPair<IPresetable>>(), defaultChoice);
        }

        public override void ExecuteOperation()
        {
            if (SelectedEquipment.NullOrEmpty() == true)
                return;

            DrawTransferList();
            DrawAvailableList();
        }

        private void DrawTransferList()
        {
            _presetOfCurrentSelection = IR_HolstersSettings.Holsters()
                .Where(preset => SelectedEquipment.All(eq =>  preset.AssocciatedEquipment.Contains(eq)))
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
            foreach(ThingDef def in SelectedEquipment)
            {
                presetable.AssocciatedEquipment.Remove(def);
            }
        }

        private void Add(IPresetable presetable)
        {
            foreach (ThingDef def in SelectedEquipment)
            {
                if (presetable.AssocciatedEquipment.Contains(def) == true)
                    continue;

                presetable.AssocciatedEquipment.Add(def);
            }
        }
    }
}