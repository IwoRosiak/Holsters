using Holsters.Settings.Drawing.Tabs.Equipment.Operations;
using Holsters.Settings.Drawing.Tabs.PresetsTab;
using Holsters.Utility.ModSettings.Settings_Drawing.Tabs.PresetsTab;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Holsters.Settings.Drawing.Tabs.Equipment
{
    internal sealed class EquipmentTab : Utility.ModSettings.Settings_Drawing.Tabs.TabDrawer
    {

        private AllEquipmentChoiceOperation _allEquipmentOperation;

        public EquipmentTab()
        {

        }

        public override string TabName => "Equipment Management";

        protected override void TabContent(Rect inRect)
        {
            if (_allEquipmentOperation == null)
            {
                _allEquipmentOperation = new AllEquipmentChoiceOperation(new Rect(1, 1, 18, 8));
            }

            Utility.ModSettings.Settings_Drawing.Section section = new Utility.ModSettings.Settings_Drawing.Section(inRect, 20, 20);

            section.AddOperation(_allEquipmentOperation);

            section.DrawOperations();

        }
    }
}
