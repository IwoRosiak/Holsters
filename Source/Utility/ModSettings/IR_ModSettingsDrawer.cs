using Holsters;
using RimWorld;
using RimWorldHolsters.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.Noise;
using static System.Net.Mime.MediaTypeNames;

namespace RimWorldHolsters
{
    internal static class IR_ModSettingsDrawer
    {
        public static IR_HolstersMod mod;
        public static void DrawPawn(Rect bodyFrameRect)
        {

            Widgets.DrawTextureFitted(bodyFrameRect, IR_Textures.backgroundPawn, 1);

            if (!IsWeaponInFrontLayer())
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

            DrawButtons(bodyFrameRect);
        }

        internal static bool IsWeaponInFrontLayer()
        {
            return mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).IsAtFront;
        }

        private static void DrawWeapon(Rect rect)
        {
            if (mod.CurWeapons.NullOrEmpty()) //||mod.GetCurGroup()sIndex == WeaponType.doNotDisplay)
            {
                return;
            }
            Vector2 offset = new Vector2(IR_HolstersSettings.GetWeaponPos(mod.GetCurGroup(), mod.CurDir, mod.IsSidearmMode, mod.CurrentBody).x, -IR_HolstersSettings.GetWeaponPos(mod.GetCurGroup(), mod.CurDir, mod.IsSidearmMode, mod.CurrentBody).z);

            Texture text = mod.GetCurWeapon().graphic.MatNorth.mainTexture;
            float scale = (((1 / mod.GetCurWeapon().uiIconScale) / (text.width / 64)) * 1.35f * mod.GetCurWeapon().graphic.drawSize.x) * mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Size;

            //Widgets.DrawTextureRotated(rect.center + (offset * pixelRatio), text, IR_WeaponData.GetWeaponAngle(GetCurGroup()sIndex, currentDir), scale);

            Vector2 center = rect.center + (offset * IR_HolstersMod.PIXEL_RATIO);

            float num = text.width * scale;

            float num2 = text.height * scale;

            if (mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).IsFlipped)
            {
                num2 = text.height * -scale;
            }

