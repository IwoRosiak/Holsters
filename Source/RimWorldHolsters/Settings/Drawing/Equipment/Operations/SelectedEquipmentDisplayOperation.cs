using ModSettingsTools;
using System;
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

            Vector2 center = area.center;
            Widgets.DrawBox(area);

            Rect displayRect = new Rect(center.x - (area.height / 2), center.y - (area.height / 2), area.height, area.height);

            Widgets.DrawTextureRotated(displayRect, text, -45);

            Widgets.DrawBox(displayRect);
        }

        private void DisplayMultiple(List<ThingDef> def)
        {

        }
    }
}