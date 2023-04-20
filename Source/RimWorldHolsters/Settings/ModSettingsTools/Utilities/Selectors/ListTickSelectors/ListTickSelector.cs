using Holsters.Settings.Drawing.Utilities;
using Holsters.Utility.ModSettings.Settings_Drawing;
using Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie;
using System.Collections.Generic;
using System;
using UnityEngine;
using Verse;
using System.Linq;
using RimWorldHolsters.Settings.ModSettingsTools.Utilities.Selectors.ListTickSelectors;
using Holsters.Settings.ModSettingsTools.Utilities.Selectors.Builders;

namespace Holsters.Settings.ModSettingsTools.Utilities.Selectors.ListTickSelectors
{
    internal class ListTickSelector<T> : Selector
    {
        protected T _selected;

        protected float _elementWidth;

        private readonly VerticalBuilder<TickSelectorPair<T>> _builder = new VerticalBuilder<TickSelectorPair<T>>();

        public Action<T> OnSelected;
        public Action<T> OnDeselected;

        internal ListTickSelector(float elementSize)
        {
            _elementWidth = elementSize;
        }

        internal void DrawSelection(Rect drawRect, ICollection<TickSelectorPair<T>> selection, string labelName)
        {
            Widgets.LabelFit(new Rect(drawRect.x, drawRect.y, drawRect.width, buttonHeight), labelName);
            Widgets.DrawLineHorizontal(drawRect.x, drawRect.y + (buttonHeight * 0.8f), smallButtonWidth + tinyButtonWidth);

            DrawSelection(drawRect, selection);
        }

        internal void DrawSelection(Rect drawRect, ICollection<TickSelectorPair<T>> selection)
        {
            Action<Rect, TickSelectorPair<T>> drawElement = AssembleElement;

            _builder.BuildSelector(drawRect,selection.ToList(), _elementWidth, drawElement);
        }


        private void AssembleElement(Rect rect, TickSelectorPair<T> selectedElement)
        {
            Rect selectorPosition = new Rect(rect.position, new Vector2(rect.size.x - 24, rect.size.y));
            ModSettingsUtilities.DrawButton(selectorPosition, selectedElement.Name.ToString(), () => { });

            Vector2 posDiff = (selectorPosition.position + new Vector2(_elementWidth - 24, 0)) + new Vector2(0, 6);

            bool valueBefore = selectedElement.IsSelected;

            Rect checkboxRect = new Rect(posDiff, new Vector2(_elementWidth, buttonHeight));
            ModSettingsUtilities.DrawTick(checkboxRect, ref selectedElement.IsSelected);

            if (valueBefore != selectedElement.IsSelected)
            {
                if (selectedElement.IsSelected == false)
                {
                    OnDeselected?.Invoke(selectedElement.Selected);
                }
                else
                {
                    OnSelected?.Invoke(selectedElement.Selected);
                }
            }
        }

        internal T GetSelected()
        {
            return _selected;
        }


    }
}