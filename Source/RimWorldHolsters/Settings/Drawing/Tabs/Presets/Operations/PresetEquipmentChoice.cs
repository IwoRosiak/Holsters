using Holsters.Settings;
using Holsters.Utility.ModSettings.Settings_Drawing;
using Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie;
using SettingsDrawer.Sections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Settings.Drawing.Tabs.PresetsTab
{
    public class PresetEquipmentChoice : Operation
    {
        private Vector2 _scrollVector = new Vector2();
        protected const int buttonHeight = 40;
        protected const int buttonWidth = 180;
        protected const int mediumButtonWidth = 120;
        protected const int smallButtonWidth = 90;
        protected const int tinyButtonWidth = 45;
        protected const int weaponScale = 3;
        protected const int sliderWidth = 16;

        private List<ThingDef> _defs;
        public PresetEquipmentChoice(Rect area) : base(area)
        {
            _defs = EquipmentLoader.LoadEquipment();
        }

        public override void ExecuteOperation()
        {
            Rect viewRect = GetViewRectSize(area);
            Rect scrollRect = GetScrollRectSize(area, viewRect);


            Widgets.BeginScrollView(scrollRect, ref _scrollVector, viewRect);

            foreach (ThingDef selectionElement in _defs)
            {
                int positionInSelection = _defs.IndexOf(selectionElement);
                Rect selectorPosition = new Rect(CalculatePosition(viewRect, positionInSelection), new Vector2(3, buttonHeight));

                ModSettingsUtilities.DrawButton(selectorPosition, selectionElement.defName.ToString(), () =>
                {
                    SelectedEquipmentManager.CurrentlySelected = selectionElement;
                });
            }
            Widgets.EndScrollView();
        }

        protected virtual Rect GetViewRectSize(Rect drawRect)
        {
            return new Rect(drawRect.x, drawRect.y, smallButtonWidth + tinyButtonWidth * 2, buttonHeight * _defs.Count);
        }

        protected virtual Rect GetScrollRectSize(Rect drawRect, Rect viewRect)
        {
            Rect scrollRect = viewRect;
            scrollRect.width += sliderWidth;
            scrollRect.height = drawRect.height - buttonHeight;

            return scrollRect;
        }

        protected virtual Vector2 CalculatePosition(Rect drawRect, int positionInSelection)
        {
            Vector2 position = drawRect.position;
            position.y += positionInSelection * buttonHeight;

            return position;
        }
    }
}