            Widgets.DrawTextureRotated(new Rect(center.x - num / 2f, center.y - num2 / 2f, num, num2), text, IR_HolstersSettings.GetWeaponAngle(mod.GetCurGroup(), mod.CurDir, mod.IsSidearmMode));
        }

        private static void DrawBody(Rect rect)
        {
            //Rect bodyRect = new Rect(rect.x + (0.2f * rect.width), rect.y + (0.3f * rect.height), 0.6f * rect.width, 0.6f * rect.width);

            Texture texture = ChooseBodyTexture();

            float width = texture.width;

            float height = texture.height;
            
            
            if (mod.CurDir == Rot4.West)
            {
                width *= -1;
            }

            Widgets.DrawTextureRotated(new Rect(rect.center.x - width / 2f, rect.center.y - height / 2f, width, height), texture, 0);

        }

        private static void DrawHead(Rect rect)
        {
            //Rect headRect = new Rect(rect.x + (0.2f * rect.width), rect.y + (0.3f * rect.height), 0.6f * rect.width, 0.6f * rect.width);

            Texture texture = ChooseHeadTexture();

            float offset = 0;

            if (mod.CurDir == Rot4.East)
            {
                offset = -ChooseHeadOffset();
            }
            else if (mod.CurDir == Rot4.West)
            {
                offset = ChooseHeadOffset();
            }

            Widgets.DrawTextureRotated(rect.center - new Vector2(offset * IR_HolstersMod.PIXEL_RATIO, 34), texture, 0);
        }

        internal static void DrawButtons(Rect rect)
        {
            DrawDirectionsButtons(rect);
            DrawSizeButtons(rect);
            DrawAngleButtons(rect);
            DrawPositionButtons(rect);
            DrawLayerButtons(rect);

            var buttonFlip = new Rect(rect.x + (0.2f * rect.width), rect.y +(rect.height), 0.15f * rect.width, 0.1f * rect.height);

            var textPosition = new Rect(rect.x + (0.1f * rect.width), rect.y + (0.02f * rect.height), 0.3f * rect.width, 0.1f * rect.height);
            var textBodyPos = new Rect(rect.x + (0.1f * rect.width), rect.y + (0.12f * rect.height), 0.3f * rect.width, 0.1f * rect.height);

            //FLIP
            if (Widgets.ButtonText(buttonFlip, "Flip"))
            {
                bool flip = mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).IsFlipped;
                mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).IsFlipped = !flip;
                return;
            }

            Vector2 pos = new Vector2(IR_HolstersSettings.GetWeaponPos(mod.GetCurGroup(), mod.CurDir, mod.IsSidearmMode).x, -IR_HolstersSettings.GetWeaponPos(mod.GetCurGroup(), mod.CurDir, mod.IsSidearmMode).z);
            //+Math.Round(IR_HolstersSettings.GetWeaponPos(mod.GetCurGroup(), mod.curDir, mod.isSidearmMode).x, 4) + ", " + Math.Round(IR_HolstersSettings.GetWeaponPos(mod.GetCurGroup(), mod.curDir, mod.isSidearmMode).z, 4)
            Widgets.Label(textPosition, "Position: " + Math.Round(pos.x, 3) + " and " + -Math.Round(pos.y, 3));

            //TODO: Body offsets! Vector3 bodyOffset = mod.GetCurGroup().GetBodyOffset(mod.curDir, mod.isSidearmMode) * mod.GetCurGroup().GetBodyOffsetModif(mod.currentBody, mod.isSidearmMode);


            //TODO: Body offsets! Widgets.Label(textBodyPos, "Body offset: " + Math.Round(bodyOffset.x, 3) + ", " + Math.Round(bodyOffset.z, 3));
            //Widgets.Label(textPosition, "Total: " + Math.Round(mod.GetCurGroup().GetBodyOffset(mod.curDir, mod.isSidearmMode).x, 3) + ", " + Math.Round(mod.GetCurGroup().GetBodyOffset(mod.curDir, mod.isSidearmMode).z, 3));
        }

        private static void DrawLayerButtons(Rect rect)
        {
            Rect buttonLayer = new Rect(rect.x, rect.y + (rect.height), 0.2f * rect.width, 0.1f * rect.height);

            string layerLabel = IsWeaponInFrontLayer() ? "front" : "back";

            if (Widgets.ButtonText(buttonLayer, "Layer: " + layerLabel))
            {
                var temp = mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).IsAtFront;
                temp = !temp;
                mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).IsAtFront = temp;
            }
        }

        private static void DrawPositionButtons(Rect rect)
        {
            var buttonWest = new Rect(rect.x, rect.y + (0.45f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            var buttonEast = new Rect(rect.x + (0.9f * rect.width), rect.y + (0.45f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            var buttonNorth = new Rect(rect.x + (0.45f * rect.width), rect.y, 0.1f * rect.width, 0.1f * rect.height);
            var buttonSouth = new Rect(rect.x + (0.45f * rect.width), rect.y + (0.9f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
       
            if (Widgets.ButtonText(buttonWest, "-X"))
            {
                Vector3 temp = mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Position;
                temp.x -= 0.05f;
                mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Position = temp;
            }
            if (Widgets.ButtonText(buttonEast, "+X"))
            {
                Vector3 temp = mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Position;
                temp.x += 0.05f;
                mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Position = temp;
            }
            if (Widgets.ButtonText(buttonNorth, "+Y"))
            {
                Vector3 temp = mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Position;
                temp.z += 0.05f;
                mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Position = temp;
            }
            if (Widgets.ButtonText(buttonSouth, "-Y"))
            {
                Vector3 temp = mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Position;
                temp.z -= 0.05f;
                mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Position = temp;
            }
        }

        private static void DrawDirectionsButtons(Rect rect)
        {
            var buttonLookWest = new Rect(rect.x + (0.1f * rect.width), rect.y + (0.45f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            var buttonLookEast = new Rect(rect.x + (0.8f * rect.width), rect.y + (0.45f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            var buttonLookNorth = new Rect(rect.x + (0.45f * rect.width), rect.y + (0.1f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            var buttonLookSouth = new Rect(rect.x + (0.45f * rect.width), rect.y + (0.8f * rect.height), 0.1f * rect.width, 0.1f * rect.height);

            //DIRECTION
            if (Widgets.ButtonText(buttonLookWest, "W"))
            {
                mod.CurDir = Rot4.West;
            }
            if (Widgets.ButtonText(buttonLookEast, "E"))
            {
                mod.CurDir = Rot4.East;
            }
            if (Widgets.ButtonText(buttonLookNorth, "N"))
            {
                mod.CurDir = Rot4.North;
            }
            if (Widgets.ButtonText(buttonLookSouth, "S"))
            {
                mod.CurDir = Rot4.South;
            }
        }

        internal static void DrawSizeButtons(Rect rect)
        {
            var buttonPlusSize = new Rect(rect.x + (0.6f * rect.width), rect.y + (1f * rect.height), 0.05f * rect.width, 0.1f * rect.height);
            var buttonSize = new Rect(rect.x + (0.4f * rect.width), rect.y + (1f * rect.height), 0.2f * rect.width, 0.1f * rect.height);
            var buttonMinusSize = new Rect(rect.x + (0.35f * rect.width), rect.y + (1f * rect.height), 0.05f * rect.width, 0.1f * rect.height);

            if (Widgets.ButtonText(buttonMinusSize, "<"))
            {
                var temp = mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Size;
                temp -= 0.05f;
                mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Size = temp;
            }

            _ = Widgets.ButtonText(buttonSize, "Size: " + Math.Round(mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Size * 100, 3) + "%");

            if (Widgets.ButtonText(buttonPlusSize, ">"))
            {
                var temp = mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Size;
                temp += 0.05f;
                mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Size = temp;
            }
        }

        internal static void DrawAngleButtons(Rect rect)
        {
            var buttonRotateLeft = new Rect(rect.x + (0.7f * rect.width), rect.y + rect.height, 0.05f * rect.width, 0.1f * rect.height);
            var buttonRotateRight = new Rect(rect.x + (0.90f * rect.width), rect.y + rect.height, 0.05f * rect.width, 0.1f * rect.height);

            var buttonAngle = new Rect(rect.x + (0.75f * rect.width), rect.y + rect.height, 0.15f * rect.width, 0.1f * rect.height);

            var buttonRotateLeftPlus = new Rect(rect.x + (0.65f * rect.width), rect.y + rect.height, 0.05f * rect.width, 0.1f * rect.height);
            var buttonRotateRightPlus = new Rect(rect.x + (0.95f * rect.width), rect.y + rect.height, 0.05f * rect.width, 0.1f * rect.height);



            _ = Widgets.ButtonText(buttonAngle, "Angle: " + IR_HolstersSettings.GetWeaponAngle(mod.GetCurGroup(), mod.CurDir, mod.IsSidearmMode) % 360 + "°");

            if (Widgets.ButtonText(buttonRotateLeftPlus, "<<"))
            {
                var tempX = mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Rotation;
                tempX -= 5;
                mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Rotation = tempX;
            }
            if (Widgets.ButtonText(buttonRotateLeft, "<"))
            {
                var tempX = mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Rotation;
                tempX -= 1;
                mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Rotation = tempX;
            }

            if (Widgets.ButtonText(buttonRotateRight, ">"))
            {
                var tempX = mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Rotation;
                tempX += 1;
                mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Rotation = tempX;
            }
            if (Widgets.ButtonText(buttonRotateRightPlus, ">>"))
            {
                var tempX = mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Rotation;
                tempX += 5;
                mod.GetCurGroup().HolsterRenderData.GetConfiguration(mod.CurDir, mod.IsSidearmMode).Rotation = tempX;
            }
        }
        /*
        internal static void DrawBodyManagement(Rect rect)
        {
            Rect buttonHulk = new Rect(rect.x + (0.1f * rect.width), rect.y + (0.1f * rect.height), 0.3f * rect.width, 0.1f * rect.height);
            Rect buttonThin = new Rect(rect.x + (0.1f * rect.width), rect.y + (0.2f * rect.height), 0.3f * rect.width, 0.1f * rect.height);
            Rect buttonFat = new Rect(rect.x + (0.1f * rect.width), rect.y + (0.3f * rect.height), 0.3f * rect.width, 0.1f * rect.height);
            Rect buttonMale = new Rect(rect.x + (0.1f * rect.width), rect.y + (0.4f * rect.height), 0.3f * rect.width, 0.1f * rect.height);
            Rect buttonFemale = new Rect(rect.x + (0.1f * rect.width), rect.y + (0.5f * rect.height), 0.3f * rect.width, 0.1f * rect.height);

            Rect buttonHulkImpact = new Rect(rect.x + (0.4f * rect.width), rect.y + (0.1f * rect.height), 0.5f * rect.width, 0.1f * rect.height);
            Rect buttonThinImpact = new Rect(rect.x + (0.4f * rect.width), rect.y + (0.2f * rect.height), 0.5f * rect.width, 0.1f * rect.height);
            Rect buttonFatImpact = new Rect(rect.x + (0.4f * rect.width), rect.y + (0.3f * rect.height), 0.5f * rect.width, 0.1f * rect.height);
            Rect buttonMaleImpact = new Rect(rect.x + (0.4f * rect.width), rect.y + (0.4f * rect.height), 0.5f * rect.width, 0.1f * rect.height);
            Rect buttonFemaleImpact = new Rect(rect.x + (0.4f * rect.width), rect.y + (0.5f * rect.height), 0.5f * rect.width, 0.1f * rect.height);

            Rect buttonHulkImpactLess = new Rect(rect.x, rect.y + (0.1f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonThinImpactLess = new Rect(rect.x, rect.y + (0.2f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonFatImpactLess = new Rect(rect.x, rect.y + (0.3f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonMaleImpactLess = new Rect(rect.x, rect.y + (0.4f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonFemaleImpactLess = new Rect(rect.x, rect.y + (0.5f * rect.height), 0.1f * rect.width, 0.1f * rect.height);

            Rect buttonHulkImpactMore = new Rect(rect.x + (0.9f * rect.width), rect.y + (0.1f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonThinImpactMore = new Rect(rect.x + (0.9f * rect.width), rect.y + (0.2f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonFatImpactMore = new Rect(rect.x + (0.9f * rect.width), rect.y + (0.3f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonMaleImpactMore = new Rect(rect.x + (0.9f * rect.width), rect.y + (0.4f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonFemaleImpactMore = new Rect(rect.x + (0.9f * rect.width), rect.y + (0.5f * rect.height), 0.1f * rect.width, 0.1f * rect.height);

            Rect buttonBodyWest = new Rect(rect.x , rect.y + (0.6f * rect.height), 0.5f * rect.width, 0.1f * rect.height);
            Rect buttonBodyEast = new Rect(rect.x + (0.5f * rect.width), rect.y + (0.6f * rect.height), 0.5f * rect.width, 0.1f * rect.height);
            Rect buttonBodyNorth = new Rect(rect.x + (0.5f * rect.width), rect.y + (0.7f * rect.height), 0.5f * rect.width, 0.1f * rect.height);
            Rect buttonBodySouth = new Rect(rect.x, rect.y + (0.7f * rect.height), 0.5f * rect.width, 0.1f * rect.height);

            Rect gapLine = rect;

            gapLine.height = 30;

            Widgets.Label(gapLine, "Body Types ");
            Widgets.DrawLineHorizontal(gapLine.x, gapLine.y + 24f, gapLine.width);

            //BODIES
            if (Widgets.ButtonText(buttonFat, "Fat"))
            {
                mod.currentBody = BodyType.fat;
            }
            if (Widgets.ButtonText(buttonThin, "Thin"))
            {
                mod.currentBody = BodyType.thin;
            }
            if (Widgets.ButtonText(buttonHulk, "Hulk"))
            {
                mod.currentBody = BodyType.hulk;
            }
            if (Widgets.ButtonText(buttonMale, "Male"))
            {
                mod.currentBody = BodyType.male;
            }
            if (Widgets.ButtonText(buttonFemale, "Female"))
            {
                mod.currentBody = BodyType.female;
            }

            Widgets.ButtonText(buttonFatImpact, "Impact: " + Math.Round(mod.GetCurGroup().GetBodyOffsetModif(BodyType.fat, mod.isSidearmMode)*100, 3) + "%");
            Widgets.ButtonText(buttonThinImpact, "Impact: " + Math.Round(mod.GetCurGroup().GetBodyOffsetModif(BodyType.thin, mod.isSidearmMode) * 100, 3) + "%");
            Widgets.ButtonText(buttonHulkImpact, "Impact: " + Math.Round(mod.GetCurGroup().GetBodyOffsetModif(BodyType.hulk, mod.isSidearmMode) * 100, 3) + "%");
            Widgets.ButtonText(buttonMaleImpact, "Impact: " + Math.Round(mod.GetCurGroup().GetBodyOffsetModif(BodyType.male, mod.isSidearmMode) * 100, 3) + "%");
            Widgets.ButtonText(buttonFemaleImpact, "Impact: " + Math.Round(mod.GetCurGroup().GetBodyOffsetModif(BodyType.female, mod.isSidearmMode) * 100, 3) + "%");

            if (Widgets.ButtonText(buttonFatImpactLess, "<"))
            {
                float bodyModif = mod.GetCurGroup().GetBodyOffsetModif(BodyType.fat, mod.isSidearmMode);
                bodyModif -=0.05f;

                mod.GetCurGroup().SetBodyOffsetModif(BodyType.fat, bodyModif, mod.isSidearmMode);
            }
            if (Widgets.ButtonText(buttonThinImpactLess, "<"))
            {
                float bodyModif = mod.GetCurGroup().GetBodyOffsetModif(BodyType.thin, mod.isSidearmMode);
                bodyModif -= 0.05f;

                mod.GetCurGroup().SetBodyOffsetModif(BodyType.thin, bodyModif, mod.isSidearmMode);
            }
            if (Widgets.ButtonText(buttonHulkImpactLess, "<"))
            {
                float bodyModif = mod.GetCurGroup().GetBodyOffsetModif(BodyType.hulk, mod.isSidearmMode);
                bodyModif -= 0.05f;

                mod.GetCurGroup().SetBodyOffsetModif(BodyType.hulk, bodyModif, mod.isSidearmMode);
            }
            if (Widgets.ButtonText(buttonMaleImpactLess, "<"))
            {
                float bodyModif = mod.GetCurGroup().GetBodyOffsetModif(BodyType.male, mod.isSidearmMode);
                bodyModif -= 0.05f;

                mod.GetCurGroup().SetBodyOffsetModif(BodyType.male, bodyModif, mod.isSidearmMode);
            }
            if (Widgets.ButtonText(buttonFemaleImpactLess, "<"))
            {
                float bodyModif = mod.GetCurGroup().GetBodyOffsetModif(BodyType.female, mod.isSidearmMode);
                bodyModif -= 0.05f;

                mod.GetCurGroup().SetBodyOffsetModif(BodyType.female, bodyModif, mod.isSidearmMode);
            }


            if (Widgets.ButtonText(buttonFatImpactMore, ">"))
            {
                float bodyModif = mod.GetCurGroup().GetBodyOffsetModif(BodyType.fat, mod.isSidearmMode);
                bodyModif += 0.05f;

                mod.GetCurGroup().SetBodyOffsetModif(BodyType.fat, bodyModif, mod.isSidearmMode);
            }
            if (Widgets.ButtonText(buttonThinImpactMore, ">"))
            {
                float bodyModif = mod.GetCurGroup().GetBodyOffsetModif(BodyType.thin, mod.isSidearmMode);
                bodyModif += 0.05f;

                mod.GetCurGroup().SetBodyOffsetModif(BodyType.thin, bodyModif, mod.isSidearmMode);
            }
            if (Widgets.ButtonText(buttonHulkImpactMore, ">"))
            {
                float bodyModif = mod.GetCurGroup().GetBodyOffsetModif(BodyType.hulk, mod.isSidearmMode);
                bodyModif += 0.05f;

                mod.GetCurGroup().SetBodyOffsetModif(BodyType.hulk, bodyModif, mod.isSidearmMode);
            }
            if (Widgets.ButtonText(buttonMaleImpactMore, ">"))
            {
                float bodyModif = mod.GetCurGroup().GetBodyOffsetModif(BodyType.male, mod.isSidearmMode);
                bodyModif += 0.05f;

                mod.GetCurGroup().SetBodyOffsetModif(BodyType.male, bodyModif, mod.isSidearmMode);
            }
            if (Widgets.ButtonText(buttonFemaleImpactMore, ">"))
            {
                float bodyModif = mod.GetCurGroup().GetBodyOffsetModif(BodyType.female, mod.isSidearmMode);
                bodyModif += 0.05f;

                mod.GetCurGroup().SetBodyOffsetModif(BodyType.female, bodyModif, mod.isSidearmMode);
            }



            if (Widgets.ButtonText(buttonBodyWest, "-X", true, true, Color.blue, true))
            {
                var bodyOffset = mod.GetCurGroup().GetBodyOffset(mod.curDir, mod.isSidearmMode);
                bodyOffset.x -= 0.05f;
                mod.GetCurGroup().SetBodyOffset(mod.curDir, bodyOffset, mod.isSidearmMode);
            }
            if (Widgets.ButtonText(buttonBodyEast, "+X"))
            {
                var bodyOffset = mod.GetCurGroup().GetBodyOffset(mod.curDir, mod.isSidearmMode);
                bodyOffset.x += 0.05f;
                mod.GetCurGroup().SetBodyOffset(mod.curDir, bodyOffset, mod.isSidearmMode);
            }
            if (Widgets.ButtonText(buttonBodyNorth, "+Y"))
            {
                var bodyOffset = mod.GetCurGroup().GetBodyOffset(mod.curDir, mod.isSidearmMode);
                bodyOffset.z += 0.05f;
                mod.GetCurGroup().SetBodyOffset(mod.curDir, bodyOffset, mod.isSidearmMode);
            }
            if (Widgets.ButtonText(buttonBodySouth, "-Y"))
            {
                var bodyOffset = mod.GetCurGroup().GetBodyOffset(mod.curDir, mod.isSidearmMode);
                bodyOffset.z -= 0.05f;
                mod.GetCurGroup().SetBodyOffset(mod.curDir, bodyOffset, mod.isSidearmMode);
            }
        }*/

        private static Texture ChooseBodyTexture() => BodyTypeDataProvider.AllBodyTypes.SingleOrDefault(b => b.DefName.ToLower() == mod.CurrentBody.ToString()).BodyTextures[mod.CurDir];

        private static Texture ChooseHeadTexture()
        {
            switch (mod.CurrentBody)
            {
                default:
                case BodyType.hulk:
                case BodyType.fat:
                case BodyType.male:
                    return IR_Textures.maleHead[mod.CurDir];
                    break;

                case BodyType.thin:
                case BodyType.female:
                    return IR_Textures.femaleHead[mod.CurDir];
                    break;
            }
        }

        private static float ChooseHeadOffset()
        {
            switch (mod.CurrentBody)
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
    }
}
