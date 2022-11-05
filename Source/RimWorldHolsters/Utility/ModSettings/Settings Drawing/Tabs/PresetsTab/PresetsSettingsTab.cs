using Holsters;
using RimWorldHolsters.Core;
using RimWorldHolsters.Utility.ModSettings.Settings_Drawing.Tabs.PresetsTab;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Utility.ModSettings.Settings_Drawing.Tabs
{
    internal class PresetsSettingsTab : TabDrawer
    {
        internal BodyType currentBody = BodyType.male;
        internal Rot4 curDir = Rot4.South;

        private PresetChoice _presetChoice;

        public override string TabName => "Presets";

        protected override void TabContent(Rect inRect)
        {
            Rect bodyFrameRect = new Rect(inRect.x + (0.2f * inRect.width), inRect.y + (0.34f * inRect.height), 0.6f * inRect.width, 0.6f * inRect.height);
            Widgets.DrawTextureFitted(bodyFrameRect, IR_Textures.backgroundPawn, 1);

            DrawGroupsManagement(inRect);
        }

        private void DrawGroupsManagement(Rect rect)
        {
            if (_presetChoice == null)
            {
                _presetChoice = new PresetChoice(new RectInt(4, 5, 12, 4));
            }

            Section section = new Section(rect, 20, 10);

            section.AddOperation(_presetChoice);





            section.DrawOperations();




            //DrawPresetNameChange(rect);
            //DrawPresetDelete(rect);
            //DrawCreateNewPreset(rect);
            //DrawCopyPreset(rect);

            //section.DrawOperations();
        }







        private string _groupName;

        private void DrawPresetNameChange(Rect drawRect)
        {
            drawRect.height = 30;

            _groupName = Widgets.TextEntryLabeled(drawRect, "Group's name", _groupName);

            drawRect.y += 30;
            drawRect.width = drawRect.width * 0.5f;

            ModSettingsUtilities.DrawButton(drawRect, "Change Name", () =>
            {
                if (_groupName.Equals(""))
                {
                    return;
                }

                _groupName = "";
            });
        }

        private void DrawPresetDelete(Rect rect)
        {
            ModSettingsUtilities.DrawButton(rect, "Delete group", () =>
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
            });
        }

        private void DrawCreateNewPreset(Rect rect)
        {
            ModSettingsUtilities.DrawButton(rect, "Create group", () =>
            {
                if (_groupName.Equals(""))
                {
                    //errorLog = "Name needs at least one letter!";
                    return;
                }

                //errorLog = groupName + " created!";
                //IR_HolstersSettings.AddNewSettingsGroup(groupName);
            });
        }

        private void DrawCopyPreset(Rect rect)
        {

        }
    }
}
