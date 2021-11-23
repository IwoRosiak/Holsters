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
            //IR_HolstersInit.InitWeaponDataSettings();
            //Widgets.DrawTextureFitted(new Rect(inRect.x, inRect.y, inRect.width, inRect.height), Textures.SettingsBackGround, 1); //later!

            //Rect masterRect = new Rect(inRect.x + (0.1f * inRect.width), inRect.y + 40, 0.8f * inRect.width, 936);
            //Rect TopSettings = new Rect(masterRect.x, masterRect.y, masterRect.width, 45);

            Listing_Standard listing = new Listing_Standard();

            listing.Begin(inRect);

            if (!IR_HolstersSettings.WeaponDataSettings.ContainsKey(currentType))
            {
                IR_HolstersSettings.InitWeaponDataSettings();
            }

            if (listing.ButtonText("Reset current type"))
            {
                IR_HolstersSettings.InitWeaponDataSettings();
            }

            if (listing.ButtonText("++ current type") && IR_HolstersSettings.WeaponDataSettings.ContainsKey(currentType + 1))
            {
                currentType++;
            }
            if (listing.ButtonText("-- current type") && IR_HolstersSettings.WeaponDataSettings.ContainsKey(currentType - 1))
            {
                currentType--;
            }

            listing.End();

            Rect bodyFrameRect = new Rect(inRect.x + (0.2f * inRect.width), inRect.y + (0.2f * inRect.height), 0.6f * inRect.width, 0.5f * inRect.width);
            Widgets.DrawBoxSolid(bodyFrameRect, Color.gray);

            if (currentDir == Rot4.North)
            {
                DrawHead(bodyFrameRect);
            }

            if (DrawingMode())
            {
                DrawWeapon(bodyFrameRect);
                DrawBody(bodyFrameRect);
                if (currentDir != Rot4.North)
                {
                    DrawHead(bodyFrameRect);
                }
            }
            else
            {
                DrawBody(bodyFrameRect);
                if (currentDir != Rot4.North)
                {
                    DrawHead(bodyFrameRect);
                }
                DrawWeapon(bodyFrameRect);
            }

            DrawBodyButton(bodyFrameRect);
        }

        private bool DrawingMode()
        {
            if (IR_WeaponData.GetWeaponPos(currentType, currentDir).y == 0f)
            {
                return false;
            }
            return true;
        }

        private void DrawWeapon(Rect rect)
        {
            Rect weaponRect = new Rect(rect.x + (0.2f * rect.width), rect.y + (0.3f * rect.height), 0.6f * rect.width, 0.4f * rect.width);

            Vector2 offset = new Vector2(IR_WeaponData.GetWeaponPos(currentType, currentDir).x, -IR_WeaponData.GetWeaponPos(currentType, currentDir).z);

            offset += GetBodySizeOffset();

            Widgets.DrawTextureRotated(weaponRect.center + offset * 100 - new Vector2(0, 34), ChooseTexture(), IR_WeaponData.GetWeaponAngle(currentType, currentDir));
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
            Rect bodyRect = new Rect(rect.x + (0.2f * rect.width), rect.y + (0.3f * rect.height), 0.6f * rect.width, 0.4f * rect.width);

            var texture = ChooseBodyTexture();

            Widgets.DrawTextureRotated(rect.center, texture, 0);
        }

        private void DrawHead(Rect rect)
        {
            Rect headRect = new Rect(rect.x + (0.2f * rect.width), rect.y + (0.2f * rect.height), 0.6f * rect.width, 0.4f * rect.width);

            var texture = ChooseHeadTexture();

            Widgets.DrawTextureRotated(rect.center - new Vector2(0, 34), texture, 0);
        }

        private void DrawBodyButton(Rect rect)
        {
            Rect buttonWest = new Rect(rect.x, rect.y + (0.45f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonEast = new Rect(rect.x + (0.9f * rect.width), rect.y + (0.45f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonNorth = new Rect(rect.x + (0.45f * rect.width), rect.y, 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonSouth = new Rect(rect.x + (0.45f * rect.width), rect.y + (0.9f * rect.height), 0.1f * rect.width, 0.1f * rect.height);

            Rect buttonLookWest = new Rect(rect.x + (0.1f * rect.width), rect.y + (0.45f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonLookEast = new Rect(rect.x + (0.8f * rect.width), rect.y + (0.45f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonLookNorth = new Rect(rect.x + (0.45f * rect.width), rect.y + (0.1f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonLookSouth = new Rect(rect.x + (0.45f * rect.width), rect.y + (0.8f * rect.height), 0.1f * rect.width, 0.1f * rect.height);

            Rect buttonHulk = new Rect(rect.x, rect.y, 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonThin = new Rect(rect.x + (0.1f * rect.width), rect.y, 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonFat = new Rect(rect.x + (0.2f * rect.width), rect.y, 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonMale = new Rect(rect.x + (0.3f * rect.width), rect.y, 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonFemale = new Rect(rect.x + (0.4f * rect.width), rect.y, 0.1f * rect.width, 0.1f * rect.height);

            if (Widgets.ButtonText(buttonWest, "-X"))
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

        private Texture ChooseTexture()
        {
            switch (currentType)
            {
                case WeaponType.grenades:
                    return IR_Textures.grenades;
                    break;

                case WeaponType.longRanged:
                    return IR_Textures.rifle;
                    break;

                case WeaponType.shortRanged:
                    return IR_Textures.revolver;
                    break;

                case WeaponType.longMelee:
                    return IR_Textures.sword;
                    break;

                case WeaponType.shortMelee:
                    return IR_Textures.knife;
                    break;

                case WeaponType.bow:
                    return IR_Textures.bow;
                    break;
            }
            return IR_Textures.sword;
        }

        private Texture ChooseBodyTexture()
        {
            switch (currentBody)
            {
                case BodyType.hulk:
                    return IR_Textures.hulkBody[currentDir];
                    break;

                case BodyType.fat:
                    return IR_Textures.fatBody[currentDir];
                    break;

                case BodyType.thin:
                    return IR_Textures.thinBody[currentDir];
                    break;

                case BodyType.male:
                    return IR_Textures.maleBody[currentDir];
                    break;

                case BodyType.female:
                    return IR_Textures.femaleBody[currentDir];
                    break;

                default:
                    return IR_Textures.maleBody[currentDir];
                    break;
            }
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

        private Vector2 BodyOffset()
        {
            return Vector2.zero;
        }

        private void InitSettingsDict()
        {
            if (IR_HolstersSettings.WeaponDataSettings.NullOrEmpty())
            {
                IR_HolstersSettings.InitWeaponDataSettings();
            }
        }

        public override string SettingsCategory()
        {
            return "Holsters";
        }

        private BodyType currentBody = BodyType.male;
        private WeaponType currentType = WeaponType.longMelee;
        private Rot4 currentDir = Rot4.South;
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