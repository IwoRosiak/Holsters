using RimWorld;
using RimWorldHolsters.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters
{
    public class IR_HolstersMod : Mod
    {
        internal const float PIXEL_RATIO = 96;

        private readonly IR_HolstersSettings _settings;

        private string _errorLog;
        private bool _restoreDefaultConfirmation;

        private float _receivingGroupIndex;
        private string _groupName;

        internal BodyType CurrentBody = BodyType.male;
        internal List<ThingDef> _curWeapons = new List<ThingDef>();
        internal int _curGroupIndex = 0;


        internal Rot4 _curDir = Rot4.South;
        internal bool _isSidearmMode = false;
        internal bool _isPrimaryMode = true;


        internal int _curWeaponIndex = 0;

        //private string instructions = "Guide: \nAll placement settings are group specific, not weapon specific. \nIf you use any sidearm mod you can also edit position for those seperately. \nPositions have to be manually adjusted for each side the pawn is looking at. \nBody offsets are there since some bodies have different dimensions. The position offsets are shared for all bodies but can be modified using impacts (impact 0 means body offsets do not affect this body type.)\n";

        public IR_HolstersMod(ModContentPack content) : base(content)
        {
            _settings = GetSettings<IR_HolstersSettings>();
        }

        public override string SettingsCategory() => "Holsters";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            IR_ModSettingsDrawer.mod = this;

            if (_curWeapons.NullOrEmpty())
            {
                _ = TryLoadWeapons(true);
            } 

            Widgets.DrawTextureFitted(inRect, IR_Textures.background, 1);

            var middleRect = new Rect(inRect.x + (0.3f * inRect.width) +10f, inRect.y, 0.4f * inRect.width -20f, 0.3f * inRect.height);
            var middleListing = new Listing_Standard();
            

            middleListing.Begin(middleRect);

            //middleListing.Label(instructions);

            if (!_errorLog.NullOrEmpty())
            {
                _ = middleListing.Label(_errorLog);
            }
           
            middleListing.End();

            var bodyFrameRect = new Rect(inRect.x + (0.2f * inRect.width), inRect.y + (0.34f * inRect.height), 0.6f * inRect.width, 0.6f * inRect.height);
            IR_ModSettingsDrawer.DrawPawn(bodyFrameRect);


            DrawWeaponsManagement(inRect);
            DrawGroupsManagement(inRect);


            //DANGER ZONE
            var dangerZoneRect = new Rect(inRect.x + 10f, inRect.y + (0.55f * inRect.height), 0.2f * inRect.width - 20f, 0.6f * inRect.height);
            Widgets.DrawLineHorizontal(dangerZoneRect.x, dangerZoneRect.y + 24f, dangerZoneRect.width);
            var dangerZoneListing = new Listing_Standard();
            dangerZoneListing.Begin(dangerZoneRect);

            _ = dangerZoneListing.Label("DANGER ZONE");
            dangerZoneListing.Gap();

            if (dangerZoneListing.ButtonText("Restore default settings") && _restoreDefaultConfirmation)
            {
                _curGroupIndex = 0;
                _curWeaponIndex = 0;
                IR_HolstersSettings.ResetAllGroups();

                _ = TryLoadWeapons(true);
                return;
            }

            dangerZoneListing.CheckboxLabeled("Confirm: ", ref _restoreDefaultConfirmation, "It cannot be reversed!");

            dangerZoneListing.End();

            //Rect layerOffsetsRect= new Rect(inRect.x + 10f, inRect.y + (0.5f * inRect.height), 0.2f * inRect.width - 20f, 0.6f * inRect.height);


            //TODO: BODY MODIFS IR_ModSettingsDrawer.DrawBodyManagement(new Rect(inRect.x + (0.8f * inRect.width) + 10f, inRect.y + (0.3f * inRect.height), 0.2f * inRect.width -20f, 0.6f * inRect.height));

            

            //MODE MANAGEMENT
            var leftSideRect = new Rect(inRect.x +10f , inRect.y+ (inRect.height*0.3f), 0.2f * inRect.width - 20f, inRect.height);
            Widgets.DrawLineHorizontal(leftSideRect.x, leftSideRect.y + 24f, leftSideRect.width);


            var leftSideListing = new Listing_Standard();
            leftSideListing.Begin(leftSideRect);

            _ = leftSideListing.Label("Display Settings");
            leftSideListing.Gap();

            string currentModeLabel;
            if (_isSidearmMode)
            {
                currentModeLabel = "sidearms";
            }
            else
            {
                currentModeLabel = "primary";
            }

            leftSideListing.CheckboxLabeled("Display indoors: ", ref IR_HolstersSettings.DisplayIndoors);
            leftSideListing.CheckboxLabeled("Display sidearms: ", ref IR_HolstersSettings.DisplaySidearms);
            leftSideListing.CheckboxLabeled("Smart sidearms: ", ref IR_HolstersSettings.SmartSideDisplay);

            if (leftSideListing.ButtonText("Edit mode: " + currentModeLabel, "Change to edit sidearms positions and primary positions of the weapons."))
            {
                _isSidearmMode = !_isSidearmMode;
                return;
            }

            leftSideListing.End();
        }

        internal ThingDef GetCurWeapon()
        {
            if (_curWeapons.NullOrEmpty())
            {
                return null;
            }

            return _curWeapons[_curWeaponIndex];
        }

        internal HolsterWeaponRenderGroup GetCurGroup()
        {
            if (IR_HolstersSettings.RenderGroups.NullOrEmpty())
            {
                IR_HolstersSettings.InitBasicGroups();
            }

            return IR_HolstersSettings.RenderGroups[_curGroupIndex];
        }

        private void DrawGroupsManagement(Rect rect)
        {
            var mainRect = new Rect(rect.x + (0.7f * rect.width) + 10f, rect.y, 0.3f * rect.width -20f, 0.40f * rect.height);

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
                if (_curGroupIndex == 0)
                {
                    _curGroupIndex = IR_HolstersSettings.RenderGroups.Count - 1;
                }
                else
                {
                    _curGroupIndex--;
                }

                _ = TryLoadWeapons(true);
                return;
            }

            changeGroupRect.width = mainRect.width * 0.6f;
            changeGroupRect.x = mainRect.x + (mainRect.width * 0.2f);
            _ = Widgets.ButtonText(changeGroupRect, GetCurGroup().Name, true, false, false);

            changeGroupRect.width = mainRect.width * 0.2f;
            changeGroupRect.x = mainRect.x + (mainRect.width * 0.8f);
            if (Widgets.ButtonText(changeGroupRect, "->"))
            {
                if (_curGroupIndex == IR_HolstersSettings.RenderGroups.Count - 1)
                {
                    _curGroupIndex = 0;
                }
                else
                {
                    _curGroupIndex++;
                }

                 _ = TryLoadWeapons(true);
                return;
            }

            mainRect.y += 30;

            Rect changeNameRect = mainRect;
            changeNameRect.height = 30;

            _groupName = Widgets.TextEntryLabeled(changeNameRect, "Group's name", _groupName);

            changeNameRect.y += 30;
            changeNameRect.width = mainRect.width * 0.5f;

            if (Widgets.ButtonText(changeNameRect, "Change name"))
            {
                if (_groupName.Equals(""))
                {
                    _errorLog = "Name needs at least one letter!";
                    return;
                }

                _errorLog = GetCurGroup().Name + " changed to " + _groupName + ".";
                IR_HolstersSettings.ChangeGroupsName(GetCurGroup(), _groupName);
                _groupName = "";
            }

            changeNameRect.x = mainRect.x + (mainRect.width * 0.5f);

            if (Widgets.ButtonText(changeNameRect, "Create group"))
            {
                if (_groupName.Equals(""))
                {
                    _errorLog = "Name needs at least one letter!";
                    return;
                }

                _errorLog = _groupName + " created!";
                IR_HolstersSettings.AddNewSettingsGroup(_groupName);
            }

            mainRect.y += 60;

            mainRect.height = 30;
            if (Widgets.ButtonText(mainRect ,"Delete group"))
            {
                if (!GetCurGroup().ShouldDisplay)
                {
                    _errorLog = "This group cannot be deleted.";

                } else if (GetCurWeapon() == null)
                {
                    _errorLog = GetCurGroup().Name + " deleted!";
                    IR_HolstersSettings.RemoveGroup(GetCurGroup());
                    
                    _curGroupIndex = 0;
                    return;

                }
                else
                {
                    _errorLog = "The group can be only deleted if it has no weapons assigned (move weapons to another group to resolve that issue)";
                }
            }
        }

        private void DrawWeaponsManagement(Rect rect)
        {
            var mainRect = new Rect(rect.x + 10f, rect.y, 0.3f * rect.width -20f, 0.4f * rect.height);

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
                if (_curWeaponIndex == 0)
                {
                    _curWeaponIndex = _curWeapons.Count - 1;
                }
                else
                {
                    _curWeaponIndex--;
                }
            }

            changeWeapons.width = mainRect.width * 0.6f;
            changeWeapons.x = mainRect.x + (mainRect.width * 0.2f);
            
            if (_curWeapons.NullOrEmpty())
            {
                _ = Widgets.ButtonText(changeWeapons, "Empty group", true, false, false);
            }
            else
            {
                _ = Widgets.ButtonText(changeWeapons, _curWeapons[_curWeaponIndex].label, true, false, false);
            }

            changeWeapons.width = mainRect.width * 0.2f;
            changeWeapons.x = mainRect.x + (mainRect.width * 0.8f);
            if (Widgets.ButtonText(changeWeapons, "->"))
            {
                if (_curWeaponIndex == _curWeapons.Count - 1)
                {
                    _curWeaponIndex = 0;
                }
                else
                {
                    _curWeaponIndex++;
                }
            }

            mainRect.y+=30;
            mainRect.height = 30;

            if (Widgets.ButtonText(mainRect ,"Send to: " + IR_HolstersSettings.RenderGroups[(int)_receivingGroupIndex].Name))
            {
                if (GetCurWeapon() != null)
                {
                    _errorLog = GetCurWeapon().defName + " moved to " + IR_HolstersSettings.RenderGroups[(int)_receivingGroupIndex].Name;
                    ChangeCurWeaponsGroup(GetCurGroup(), (int)_receivingGroupIndex);
                    TryLoadWeapons(false);
                    return;
                }
            }

            mainRect.y+=30;
            _receivingGroupIndex = (int)Widgets.HorizontalSlider(mainRect, _receivingGroupIndex, 0, IR_HolstersSettings.RenderGroups.Count - 1);


            mainRect.y += 30;
        }

        private bool TryLoadWeapons(bool resetIndex)
        {
            if (resetIndex)
            {
                _curWeaponIndex = 0;
            } 
            else
            {
                AdjustWeaponIndex();
            }
            _curWeapons.Clear();

            foreach (ThingDef weapon in GenDefDatabase.GetAllDefsInDatabaseForDef(typeof(ThingDef)))
            {
                if (weapon.IsWeapon && weapon.equipmentType == EquipmentType.Primary && GetCurGroup().Weapons.Contains(weapon.defName) && weapon.tradeability != Tradeability.None)
                {
                    _curWeapons.Add(weapon);
                }
            }
            
            if (_curWeapons.NullOrEmpty())
            {
                return false;
            }
            return true;
        }

        private void AdjustWeaponIndex()
        {
            if (_curWeaponIndex == 0)
                return;
            
            _curWeaponIndex--;
        }

        private void ChangeCurWeaponsGroup(HolsterWeaponRenderGroup fromGroup, int toGroupIndex)
        {
            ThingDef weapon = GetCurWeapon();

            _ = fromGroup.Weapons.Remove(weapon.defName);
            IR_HolstersSettings.RenderGroups[toGroupIndex].Weapons.Add(weapon.defName);
        }


    }
}