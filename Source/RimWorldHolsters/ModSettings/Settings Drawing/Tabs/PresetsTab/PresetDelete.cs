using Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Holsters.Utility.ModSettings.Settings_Drawing.Tabs.PresetsTab
{
    internal class PresetDelete : Operation
    {
        public PresetDelete(Rect area) : base(area)
        {
        }
        public override void ExecuteOperation()
        {
            Section section = new Section(area, 1, 1);

            Button button = new Button(new Rect(0, 0, 1, 1), "Create new", ButtonClick);
            section.AddOperation(button);

            section.DrawOperations();
        }

        private void ButtonClick()
        {
            if (false)//!GetCurGroup().isDisplay)
            {
                //errorLog = "This group cannot be deleted.";

            }
            else if (false)//GetCurWeapon() == null)
            {
                //errorLog = GetCurGroup().Name + " deleted!";
                //IR_HolstersSettings.RemoveGroup(GetCurGroup());

                //curGroupIndex = 0;
                return;

            }
            else
            {
                //errorLog = "The group can be only deleted if it has no weapons assigned (move weapons to another group to resolve that issue)";
            }
        }
    }
}
