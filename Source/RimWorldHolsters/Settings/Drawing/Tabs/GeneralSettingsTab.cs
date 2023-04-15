using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse.Noise;
using Verse;

namespace Holsters.Utility.ModSettings.Settings_Drawing.Tabs
{
    internal class GeneralSettingsTab : TabDrawer
    {
        private string errorLog;
        private bool restoreDefaultConfirmation;
        private bool resetConfirmation;

        private float receivingGroupIndex;
        private string groupName;

        public override string TabName => "General";

        protected override void TabContent(Rect inRect)
        {
            //Rect leftSideRect = new Rect(inRect.x + 10f, inRect.y + (inRect.height * 0.3f), 0.2f * inRect.width - 20f, inRect.height);
            //Widgets.DrawLineHorizontal(leftSideRect.x, leftSideRect.y + 24f, leftSideRect.width);


            /*
            Listing_Standard leftSideListing = new Listing_Standard();
            leftSideListing.Begin(leftSideRect);

            leftSideListing.Label("Display Settings");
            leftSideListing.Gap();


            leftSideListing.CheckboxLabeled("Display indoors: ", ref IR_HolstersSettings.displayIndoors);
            leftSideListing.CheckboxLabeled("Display sidearms: ", ref IR_HolstersSettings.displaySide);
            leftSideListing.CheckboxLabeled("Smart sidearms: ", ref IR_HolstersSettings.smartSideDisplay);

            if (leftSideListing.ButtonText("Edit mode: " + currentModeLabel, "Change to edit sidearms positions and primary positions of the weapons."))
            {
                isSidearmMode = !isSidearmMode;
                return;
            }

            leftSideListing.End();*/
        }

        private void DrawDangerZone()
        {/*
            Rect dangerZoneRect = new Rect(inRect.x + 10f, inRect.y + (0.55f * inRect.height), 0.2f * inRect.width - 20f, 0.6f * inRect.height);
            Widgets.DrawLineHorizontal(dangerZoneRect.x, dangerZoneRect.y + 24f, dangerZoneRect.width);
            Listing_Standard dangerZoneListing = new Listing_Standard();
            dangerZoneListing.Begin(dangerZoneRect);
            dangerZoneListing.Label("DANGER ZONE");
            dangerZoneListing.Gap();

            if (dangerZoneListing.ButtonText("Restore default settings") && restoreDefaultConfirmation)
            {
                curGroupIndex = 0;
                curWeaponIndex = 0;
                IR_HolstersSettings.ResetAllGroups();

                TryLoadWeapons(true);
                return;
            }
            dangerZoneListing.CheckboxLabeled("Confirm: ", ref restoreDefaultConfirmation, "It cannot be reversed!");

            dangerZoneListing.End();*/
        }

        private void DrawErrorLog()
        {
            /*
            middleListing.Begin(middleRect);

            if (!errorLog.NullOrEmpty())
            {
                middleListing.Label(errorLog);
            }

            middleListing.End();*/
        }
    }
}
