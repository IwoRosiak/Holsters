using Holsters.Settings.Drawing.Tabs.Equipment.Operations;
using UnityEngine;

namespace Holsters.Settings.Drawing.Tabs.Equipment
{
    internal sealed class EquipmentTab : Utility.ModSettings.Settings_Drawing.Tabs.TabDrawer
    {
        private AllEquipmentChoiceOperation _allEquipmentOperation;
        private SelectedEquipmentDisplayOperation _displaySelected;
        private AllPresetsChoiceOperation _allPresetsOperation;
        public override string TabName => "Equipment Management";

        protected override void TabContent(Rect inRect)
        {
            if (_allEquipmentOperation == null)
            {
                _allEquipmentOperation = new AllEquipmentChoiceOperation(new Rect(1, 1, 8, 13));
                _displaySelected = new SelectedEquipmentDisplayOperation(new Rect(12, 1, 9, 6));
                _allPresetsOperation = new AllPresetsChoiceOperation(new Rect(12, 6, 9, 6));
            }

            Utility.ModSettings.Settings_Drawing.Section section = new Utility.ModSettings.Settings_Drawing.Section(inRect, 20, 20);

            section.AddOperation(_allEquipmentOperation);
            section.AddOperation(_displaySelected);
            section.AddOperation(_allPresetsOperation);

            section.DrawOperations();
        }
    }
}
