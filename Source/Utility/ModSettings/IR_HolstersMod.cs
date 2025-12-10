using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters
{
    public class IR_HolstersMod : Mod
    {
        private IR_HolstersSettings settings;

        public IR_HolstersMod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<IR_HolstersSettings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            IR_ModSettingsDrawer.mod = this;

            if (curWeapons.NullOrEmpty())
            {
                TryLoadWeapons(true);
            } 

            Widgets.DrawTextureFitted(inRect, IR_Textures.background, 1);

            Rect middleRect = new Rect(inRect.x + (0.3f * inRect.width) +10f, inRect.y, 0.4f * inRect.width -20f, 0.3f * inRect.height);
            Listing_Standard middleListing = new Listing_Standard();
            

            middleListing.Begin(middleRect);

            //middleListing.Label(instructions);

            if (!errorLog.NullOrEmpty())
            {
                middleListing.Label(errorLog);
            }
           
            middleListing.End();

            Rect bodyFrameRect = new Rect(inRect.x + (0.2f * inRect.width), inRect.y + (0.34f * inRect.height), 0.6f * inRect.width, 0.6f * inRect.height);
            IR_ModSettingsDrawer.DrawPawn(bodyFrameRect);


            DrawWeaponsManagement(inRect);
            DrawGroupsManagement(inRect);


            //DANGER ZONE
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

            dangerZoneListing.End();

            //Rect layerOffsetsRect= new Rect(inRect.x + 10f, inRect.y + (0.5f * inRect.height), 0.2f * inRect.width - 20f, 0.6f * inRect.height);


            IR_ModSettingsDrawer.DrawBodyManagement(new Rect(inRect.x + (0.8f * inRect.width) + 10f, inRect.y + (0.3f * inRect.height), 0.2f * inRect.width -20f, 0.6f * inRect.height));

            

            //MODE MANAGEMENT
            Rect leftSideRect = new Rect(inRect.x +10f , inRect.y+ (inRect.height*0.3f), 0.2f * inRect.width - 20f, inRect.height);
            Widgets.DrawLineHorizontal(leftSideRect.x, leftSideRect.y + 24f, leftSideRect.width);


            Listing_Standard leftSideListing = new Listing_Standard();
            leftSideListing.Begin(leftSideRect);

            leftSideListing.Label("Display Settings");
            leftSideListing.Gap();

            string currentModeLabel;
            if (isSidearmMode)
            {
                currentModeLabel = "sidearms";
            }
            else
            {
                currentModeLabel = "primary";
            }

            leftSideListing.CheckboxLabeled("Display indoors: ", ref IR_HolstersSettings.displayIndoors);
            leftSideListing.CheckboxLabeled("Display sidearms: ", ref IR_HolstersSettings.displaySide);
            leftSideListing.CheckboxLabeled("Smart sidearms: ", ref IR_HolstersSettings.smartSideDisplay);

            if (leftSideListing.ButtonText("Edit mode: " + currentModeLabel, "Change to edit sidearms positions and primary positions of the weapons."))
            {
                isSidearmMode = !isSidearmMode;
                return;
            }

            leftSideListing.End();
        }

        private string errorLog;
        private bool restoreDefaultConfirmation;
        private bool resetConfirmation;

        private float receivingGroupIndex;
        private string groupName;

        private void DrawGroupsManagement(Rect rect)
        {
            Rect mainRect = new Rect(rect.x + (0.7f * rect.width) + 10f, rect.y, 0.3f * rect.width -20f, 0.40f * rect.height);

            Rect gapLine = mainRect;
            //gapLine.x +=15f;

            gapLine.height = 30;

            Widgets.Label(gapLine, "Groups");
            Widgets.DrawLineHorizontal(gapLine.x, gapLine.y + 24f, gapLine.width);

            //SECTION FOR GROUPS

            mainRect.y += 30;

            Rect changeGroupRect = mainRect;
            changeGroupRect.height = 30;

            changeGroupRect.width = mainRect.width * 0.2f;
            if (Widgets.ButtonText(changeGroupRect, "<-"))
            {
                if (curGroupIndex == 0)
                {
                    curGroupIndex = IR_HolstersSettings.groups.Count - 1;
                }
                else
                {
                    curGroupIndex--;
                }

                TryLoadWeapons(true);
                return;
            }

            changeGroupRect.width = mainRect.width * 0.6f;
            changeGroupRect.x = mainRect.x + (mainRect.width * 0.2f);
            Widgets.ButtonText(changeGroupRect, GetCurGroup().Name, true, false, false);

            changeGroupRect.width = mainRect.width * 0.2f;
            changeGroupRect.x = mainRect.x + (mainRect.width * 0.8f);
            if (Widgets.ButtonText(changeGroupRect, "->"))
            {
                if (curGroupIndex == IR_HolstersSettings.groups.Count - 1)
                {
                    curGroupIndex = 0;
                }
                else
                {
                    curGroupIndex++;
                }

                TryLoadWeapons(true);
                return;
            }

            mainRect.y += 30;

            Rect changeNameRect = mainRect;
            changeNameRect.height = 30;

            groupName = Widgets.TextEntryLabeled(changeNameRect, "Group's name", groupName);

            changeNameRect.y += 30;
            changeNameRect.width = mainRect.width * 0.5f;

            if (Widgets.ButtonText(changeNameRect, "Change name"))
            {
                if (groupName.Equals(""))
                {
                    errorLog = "Name needs at least one letter!";
                    return;
                }

                errorLog = GetCurGroup().Name + " changed to " + groupName + ".";
                IR_HolstersSettings.ChangeGroupsName(GetCurGroup(), groupName);
                groupName = "";
            }

            changeNameRect.x = mainRect.x + (mainRect.width * 0.5f);

            if (Widgets.ButtonText(changeNameRect, "Create group"))
            {
                if (groupName.Equals(""))
                {
                    errorLog = "Name needs at least one letter!";
                    return;
                }

                errorLog = groupName + " created!";
                IR_HolstersSettings.AddNewSettingsGroup(groupName);
            }

            mainRect.y += 60;

            mainRect.height = 30;
            if (Widgets.ButtonText(mainRect ,"Delete group"))
            {
                if (!GetCurGroup().isDisplay)
                {
                    errorLog = "This group cannot be deleted.";

                } else if (GetCurWeapon() == null)
                {
                    errorLog = GetCurGroup().Name + " deleted!";
                    IR_HolstersSettings.RemoveGroup(GetCurGroup());
                    
                    curGroupIndex = 0;
                    return;

                }
                else
                {
                    errorLog = "The group can be only deleted if it has no weapons assigned (move weapons to another group to resolve that issue)";
                }
            }
        }

        private void DrawWeaponsManagement(Rect rect)
        {
            Rect mainRect = new Rect(rect.x + 10f, rect.y, 0.3f * rect.width -20f, 0.4f * rect.height);

            Rect gapLine = mainRect;

            gapLine.height = 30;

            Widgets.Label(gapLine, "Weapons");
            Widgets.DrawLineHorizontal(gapLine.x, gapLine.y + 24f, gapLine.width);

            mainRect.y += 30;

            Rect changeWeapons = mainRect;
            changeWeapons.height = 30;

            changeWeapons.width = mainRect.width * 0.2f;
            if (Widgets.ButtonText(changeWeapons, "<-"))
            {
                if (curWeaponIndex == 0)
                {
                    curWeaponIndex = curWeapons.Count - 1;
                }
                else
                {
                    curWeaponIndex--;
                }
            }

            changeWeapons.width = mainRect.width * 0.6f;
            changeWeapons.x = mainRect.x + (mainRect.width * 0.2f);
            
            if (curWeapons.NullOrEmpty())
            {
                Widgets.ButtonText(changeWeapons, "Empty group", true, false, false);
            }
            else
            {
                Widgets.ButtonText(changeWeapons, curWeapons[curWeaponIndex].label, true, false, false);
            }

            changeWeapons.width = mainRect.width * 0.2f;
            changeWeapons.x = mainRect.x + (mainRect.width * 0.8f);
            if (Widgets.ButtonText(changeWeapons, "->"))
            {
                if (curWeaponIndex == curWeapons.Count - 1)
                {
                    curWeaponIndex = 0;
                }
                else
                {
                    curWeaponIndex++;
                }
            }

            mainRect.y+=30;
            mainRect.height = 30;

            if (Widgets.ButtonText(mainRect ,"Send to: " + IR_HolstersSettings.groups[(int)receivingGroupIndex].Name))
            {
                if (GetCurWeapon() != null)
                {
                    errorLog = GetCurWeapon().defName + " moved to " + IR_HolstersSettings.groups[(int)receivingGroupIndex].Name;
                    ChangeCurWeaponsGroup(GetCurGroup(), (int)receivingGroupIndex);
                    TryLoadWeapons(false);
                    return;
                }
            }

            mainRect.y+=30;
            receivingGroupIndex = (int)Widgets.HorizontalSlider(mainRect, receivingGroupIndex, 0, IR_HolstersSettings.groups.Count - 1);


            mainRect.y += 30;
        }

        private bool TryLoadWeapons(bool resetIndex)
        {
            if (resetIndex)
            {
                curWeaponIndex = 0;
            } else AdjustWeaponIndex();
            curWeapons.Clear();

            foreach (ThingDef weapon in GenDefDatabase.GetAllDefsInDatabaseForDef(typeof(ThingDef)))
            {
                if (weapon.IsWeapon && weapon.equipmentType == EquipmentType.Primary && GetCurGroup().weapons.Contains(weapon.defName) && weapon.tradeability != Tradeability.None)
                {
                    curWeapons.Add(weapon);
                }
            }
            
            if (curWeapons.NullOrEmpty())
            {
                return false;
            }
            return true;
        }

        private void AdjustWeaponIndex()
        {
            if (curWeaponIndex != 0)
            {
                curWeaponIndex--;
            }
        }

        private void ChangeCurWeaponsGroup(WeaponGroupCordInfo fromGroup, int toGroupIndex)
        {
            ThingDef weapon = GetCurWeapon();

            fromGroup.weapons.Remove(weapon.defName);
            IR_HolstersSettings.groups[toGroupIndex].weapons.Add(weapon.defName);
        }

        internal ThingDef GetCurWeapon()
        {
            if (curWeapons.NullOrEmpty())
            {
                return null;
            }

            return curWeapons[curWeaponIndex];
        }

        internal WeaponGroupCordInfo GetCurGroup()
        {
            if (IR_HolstersSettings.groups.NullOrEmpty())
            {
                IR_HolstersSettings.InitBasicGroups();
            }

            return IR_HolstersSettings.groups[curGroupIndex];
        }

        internal BodyType currentBody = BodyType.male;
        internal List<ThingDef> curWeapons = new List<ThingDef>();
        internal int curGroupIndex = 0;


        internal Rot4 curDir = Rot4.South;
        internal bool isSidearmMode = false;
        internal bool isPrimaryMode = true;

        internal static float pixelRatio = 96;

        internal int curWeaponIndex = 0;

        private string instructions = "Guide: \nAll placement settings are group specific, not weapon specific. \nIf you use any sidearm mod you can also edit position for those seperately. \nPositions have to be manually adjusted for each side the pawn is looking at. \nBody offsets are there since some bodies have different dimensions. The position offsets are shared for all bodies but can be modified using impacts (impact 0 means body offsets do not affect this body type.)\n";
        public override string SettingsCategory()
        {
            return "Holsters";
        }
    }

    public enum BodyType
    {
        hulk,
        fat,
        thin,
        male,
        female
    }
}