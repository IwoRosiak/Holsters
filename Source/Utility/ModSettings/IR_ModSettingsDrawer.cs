using RimWorld;
using RimWorldHolsters.Utility;
using System;
using System.Linq;
using UnityEngine;
using Verse;

namespace RimWorldHolsters
{
    internal static class IR_ModSettingsDrawer
    {
        public static IR_HolstersMod Mod;
        internal static int CurrentHeadIndex = 0;

        internal static HeadTypeData CurrentHead
        {
            get
            {
                if (CurrentHeadIndex >= HeadTypeDataProvider.AllHeadTypes.Count())
                    CurrentHeadIndex = 0;

                return HeadTypeDataProvider.AllHeadTypes.ToArray()[CurrentHeadIndex];
            }
        }

        internal static HeadTypeData GetCurrentHead()
        {
            return HeadTypeDataProvider.AllHeadTypes.ToArray()[CurrentHeadIndex];
        }

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

        internal static bool IsWeaponInFrontLayer() => Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).IsAtFront;

        private static void DrawWeapon(Rect rect)
        {
            if (Mod.CurWeapons.NullOrEmpty()) //||mod.GetCurGroup()sIndex == WeaponType.doNotDisplay)
                return;

            var offset = new Vector2(IR_HolstersSettings.GetWeaponPos(Mod.GetCurGroup(), Mod.CurDir, Mod.IsSidearmMode, Mod.CurrentBody).x, -IR_HolstersSettings.GetWeaponPos(Mod.GetCurGroup(), Mod.CurDir, Mod.IsSidearmMode, Mod.CurrentBody).z);

            Texture text = Mod.GetCurWeapon().graphic.MatNorth.mainTexture;
            float scale = (((1 / Mod.GetCurWeapon().uiIconScale) / (text.width / 64)) * 1.35f * Mod.GetCurWeapon().graphic.drawSize.x) * Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Size;

            Vector2 center = rect.center + (offset * IR_HolstersMod.PIXEL_RATIO);

            float num = text.width * scale;

            float num2 = text.height * scale;

            if (Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).IsFlipped)
            {
                num2 = text.height * -scale;
            }

