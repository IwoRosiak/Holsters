using Holsters.Utility.ModSettings.Settings_Drawing;
using SettingsDrawer.Sections;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Holsters.Settings.Drawing.Tabs.Equipment.Operations
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
            float scale = (((1 / def.uiIconScale) / (text.width / 64)) * 1.35f * def.graphic.drawSize.x) ;

            //Widgets.DrawTextureRotated(rect.center + (offset * pixelRatio), text, IR_WeaponData.GetWeaponAngle(GetCurGroup()sIndex, currentDir), scale);

            Vector2 center =  area.center;

            Log.Message("Hello");

            Widgets.DrawTextureRotated(new Rect(center.x - scale / 2f, center.y - scale / 2f, 200, 200), text, 0);
        }

        private void DisplayMultiple(List<ThingDef> def)
        {

        }
    }
}