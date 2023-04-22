using Holsters.Settings.Drawing.Equipment.Operations;
using ModSettingsTools;
using System.Collections.Generic;
using UnityEngine;

namespace Holsters.Settings.Drawing.Equipment
{
    internal sealed class EquipmentTab : TabDrawer
    {
        public override string TabName => "Equipment Management";

        protected override List<Operation> Operations => new List<Operation>
        {
            new AllEquipmentChoiceOperation(new Rect(1, 1, 8, 17)),
            new SelectedEquipmentDisplayOperation(new Rect(12, 1, 9, 6)),
            new EquipmentPresetsTransferOperation(new Rect(12, 6, 9, 6))
        };

        protected override Vector2Int SectionGrid => new Vector2Int(20, 20);
    }
}
