using UnityEngine;

namespace RimWorldHolsters.Utility.ModSettings.Settings_Drawing.Tabs
{
    internal class WeaponsSettingsTab : TabDrawer
    {
        public override string TabName => "Weapons";

        protected override void TabContent(Rect inRect)
        {
            /*
            if (curWeapons.NullOrEmpty())
            {
                TryLoadWeapons(true);
                DrawWeaponsManagement(inRect);
            }*/

        }

        private void DrawWeaponsManagement(Rect rect)
        {
            /*
            Rect mainRect = new Rect(rect.x + 10f, rect.y, 0.3f * rect.width - 20f, 0.4f * rect.height);

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

            mainRect.y += 30;
            mainRect.height = 30;

            if (Widgets.ButtonText(mainRect, "Send to: " + IR_HolstersSettings.groups[(int)receivingGroupIndex].Name))
            {
                if (GetCurWeapon() != null)
                {
                    errorLog = GetCurWeapon().defName + " moved to " + IR_HolstersSettings.groups[(int)receivingGroupIndex].Name;
                    ChangeCurWeaponsGroup(GetCurGroup(), (int)receivingGroupIndex);
                    TryLoadWeapons(false);
                    return;
                }
            }

            mainRect.y += 30;
            receivingGroupIndex = (int)Widgets.HorizontalSlider(mainRect, receivingGroupIndex, 0, IR_HolstersSettings.groups.Count - 1);


            mainRect.y += 30;*/
        }

        /*
        private bool TryLoadWeapons(bool resetIndex)
        {
            if (resetIndex)
            {
                curWeaponIndex = 0;
            }
            else AdjustWeaponIndex();
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
        }*/
    }
}
