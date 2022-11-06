using RimWorldHolsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Utility.ModSettings.Settings_Drawing.TableDrawer
{
    internal class PositionButtons : Operation
    {
        public PositionButtons(Rect area) : base(area)
        {
        }

        public override void ExecuteOperation()
        {
            Section section = new Section(area, 10, 10);

            Rect west = new Rect(1, 4.5f, 1, 1);
            Rect east = new Rect(8, 4.5f, 1, 1);
            Rect north = new Rect(4.5f, 1, 1, 1);
            Rect south = new Rect(4.5f, 8, 1, 1);

            section.AddOperation(new Button(west, "-X", () => Log.Message("Up!")));
            section.AddOperation(new Button(east, "+X", () => Log.Message("Up!")));
            section.AddOperation(new Button(north, "+Y", () => Log.Message("Up!")));
            section.AddOperation(new Button(south, "-Y", () => Log.Message("Up!")));

            section.DrawOperations();
        }

        private void DrawPositionButtons(Rect rect)
        {
           
            /*
            if (Widgets.ButtonText(buttonWest, "-X", true, true, Color.blue, true))
            {
                var temp = mod.GetCurGroup().GetPos(mod.curDir, mod.isSidearmMode);
                temp.x -= 0.05f;
                mod.GetCurGroup().SetPos(mod.curDir, temp, mod.isSidearmMode);
            }
            if (Widgets.ButtonText(buttonEast, "+X"))
            {
                var tempX = mod.GetCurGroup().GetPos(mod.curDir, mod.isSidearmMode);
                tempX.x += 0.05f;
                mod.GetCurGroup().SetPos(mod.curDir, tempX, mod.isSidearmMode);
            }
            if (Widgets.ButtonText(buttonNorth, "+Y"))
            {
                var tempX = mod.GetCurGroup().GetPos(mod.curDir, mod.isSidearmMode);
                tempX.z += 0.05f;
                mod.GetCurGroup().SetPos(mod.curDir, tempX, mod.isSidearmMode);
            }
            if (Widgets.ButtonText(buttonSouth, "-Y"))
            {
                var tempX = mod.GetCurGroup().GetPos(mod.curDir, mod.isSidearmMode);
                tempX.z -= 0.05f;
                mod.GetCurGroup().SetPos(mod.curDir, tempX, mod.isSidearmMode);
            }*/
        }
    }
}
