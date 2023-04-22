using ModSettingsTools;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Holsters.Settings.Drawing.Equipment.Operations
{
    internal sealed class SelectedEquipmentDisplayOperation : Operation
    {
        public SelectedEquipmentDisplayOperation(Rect area) : base(area)
        {
        }

        public override void ExecuteOperation()
        {
            if (SelectedEquipmentTracker.SelectedEquipment.Count == 1)
            {
                DisplayOneEquipment(SelectedEquipmentTracker.SelectedEquipment.First());
            }
            else if (SelectedEquipmentTracker.SelectedEquipment.Count > 1)
            {
                DisplayMultiple(SelectedEquipmentTracker.SelectedEquipment);
            }
        }

        private void DisplayOneEquipment(ThingDef def)
        {
            Texture text = def.graphic.MatNorth.mainTexture;
            float scale = 1 / def.uiIconScale / (text.width / 64) * 1.35f * def.graphic.drawSize.x;

            Vector2 center = area.center;

            Widgets.DrawTextureRotated(new Rect(center.x - scale / 2f, center.y - scale / 2f, 200, 200), text, 0);
        }

        private void DisplayMultiple(List<ThingDef> def)
        {

        }
    }
}