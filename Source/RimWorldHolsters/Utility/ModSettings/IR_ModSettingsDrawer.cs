using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

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
            return mod.GetCurGroup().GetLayer(mod.curDir, mod.isSidearmMode);
        }

        private static void DrawWeapon(Rect rect)
        {
            if (mod.curWeapons.NullOrEmpty()) //||mod.GetCurGroup()sIndex == WeaponType.doNotDisplay)
            {
                return;
            }
            Vector2 offset = new Vector2(IR_HolstersSettings.GetWeaponPos(mod.GetCurGroup(), mod.curDir, mod.isSidearmMode, mod.currentBody).x, -IR_HolstersSettings.GetWeaponPos(mod.GetCurGroup(), mod.curDir, mod.isSidearmMode, mod.currentBody).z);

            Texture text = mod.GetCurWeapon().graphic.MatNorth.mainTexture;
            float scale = (((1 / mod.GetCurWeapon().uiIconScale) / (text.width / 64)) * 1.35f * mod.GetCurWeapon().graphic.drawSize.x) * mod.GetCurGroup().GetSize(mod.curDir);

            //Widgets.DrawTextureRotated(rect.center + (offset * pixelRatio), text, IR_WeaponData.GetWeaponAngle(GetCurGroup()sIndex, currentDir), scale);

            Vector2 center = rect.center + (offset * IR_HolstersMod.pixelRatio);

            float num = (float)text.width * scale;

            float num2 = (float)text.height * scale;

            if (IR_HolstersSettings.GetWeaponFlip(mod.GetCurGroup(), mod.curDir, mod.isSidearmMode))
            {
                num2 = (float)text.height * -scale;
            }

            Widgets.DrawTextureRotated(new Rect(center.x - num / 2f, center.y - num2 / 2f, num, num2), text, IR_HolstersSettings.GetWeaponAngle(mod.GetCurGroup(), mod.curDir, mod.isSidearmMode));
        }

        private static void DrawBody(Rect rect)
        {
            Rect bodyRect = new Rect(rect.x + (0.2f * rect.width), rect.y + (0.3f * rect.height), 0.6f * rect.width, 0.6f * rect.width);

            var texture = ChooseBodyTexture();

            Widgets.DrawTextureRotated(rect.center, texture, 0);
        }

        private static void DrawHead(Rect rect)
        {
            Rect headRect = new Rect(rect.x + (0.2f * rect.width), rect.y + (0.3f * rect.height), 0.6f * rect.width, 0.6f * rect.width);

            var texture = ChooseHeadTexture();

            float offset = 0;

            if (mod.curDir == Rot4.East)
            {
                offset = -ChooseHeadOffset();
            }
            else if (mod.curDir == Rot4.West)
            {
                offset = ChooseHeadOffset();
            }

            Widgets.DrawTextureRotated(rect.center - new Vector2(offset * IR_HolstersMod.pixelRatio, 34), texture, 0);
        }

        internal static void DrawButtons(Rect rect)
        {
            DrawDirectionsButtons(rect);
            DrawSizeButtons(rect);
            DrawAngleButtons(rect);
            DrawPositionButtons(rect);
            DrawPresetsButtons(rect);
            DrawLayerButtons(rect);


            Rect buttonFlip = new Rect(rect.x + (0.2f * rect.width), rect.y +(rect.height), 0.15f * rect.width, 0.1f * rect.height);

            Rect textPosition = new Rect(rect.x + (0.1f * rect.width), rect.y + (0.02f * rect.height), 0.3f * rect.width, 0.1f * rect.height);
            Rect textBodyPos = new Rect(rect.x + (0.1f * rect.width), rect.y + (0.12f * rect.height), 0.3f * rect.width, 0.1f * rect.height);
            //LAYER
           

            //FLIP
            if (Widgets.ButtonText(buttonFlip, "Flip"))
            {
                bool flip = mod.GetCurGroup().GetFlip(mod.curDir, mod.isSidearmMode);
                mod.GetCurGroup().SetFlip(mod.curDir, !flip, mod.isSidearmMode);
                return;
            }

            Vector2 pos = new Vector2(IR_HolstersSettings.GetWeaponPos(mod.GetCurGroup(), mod.curDir, mod.isSidearmMode).x, -IR_HolstersSettings.GetWeaponPos(mod.GetCurGroup(), mod.curDir, mod.isSidearmMode).z);
            //+Math.Round(IR_HolstersSettings.GetWeaponPos(mod.GetCurGroup(), mod.curDir, mod.isSidearmMode).x, 4) + ", " + Math.Round(IR_HolstersSettings.GetWeaponPos(mod.GetCurGroup(), mod.curDir, mod.isSidearmMode).z, 4)
            Widgets.Label(textPosition, "Position: " + Math.Round(pos.x, 3) + " and " + -Math.Round(pos.y, 3));

            Vector3 bodyOffset = mod.GetCurGroup().GetBodyOffset(mod.curDir, mod.isSidearmMode) * mod.GetCurGroup().GetBodyOffsetModif(mod.currentBody, mod.isSidearmMode);


            Widgets.Label(textBodyPos, "Body offset: " + Math.Round(bodyOffset.x, 3) + ", " + Math.Round(bodyOffset.z, 3));
            //Widgets.Label(textPosition, "Total: " + Math.Round(mod.GetCurGroup().GetBodyOffset(mod.curDir, mod.isSidearmMode).x, 3) + ", " + Math.Round(mod.GetCurGroup().GetBodyOffset(mod.curDir, mod.isSidearmMode).z, 3));
        }

        private static void DrawLayerButtons(Rect rect)
        {
            Rect buttonLayer = new Rect(rect.x, rect.y + (rect.height), 0.2f * rect.width, 0.1f * rect.height);

            string layerLabel;

            if (!IsWeaponInFrontLayer())
            {
                layerLabel = "back";
            }
            else
            {
                layerLabel = "front";
            }
            if (Prefs.DevMode)
            {
                Rect buttonLayerMinusOffset = new Rect(rect.x, rect.y + (rect.height), 0.025f * rect.width, 0.1f * rect.height);
                buttonLayer = new Rect(rect.x + 0.025f * rect.width, rect.y + (rect.height), 0.15f * rect.width, 0.1f * rect.height);
                Rect buttonLayerPlusOffset = new Rect(rect.x + 0.175f * rect.width, rect.y + (rect.height), 0.025f * rect.width, 0.1f * rect.height);

                if (Widgets.ButtonText(buttonLayerMinusOffset, "<"))
                {
                    if (IsWeaponInFrontLayer())
                    {
                        IR_HolstersSettings.frontLayerOffset -= 0.001f;
                    }

                    if (!IsWeaponInFrontLayer())
                    {
                        IR_HolstersSettings.backLayerOffset -= 0.001f;
                    }
                }



                if (Widgets.ButtonText(buttonLayerPlusOffset, ">"))
                {
                    if (IsWeaponInFrontLayer())
                    {
                        IR_HolstersSettings.frontLayerOffset += 0.001f;
                    }

                    if (!IsWeaponInFrontLayer())
                    {
                        IR_HolstersSettings.backLayerOffset += 0.001f;
                    }
                }
            }

            

            if (Widgets.ButtonText(buttonLayer, "Layer: " + layerLabel))
            {
                var temp = mod.GetCurGroup().GetLayer(mod.curDir, mod.isSidearmMode);
                temp = !temp;
                mod.GetCurGroup().SetLayer(mod.curDir, temp, mod.isSidearmMode);
            }
        }

        private static void DrawPresetsButtons(Rect rect)
        {
            Rect buttonPreset1 = new Rect(rect.x, rect.y - (0.1f * rect.height), rect.width, 0.1f * rect.height);
            /*
            if (Widgets.ButtonText(buttonPreset1, "PRESETS", true, true, Color.blue, true))
            {
                
            }*/
        }

        private static void DrawPositionButtons(Rect rect)
        {
            Rect buttonWest = new Rect(rect.x, rect.y + (0.45f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonEast = new Rect(rect.x + (0.9f * rect.width), rect.y + (0.45f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonNorth = new Rect(rect.x + (0.45f * rect.width), rect.y, 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonSouth = new Rect(rect.x + (0.45f * rect.width), rect.y + (0.9f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
       
            if (Widgets.ButtonText(buttonWest, "-X", true, true, Color.blue, true))
            {
                var temp = mod.GetCurGroup().GetPos(mod.curDir, mod.isSidearmMode);
                temp.x -= 0.05f;
                mod.GetCurGroup().SetPos(mod.curDir, temp, mod.isSidearmMode);
            }
            if (Widgets.ButtonText(buttonEast, "+X"))
            {
                var tempX = mod.GetCurGroup().GetPos(mod.curDir, mod.isSidearmMode);
                tempX.x += 0.05f;
                mod.GetCurGroup().SetPos(mod.curDir, tempX, mod.isSidearmMode);
            }
            if (Widgets.ButtonText(buttonNorth, "+Y"))
            {
                var tempX = mod.GetCurGroup().GetPos(mod.curDir, mod.isSidearmMode);
                tempX.z += 0.05f;
                mod.GetCurGroup().SetPos(mod.curDir, tempX, mod.isSidearmMode);
            }
            if (Widgets.ButtonText(buttonSouth, "-Y"))
            {
                var tempX = mod.GetCurGroup().GetPos(mod.curDir, mod.isSidearmMode);
                tempX.z -= 0.05f;
                mod.GetCurGroup().SetPos(mod.curDir, tempX, mod.isSidearmMode);
            }
        }

        private static void DrawDirectionsButtons(Rect rect)
        {
            Rect buttonLookWest = new Rect(rect.x + (0.1f * rect.width), rect.y + (0.45f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonLookEast = new Rect(rect.x + (0.8f * rect.width), rect.y + (0.45f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonLookNorth = new Rect(rect.x + (0.45f * rect.width), rect.y + (0.1f * rect.height), 0.1f * rect.width, 0.1f * rect.height);
            Rect buttonLookSouth = new Rect(rect.x + (0.45f * rect.width), rect.y + (0.8f * rect.height), 0.1f * rect.width, 0.1f * rect.height);

            //DIRECTION
            if (Widgets.ButtonText(buttonLookWest, "W"))
            {
                mod.curDir = Rot4.West;
            }
            if (Widgets.ButtonText(buttonLookEast, "E"))
            {
                mod.curDir = Rot4.East;
            }
            if (Widgets.ButtonText(buttonLookNorth, "N"))
            {
                mod.curDir = Rot4.North;
            }
            if (Widgets.ButtonText(buttonLookSouth, "S"))
            {
                mod.curDir = Rot4.South;
            }
        }

        internal static void DrawSizeButtons(Rect rect)
        {
            Rect buttonPlusSize = new Rect(rect.x + (0.6f * rect.width), rect.y + (1f * rect.height), 0.05f * rect.width, 0.1f * rect.height);
            Rect buttonSize = new Rect(rect.x + (0.4f * rect.width), rect.y + (1f * rect.height), 0.2f * rect.width, 0.1f * rect.height);
            Rect buttonMinusSize = new Rect(rect.x + (0.35f * rect.width), rect.y + (1f * rect.height), 0.05f * rect.width, 0.1f * rect.height);

            if (Widgets.ButtonText(buttonMinusSize, "<"))
            {
                var temp = mod.GetCurGroup().GetSize(mod.curDir);
                temp -= 0.05f;
                mod.GetCurGroup().SetSize(mod.curDir, temp);
            }

            Widgets.ButtonText(buttonSize, "Size: " + Math.Round(mod.GetCurGroup().GetSize(mod.curDir)*100, 3) + "%");

            if (Widgets.ButtonText(buttonPlusSize, ">"))
            {
                var temp = mod.GetCurGroup().GetSize(mod.curDir);
                temp += 0.05f;
                mod.GetCurGroup().SetSize(mod.curDir, temp);
            }
        }

        internal static void DrawAngleButtons(Rect rect)
        {
            Rect buttonRotateLeft = new Rect(rect.x + (0.7f * rect.width), rect.y + rect.height, 0.05f * rect.width, 0.1f * rect.height);
            Rect buttonRotateRight = new Rect(rect.x + (0.90f * rect.width), rect.y + rect.height, 0.05f * rect.width, 0.1f * rect.height);

            Rect buttonAngle = new Rect(rect.x + (0.75f * rect.width), rect.y + rect.height, 0.15f * rect.width, 0.1f * rect.height);

            Rect buttonRotateLeftPlus = new Rect(rect.x + (0.65f * rect.width), rect.y + rect.height, 0.05f * rect.width, 0.1f * rect.height);
            Rect buttonRotateRightPlus = new Rect(rect.x + (0.95f * rect.width), rect.y + rect.height, 0.05f * rect.width, 0.1f * rect.height);



            Widgets.ButtonText(buttonAngle, "Angle: " + IR_HolstersSettings.GetWeaponAngle(mod.GetCurGroup(), mod.curDir, mod.isSidearmMode) % 360 + "°");

            if (Widgets.ButtonText(buttonRotateLeftPlus, "<<"))
            {
                var tempX = mod.GetCurGroup().GetAngle(mod.curDir, mod.isSidearmMode);
                tempX -= 5;
                mod.GetCurGroup().SetAngle(mod.curDir, tempX, mod.isSidearmMode);
            }
            if (Widgets.ButtonText(buttonRotateLeft, "<"))
            {
                var tempX = mod.GetCurGroup().GetAngle(mod.curDir, mod.isSidearmMode);
                tempX -= 1;
                mod.GetCurGroup().SetAngle(mod.curDir, tempX, mod.isSidearmMode);
            }

            if (Widgets.ButtonText(buttonRotateRight, ">"))
            {
                var tempX = mod.GetCurGroup().GetAngle(mod.curDir, mod.isSidearmMode);
                tempX += 1;
                mod.GetCurGroup().SetAngle(mod.curDir, tempX, mod.isSidearmMode);
            }
            if (Widgets.ButtonText(buttonRotateRightPlus, ">>"))
            {
                var tempX = mod.GetCurGroup().GetAngle(mod.curDir, mod.isSidearmMode);
                tempX += 5;
                mod.GetCurGroup().SetAngle(mod.curDir, tempX, mod.isSidearmMode);
            }
        }

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
        }



        private static Texture ChooseBodyTexture()
        {
            return IR_Textures.bodies[mod.currentBody][mod.curDir];
        }

        

        private static Texture ChooseHeadTexture()
        {
            switch (mod.currentBody)
            {
                default:
                case BodyType.hulk:
                case BodyType.fat:
                case BodyType.male:
                    return IR_Textures.maleHead[mod.curDir];
                    break;

                case BodyType.thin:
                case BodyType.female:
                    return IR_Textures.femaleHead[mod.curDir];
                    break;
            }
        }

        private static float ChooseHeadOffset()
        {
            switch (mod.currentBody)
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
