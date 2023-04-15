using Holsters.Settings;
using Holsters.Utility.ModSettings.PresetsLoading;
using Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie.Operations;
using UnityEngine;

namespace Holsters.Utility.ModSettings.Settings_Drawing.TableDrawer
{
    internal class EditTable : Operation
    {
        private RotationButtons _buttons;
        private PositionButtons _positionButtons;


        private IPresetable _currentlySelected;

        public EditTable(Rect area) : base(area)
        {
        }

        public override void Initialise()
        {
            base.Initialise();

            if (_buttons == null)
            {
                _buttons = new RotationButtons(new Rect(0, 0, 10, 10));
                _positionButtons = new PositionButtons(new Rect(0, 0, 10, 10));
            }

        }

        public override void ExecuteOperation()
        {
            Section section = new Section(area, 10, 10);

            section.AddOperation(new Image(new Rect(0, 0, 10, 10), IR_Textures.backgroundPawn));
            section.AddOperation(_buttons);
            section.AddOperation(new PawnDrawer(new Rect(0, 0, 10, 10), _buttons.Rotation, BodyType.male));


            HandlePositionButtons();
            section.AddOperation(_positionButtons);

            section.AddOperation(new WeaponDrawer(new Rect(0, 0, 10, 10), _buttons.Rotation, TEMPDEFOF.Gun_BoltActionRifle, _currentlySelected.Preset));

            section.DrawOperations();
        }

        public void UpdateSelection(IPresetable currentlySelected)
        {
            _currentlySelected = currentlySelected;
        }

        private void HandlePositionButtons()
        {
            _currentlySelected.ModifyProperty((holster => holster.Position += new Vector3(_positionButtons.ApplyProperty().x, 0, _positionButtons.ApplyProperty().y)), _buttons.Rotation);
        }

        /*
        internal static bool IsWeaponInFrontLayer()
        {
            return mod.GetCurGroup().GetLayer(mod.curDir, mod.isSidearmMode);
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





        




    */
    }
}
