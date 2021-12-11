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
            InitSettingsDict();

            if (curWeapons.NullOrEmpty())
            {
                TryLoadWeapons();
            }

            if (!IR_HolstersSettings.WeaponDataSettings.ContainsKey(WeaponType.doNotDisplay))
            {
                IR_HolstersSettings.InitWeaponDataSettings();
            }

            if (IR_HolstersSettings.WeaponDataSettings[currentType].pos.NullOrEmpty() || IR_HolstersSettings.WeaponDataSettings[currentType].flip.NullOrEmpty())
            {
                IR_HolstersSettings.InitSpecificSetting(currentType, IR_HolstersSettings.WeaponDataSettings[currentType]);
            }

            Widgets.DrawTextureFitted(inRect, IR_Textures.background, 1);

            Listing_Standard listing = new Listing_Standard();

            listing.Begin(inRect);

            listing.ColumnWidth = inRect.width / 3.15f;

            if (!IR_HolstersSettings.WeaponDataSettings.ContainsKey(currentType))
            {
                IR_HolstersSettings.InitWeaponDataSettings();
            }

            if (listing.ButtonText("Previous weapon type") && IR_HolstersSettings.WeaponDataSettings.ContainsKey(currentType - 1))
            {
                currentType--;

                TryLoadWeapons();
                return;
            }
            if (listing.ButtonText("Previous weapon"))
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

            if (listing.ButtonText("Reset every type"))
            {
                IR_HolstersSettings.InitWeaponDataSettings();

                return;
            }

            if (listing.ButtonText("Flip"))
            {
                var tempX = IR_HolstersSettings.WeaponDataSettings[currentType].flip[currentDir];
                tempX = !tempX;
                IR_HolstersSettings.WeaponDataSettings[currentType].flip[currentDir] = tempX;
                return;
            }
            listing.NewColumn();
            listing.Label("Current weapon type: " + currentType.ToString());
            if (curWeapons.NullOrEmpty())
            {
                listing.Label("Current weapon: NONE");
            }
            else
            {
                listing.Label("Current weapon: " + curWeapons[curWeaponIndex].label);
            }

            listing.Label("Current coordinates: " + Math.Round(IR_WeaponData.GetWeaponPos(currentType, currentDir).x, 3) + "X " + Math.Round(IR_WeaponData.GetWeaponPos(currentType, currentDir).z, 3) + "Y " + "Angle: " + IR_WeaponData.GetWeaponAngle(currentType, currentDir));

            listing.NewColumn();

            if (listing.ButtonText("Next weapon type") && IR_HolstersSettings.WeaponDataSettings.ContainsKey(currentType + 1))
            {
                currentType++;
                TryLoadWeapons();
                return;
            }

            if (listing.ButtonText("Next weapon"))
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

            if (listing.ButtonText("Reset current type"))
            {
                IR_HolstersSettings.InitSpecificSetting(currentType, IR_HolstersSettings.WeaponDataSettings[currentType]);
            }

            if (listing.ButtonText("Change type to: " + sendToCat))
            {
                ChangeWeaponType(sendToCat);
            }
            sendToCat = (WeaponType)listing.Slider((float)sendToCat, 0, 9);

            listing.End();

            Rect bodyFrameRect = new Rect(inRect.x + (0.2f * inRect.width), inRect.y + (0.3f * inRect.height), 0.6f * inRect.width, 0.6f * inRect.height);

            Widgets.DrawTextureFitted(bodyFrameRect, IR_Textures.backgroundPawn, 1);

            if (DrawingMode())
            {
                DrawWeapon(bodyFrameRect);
                DrawBody(bodyFrameRect);

                DrawHead(bodyFrameRect);
            }
            else
            {
                DrawBody(bodyFrameRect);

                DrawHead(bodyFrameRect);

                DrawWeapon(bodyFrameRect);
            }

            DrawBodyButton(bodyFrameRect);
        }

        private WeaponType sendToCat;

        private bool TryLoadWeapons()
        {
            curWeaponIndex = 0;
            curWeapons.Clear();
            foreach (ThingDef weapon in GenDefDatabase.GetAllDefsInDatabaseForDef(typeof(ThingDef)))
            {
                if (weapon.IsWeapon && weapon.equipmentType == EquipmentType.Primary && IR_WeaponType.EstablishWeaponType(weapon) == currentType && weapon.tradeability != Tradeability.None)
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

        private void ChangeWeaponType(WeaponType cat)
        {
            if (curWeapons.NullOrEmpty())
            {
                return;
            }
            var weapon = GetCurWeapon();
            if (IR_HolstersSettings.WeaponSpecialType.ContainsKey(weapon.defName))
            {
                IR_HolstersSettings.WeaponSpecialType.Remove(weapon.defName);
            }

            IR_HolstersSettings.WeaponSpecialType.Add(weapon.defName, cat);

            TryLoadWeapons();
        }

        private bool DrawingMode()
        {
            if (IR_WeaponData.GetWeaponPos(currentType, currentDir).y >= 1f)
            {
                return false;
            }
            return true;
        }

        private List<ThingDef> curWeapons = new List<ThingDef>();
        private int curWeaponIndex = 0;

        private void DrawWeapon(Rect rect)
        {
            if (curWeapons.NullOrEmpty() || currentType == WeaponType.doNotDisplay)
            {
                return;
            }
            Vector2 offset = new Vector2(IR_WeaponData.GetWeaponPos(currentType, currentDir).x, -IR_WeaponData.GetWeaponPos(currentType, currentDir).z);

            offset += GetBodySizeOffset();

            Texture text = curWeapons[curWeaponIndex].graphic.MatNorth.mainTexture;
            float scale = ((1 / curWeapons[curWeaponIndex].uiIconScale) / (text.width / 64)) * 1.35f * GetCurWeapon().graphic.drawSize.x;

            //Widgets.DrawTextureRotated(rect.center + (offset * pixelRatio), text, IR_WeaponData.GetWeaponAngle(currentType, currentDir), scale);

            Vector2 center = rect.center + (offset * pixelRatio);

            float num = (float)text.width * scale;

            float num2 = (float)text.height * scale;

            if (IR_WeaponData.GetWeaponFlip(currentType, currentDir))
            {
                num2 = (float)text.height * -scale;
            }

            Widgets.DrawTextureRotated(new Rect(center.x - num / 2f, center.y - num2 / 2f, num, num2), text, IR_WeaponData.GetWeaponAngle(currentType, currentDir));

            //Widgets.DrawTextureRotated(new Rect(center.x - num / 2f, center.y - num2 / 2f, num, num2), text, IR_WeaponData.GetWeaponAngle(currentType, currentDir));
        }

        private ThingDef GetCurWeapon()
        {
            return curWeapons[curWeaponIndex];
        }

        private Vector2 GetBodySizeOffset()
        {
            switch (currentType)
            {
                case WeaponType.bow:

                case WeaponType.longRanged:

                case WeaponType.longMelee:
                    return IR_PositionAdjuster.GetBodyOffsetLargeWeapons(currentDir, null, currentBody);
                    break;

                case WeaponType.shortRanged:

                case WeaponType.shortMelee:

                case WeaponType.grenades:
                    return IR_PositionAdjuster.GetBodyOffsetSmallWeapons(currentDir, null, currentBody);
                    break;

                default:
                    return Vector2.zero;
                    break;
            }
        }

        private void DrawBody(Rect rect)
        {
            Rect bodyRect = new Rect(rect.x + (0.2f * rect.width), rect.y + (0.3f * rect.height), 0.6f * rect.width, 0.6f * rect.width);

            var texture = ChooseBodyTexture();

            Widgets.DrawTextureRotated(rect.center, texture, 0);
        }

        private void DrawHead(Rect rect)
        {
            Rect headRect = new Rect(rect.x + (0.2f * rect.width), rect.y + (0.3f * rect.height), 0.6f * rect.width, 0.6f * rect.width);

            var texture = ChooseHeadTexture();

            float offset = 0;

            if (currentDir == Rot4.East)
            {
                offset = -ChooseHeadOffset();
            }
            else if (currentDir == Rot4.West)
            {
                offset = ChooseHeadOffset();
            }

            Widgets.DrawTextureRotated(rect.center - new Vector2(offset * pixelRatio, 34), texture, 0);
        }

        private void DrawBodyButton(Rect rect)
        {
            Rect buttonWest = new Rect(rect.x, rect.y + (0.45f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonEast = new Rect(rect.x + (0.9f * rect.width), rect.y + (0.45f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonNorth = new Rect(rect.x + (0.45f * rect.width), rect.y, 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonSouth = new Rect(rect.x + (0.45f * rect.width), rect.y + (0.9f * rect.height), 0.1f * rect.width, 0.1f * rect.height);

            Rect buttonLayerMinus = new Rect(rect.x + (0.35f * rect.width), rect.y + (0.9f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonLayerPlus = new Rect(rect.x + (0.55f * rect.width), rect.y + (0.9f * rect.height), 0.1f * rect.width, 0.1f * rect.height);

            Rect buttonLookWest = new Rect(rect.x + (0.1f * rect.width), rect.y + (0.45f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonLookEast = new Rect(rect.x + (0.8f * rect.width), rect.y + (0.45f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonLookNorth = new Rect(rect.x + (0.45f * rect.width), rect.y + (0.1f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonLookSouth = new Rect(rect.x + (0.45f * rect.width), rect.y + (0.8f * rect.height), 0.1f * rect.width, 0.1f * rect.height);

            Rect buttonRotateLeft = new Rect(rect.x + (0.1f * rect.width), rect.y, 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonRotateRight = new Rect(rect.x + (0.8f * rect.width), rect.y, 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonRotateLeftPlus = new Rect(rect.x, rect.y, 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonRotateRightPlus = new Rect(rect.x + (0.9f * rect.width), rect.y, 0.1f * rect.width, 0.1f * rect.height);

            Rect buttonHulk = new Rect(rect.x, rect.y + rect.height, 0.2f * rect.width, 0.1f * rect.height);
            Rect buttonThin = new Rect(rect.x + (0.2f * rect.width), rect.y + rect.height, 0.2f * rect.width, 0.1f * rect.height);
            Rect buttonFat = new Rect(rect.x + (0.4f * rect.width), rect.y + rect.height, 0.2f * rect.width, 0.1f * rect.height);
            Rect buttonMale = new Rect(rect.x + (0.6f * rect.width), rect.y + rect.height, 0.2f * rect.width, 0.1f * rect.height);
            Rect buttonFemale = new Rect(rect.x + (0.8f * rect.width), rect.y + rect.height, 0.2f * rect.width, 0.1f * rect.height);

            if (Widgets.ButtonText(buttonWest, "-X", true, true, Color.blue, true))
            {
                var tempX = IR_HolstersSettings.WeaponDataSettings[currentType].pos[currentDir];
                tempX.x -= 0.05f;
                IR_HolstersSettings.WeaponDataSettings[currentType].pos[currentDir] = tempX;
            }
            if (Widgets.ButtonText(buttonEast, "+X"))
            {
                var tempX = IR_HolstersSettings.WeaponDataSettings[currentType].pos[currentDir];
                tempX.x += 0.05f;
                IR_HolstersSettings.WeaponDataSettings[currentType].pos[currentDir] = tempX;
            }
            if (Widgets.ButtonText(buttonNorth, "+Y"))
            {
                var tempX = IR_HolstersSettings.WeaponDataSettings[currentType].pos[currentDir];
                tempX.z += 0.05f;
                IR_HolstersSettings.WeaponDataSettings[currentType].pos[currentDir] = tempX;
            }
            if (Widgets.ButtonText(buttonSouth, "-Y"))
            {
                var tempX = IR_HolstersSettings.WeaponDataSettings[currentType].pos[currentDir];
                tempX.z -= 0.05f;
                IR_HolstersSettings.WeaponDataSettings[currentType].pos[currentDir] = tempX;
            }

            if (Widgets.ButtonText(buttonLayerMinus, "Layer -"))
            {
                var tempX = IR_HolstersSettings.WeaponDataSettings[currentType].pos[currentDir];
                tempX.y -= 0.1f;
                IR_HolstersSettings.WeaponDataSettings[currentType].pos[currentDir] = tempX;
            }

            if (Widgets.ButtonText(buttonLayerPlus, "Layer +"))
            {
                var tempX = IR_HolstersSettings.WeaponDataSettings[currentType].pos[currentDir];
                tempX.y += 0.1f;
                IR_HolstersSettings.WeaponDataSettings[currentType].pos[currentDir] = tempX;
            }

            if (Widgets.ButtonText(buttonLookWest, "W"))
            {
                currentDir = Rot4.West;
            }
            if (Widgets.ButtonText(buttonLookEast, "E"))
            {
                currentDir = Rot4.East;
            }
            if (Widgets.ButtonText(buttonLookNorth, "N"))
            {
                currentDir = Rot4.North;
            }
            if (Widgets.ButtonText(buttonLookSouth, "S"))
            {
                currentDir = Rot4.South;
            }

            if (Widgets.ButtonText(buttonRotateLeftPlus, "Rot --"))
            {
                var tempX = IR_HolstersSettings.WeaponDataSettings[currentType].angle[currentDir];
                tempX -= 5;
                IR_HolstersSettings.WeaponDataSettings[currentType].angle[currentDir] = tempX;
            }
            if (Widgets.ButtonText(buttonRotateLeft, "Rot -"))
            {
                var tempX = IR_HolstersSettings.WeaponDataSettings[currentType].angle[currentDir];
                tempX -= 1;
                IR_HolstersSettings.WeaponDataSettings[currentType].angle[currentDir] = tempX;
            }

            if (Widgets.ButtonText(buttonRotateRight, "Rot +"))
            {
                var tempX = IR_HolstersSettings.WeaponDataSettings[currentType].angle[currentDir];
                tempX += 1;
                IR_HolstersSettings.WeaponDataSettings[currentType].angle[currentDir] = tempX;
            }
            if (Widgets.ButtonText(buttonRotateRightPlus, "Rot ++"))
            {
                var tempX = IR_HolstersSettings.WeaponDataSettings[currentType].angle[currentDir];
                tempX += 5;
                IR_HolstersSettings.WeaponDataSettings[currentType].angle[currentDir] = tempX;
            }

            if (Widgets.ButtonText(buttonFat, "Fat"))
            {
                currentBody = BodyType.fat;
            }
            if (Widgets.ButtonText(buttonThin, "Thin"))
            {
                currentBody = BodyType.thin;
            }
            if (Widgets.ButtonText(buttonHulk, "Hulk"))
            {
                currentBody = BodyType.hulk;
            }
            if (Widgets.ButtonText(buttonMale, "Male"))
            {
                currentBody = BodyType.male;
            }
            if (Widgets.ButtonText(buttonFemale, "Female"))
            {
                currentBody = BodyType.female;
            }
        }

        private Texture ChooseBodyTexture()
        {
            return IR_Textures.bodies[currentBody][currentDir];
        }

        private Texture ChooseHeadTexture()
        {
            switch (currentBody)
            {
                default:
                case BodyType.hulk:
                case BodyType.fat:
                case BodyType.male:
                    return IR_Textures.maleHead[currentDir];
                    break;

                case BodyType.thin:
                case BodyType.female:
                    return IR_Textures.femaleHead[currentDir];
                    break;
            }
        }

        private float ChooseHeadOffset()
        {
            switch (currentBody)
            {
                default:
                case BodyType.hulk:
                    return BodyTypeDefOf.Hulk.headOffset.x;
                    break;

                case BodyType.fat:
                    return BodyTypeDefOf.Fat.headOffset.x;
                    break;

                case BodyType.male:
                    return BodyTypeDefOf.Male.headOffset.x;
                    break;

                case BodyType.thin:
                    return BodyTypeDefOf.Thin.headOffset.x;
                    break;

                case BodyType.female:
                    return BodyTypeDefOf.Female.headOffset.x;
                    break;
            }
        }

        private void InitSettingsDict()
        {
            if (IR_HolstersSettings.WeaponDataSettings.NullOrEmpty())
            {
                IR_HolstersSettings.InitWeaponDataSettings();
            }

            if (IR_HolstersSettings.WeaponSpecialType.NullOrEmpty())
            {
                IR_HolstersSettings.WeaponSpecialType = new Dictionary<string, WeaponType>();
            }
        }

        public override string SettingsCategory()
        {
            return "Holsters";
        }

        private BodyType currentBody = BodyType.male;
        private WeaponType currentType = WeaponType.longRanged;
        private Rot4 currentDir = Rot4.South;

        private const float pixelRatio = 96;
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