            Widgets.DrawTextureRotated(new Rect(center.x - num / 2f, center.y - num2 / 2f, num, num2), text, IR_HolstersSettings.GetWeaponAngle(Mod.GetCurGroup(), Mod.CurDir, Mod.IsSidearmMode));
        }

        private static void DrawBody(Rect rect)
        {
            Texture texture = ChooseBodyTexture();

            float width = texture.width;
            float height = texture.height;

            if (Mod.CurDir == Rot4.West)
            {
                width *= -1;
            }

            Widgets.DrawTextureRotated(new Rect(rect.center.x - width / 2f, rect.center.y - height / 2f, width, height), texture, 0);
        }

        private static void DrawHead(Rect rect)
        {
            Texture texture = ChooseHeadTexture();

            Vector2 offset = Vector2.zero;

            float width = texture.width * Mod.CurrentBody.HeadSizeFactor;
            float height = texture.height * Mod.CurrentBody.HeadSizeFactor;

            Vector2 headOffset = ChooseHeadOffset();
            
            switch (Mod.CurDir.AsInt)
            {
                case 0: // North
                case 2: // South
                    offset = new Vector2(0f, headOffset.y);
                    break;
                case 1: //East
                    offset = new Vector2(-headOffset.x, headOffset.y);
                    break;
                case 3: // West
                    width *= -1;
                    offset = new Vector2(headOffset.x, headOffset.y);
                    break;
            }

            Vector2 anchorOffset = Mod.CurrentBody.HeadAnchors[Mod.CurDir];

            offset = new Vector2(offset.x - anchorOffset.x, offset.y - anchorOffset.y);

            offset *= IR_HolstersMod.PIXEL_RATIO;

            var headRect = new Rect(rect.center.x - width / 2f - offset.x, rect.center.y - height / 2f - offset.y, width, height);
            
            Widgets.DrawTextureRotated(headRect, texture, 0);
        }

        internal static void DrawButtons(Rect rect)
        {
            DrawDirectionsButtons(rect);
            DrawSizeButtons(rect);
            DrawAngleButtons(rect);
            DrawPositionButtons(rect);
            DrawLayerButtons(rect);

            var buttonFlip = new Rect(rect.x + (0.2f * rect.width), rect.y + (rect.height), 0.15f * rect.width, 0.1f * rect.height);

            var textPosition = new Rect(rect.x + (0.1f * rect.width), rect.y + (0.02f * rect.height), 0.3f * rect.width, 0.1f * rect.height);
            var textBodyPos = new Rect(rect.x + (0.1f * rect.width), rect.y + (0.12f * rect.height), 0.3f * rect.width, 0.1f * rect.height);

            //FLIP
            if (Widgets.ButtonText(buttonFlip, "Flip"))
            {
                bool flip = Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).IsFlipped;
                Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).IsFlipped = !flip;
                return;
            }

            var pos = new Vector2(IR_HolstersSettings.GetWeaponPos(Mod.GetCurGroup(), Mod.CurDir, Mod.IsSidearmMode).x, -IR_HolstersSettings.GetWeaponPos(Mod.GetCurGroup(), Mod.CurDir, Mod.IsSidearmMode).z);

            Widgets.Label(textPosition, "Position: " + Math.Round(pos.x, 3) + " and " + -Math.Round(pos.y, 3));

            Vector3 bodyOffset = Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).BodyOffset * Mod.GetCurGroup().HolsterRenderData.GetBodyModifier(Mod.CurrentBody.DefName, Mod.IsSidearmMode);

            Widgets.Label(textBodyPos, "Body offset: " + Math.Round(bodyOffset.x, 3) + ", " + Math.Round(bodyOffset.z, 3));
            //Widgets.Label(textPosition, "Total: " + Math.Round(mod.GetCurGroup().GetBodyOffset(mod.curDir, mod.isSidearmMode).x, 3) + ", " + Math.Round(mod.GetCurGroup().GetBodyOffset(mod.curDir, mod.isSidearmMode).z, 3));
        }

        private static void DrawLayerButtons(Rect rect)
        {
            var buttonLayer = new Rect(rect.x, rect.y + (rect.height), 0.2f * rect.width, 0.1f * rect.height);

            string layerLabel = IsWeaponInFrontLayer() ? "front" : "back";

            if (Widgets.ButtonText(buttonLayer, "Layer: " + layerLabel))
            {
                var temp = Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).IsAtFront;
                temp = !temp;
                Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).IsAtFront = temp;
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
                Vector3 temp = Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Position;
                temp.x -= 0.05f;
                Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Position = temp;
            }

            if (Widgets.ButtonText(buttonEast, "+X"))
            {
                Vector3 temp = Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Position;
                temp.x += 0.05f;
                Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Position = temp;
            }

            if (Widgets.ButtonText(buttonNorth, "+Y"))
            {
                Vector3 temp = Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Position;
                temp.z += 0.05f;
                Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Position = temp;
            }

            if (Widgets.ButtonText(buttonSouth, "-Y"))
            {
                Vector3 temp = Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Position;
                temp.z -= 0.05f;
                Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Position = temp;
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
                Mod.CurDir = Rot4.West;
            }

            if (Widgets.ButtonText(buttonLookEast, "E"))
            {
                Mod.CurDir = Rot4.East;
            }

            if (Widgets.ButtonText(buttonLookNorth, "N"))
            {
                Mod.CurDir = Rot4.North;
            }

            if (Widgets.ButtonText(buttonLookSouth, "S"))
            {
                Mod.CurDir = Rot4.South;
            }
        }

        internal static void DrawSizeButtons(Rect rect)
        {
            var buttonPlusSize = new Rect(rect.x + (0.6f * rect.width), rect.y + (1f * rect.height), 0.05f * rect.width, 0.1f * rect.height);
            var buttonSize = new Rect(rect.x + (0.4f * rect.width), rect.y + (1f * rect.height), 0.2f * rect.width, 0.1f * rect.height);
            var buttonMinusSize = new Rect(rect.x + (0.35f * rect.width), rect.y + (1f * rect.height), 0.05f * rect.width, 0.1f * rect.height);

            if (Widgets.ButtonText(buttonMinusSize, "<"))
            {
                var temp = Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Size;
                temp -= 0.05f;
                Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Size = temp;
            }

            _ = Widgets.ButtonText(buttonSize, "Size: " + Math.Round(Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Size * 100, 3) + "%");

            if (Widgets.ButtonText(buttonPlusSize, ">"))
            {
                var temp = Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Size;
                temp += 0.05f;
                Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Size = temp;
            }
        }

        internal static void DrawAngleButtons(Rect rect)
        {
            var buttonRotateLeft = new Rect(rect.x + (0.7f * rect.width), rect.y + rect.height, 0.05f * rect.width, 0.1f * rect.height);
            var buttonRotateRight = new Rect(rect.x + (0.90f * rect.width), rect.y + rect.height, 0.05f * rect.width, 0.1f * rect.height);

            var buttonAngle = new Rect(rect.x + (0.75f * rect.width), rect.y + rect.height, 0.15f * rect.width, 0.1f * rect.height);

            var buttonRotateLeftPlus = new Rect(rect.x + (0.65f * rect.width), rect.y + rect.height, 0.05f * rect.width, 0.1f * rect.height);
            var buttonRotateRightPlus = new Rect(rect.x + (0.95f * rect.width), rect.y + rect.height, 0.05f * rect.width, 0.1f * rect.height);

            _ = Widgets.ButtonText(buttonAngle, "Angle: " + IR_HolstersSettings.GetWeaponAngle(Mod.GetCurGroup(), Mod.CurDir, Mod.IsSidearmMode) % 360 + "°");

            if (Widgets.ButtonText(buttonRotateLeftPlus, "<<"))
            {
                var tempX = Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Rotation;
                tempX -= 5;
                Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Rotation = tempX;
            }

            if (Widgets.ButtonText(buttonRotateLeft, "<"))
            {
                var tempX = Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Rotation;
                tempX -= 1;
                Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Rotation = tempX;
            }

            if (Widgets.ButtonText(buttonRotateRight, ">"))
            {
                var tempX = Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Rotation;
                tempX += 1;
                Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Rotation = tempX;
            }

            if (Widgets.ButtonText(buttonRotateRightPlus, ">>"))
            {
                var tempX = Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Rotation;
                tempX += 5;
                Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).Rotation = tempX;
            }
        }

        internal static void DrawBodyManagement(Rect rect)
        {
            Rect gapLine = rect;

            gapLine.height = 30;

            Widgets.Label(gapLine, "Body Types ");
            Widgets.DrawLineHorizontal(gapLine.x, gapLine.y + 24f, gapLine.width);

            Rect headManagementRect = new Rect(rect.x, rect.y + 30f, rect.width, rect.height);

            DrawHeadManagement(headManagementRect);

            float height = 0.1f * rect.height;
            float positionY = rect.y + 30f + height;
           
            foreach (BodyTypeData bodyType in BodyTypeDataProvider.AllBodyTypes)
            {
                var buttonReduceImpact = new Rect(rect.x, positionY, 0.1f * rect.width, height);
                var buttonBody = new Rect(rect.x + (0.1f * rect.width), positionY, 0.3f * rect.width, height);
                var buttonDisplayImpact = new Rect(rect.x + (0.4f * rect.width), positionY, 0.5f * rect.width, height);
                var buttonIncreaseImpact = new Rect(rect.x + (0.9f * rect.width), positionY, 0.1f * rect.width, height);

                if (Widgets.ButtonText(buttonBody, bodyType.DefName))
                {
                    Mod.CurrentBody = bodyType;
                }

                var offsetModifier = Mod.GetCurGroup().HolsterRenderData.GetBodyModifier(bodyType.DefName, Mod.IsSidearmMode);

                _ = Widgets.ButtonText(buttonDisplayImpact, "Impact: " + Math.Round(offsetModifier * 100, 3) + "%");

                if (Widgets.ButtonText(buttonReduceImpact, "<"))
                {
                    offsetModifier -= 0.05f;

                    Mod.GetCurGroup().HolsterRenderData.SetBodyModifier(bodyType.DefName, Mod.IsSidearmMode, offsetModifier);
                }

                if (Widgets.ButtonText(buttonIncreaseImpact, ">"))
                {
                    offsetModifier += 0.05f;

                    Mod.GetCurGroup().HolsterRenderData.SetBodyModifier(bodyType.DefName, Mod.IsSidearmMode, offsetModifier);
                }

                positionY += height;
            }

            float width = 0.5f * rect.width;
            var buttonBodyWest = new Rect(rect.x, positionY, width, height);
            var buttonBodyEast = new Rect(rect.x + width, positionY, width, height);
            positionY += height;
            var buttonBodyNorth = new Rect(rect.x + width, positionY, width, height);
            var buttonBodySouth = new Rect(rect.x, positionY, width, height);

            if (Widgets.ButtonText(buttonBodyWest, "-X", true, true, Color.blue, true))
            {
                Vector3 bodyOffset = Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).BodyOffset;
                bodyOffset.x -= 0.05f;
                Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).BodyOffset = bodyOffset;
            }

            if (Widgets.ButtonText(buttonBodyEast, "+X"))
            {
                Vector3 bodyOffset = Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).BodyOffset;
                bodyOffset.x += 0.05f;
                Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).BodyOffset = bodyOffset;
            }

            if (Widgets.ButtonText(buttonBodyNorth, "+Y"))
            {
                Vector3 bodyOffset = Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).BodyOffset;
                bodyOffset.z += 0.05f;
                Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).BodyOffset = bodyOffset;
            }

            if (Widgets.ButtonText(buttonBodySouth, "-Y"))
            {
                Vector3 bodyOffset = Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).BodyOffset;
                bodyOffset.z -= 0.05f;
                Mod.GetCurGroup().HolsterRenderData.GetConfiguration(Mod.CurDir, Mod.IsSidearmMode).BodyOffset = bodyOffset;
            }
        }

        private static void DrawHeadManagement(Rect rect)
        {
            float height = 0.1f * rect.height;

            var previousButton = new Rect(rect.x, rect.y, 0.2f * rect.width, height);
            var currentDisplay = new Rect(rect.x + rect.width * 0.2f, rect.y, 0.6f * rect.width, height);
            var nextButton = new Rect(rect.x + rect.width * 0.8f, rect.y, 0.2f * rect.width, height);

            if (Widgets.ButtonText(previousButton, "<-"))
            {
                if (CurrentHeadIndex == 0)
                {
                    CurrentHeadIndex = HeadTypeDataProvider.AllHeadTypes.Count() - 1;
                }
                else
                {
                    CurrentHeadIndex--;
                }
            }

            _ = Widgets.ButtonText(currentDisplay, GetCurrentHead().DefName, true, false, false);

            if (Widgets.ButtonText(nextButton, "->"))
            {
                if (CurrentHeadIndex == HeadTypeDataProvider.AllHeadTypes.Count() - 1)
                {
                    CurrentHeadIndex = 0;
                }
                else
                {
                    CurrentHeadIndex++;
                }
            }
        }

        private static Texture ChooseBodyTexture() => Mod.CurrentBody.BodyTextures[Mod.CurDir];

        private static Texture ChooseHeadTexture()
        {
            if (CurrentHead == null)
                return HeadTypeDataProvider.AllHeadTypes.First().HeadTextures[Mod.CurDir];

            return CurrentHead.HeadTextures[Mod.CurDir];
        }

        private static Vector2 ChooseHeadOffset()
        {
            if (Mod.CurrentBody == null)
                return BodyTypeDataProvider.AllBodyTypes.First().Offset;

            return Mod.CurrentBody.Offset;
        }
    }
}
