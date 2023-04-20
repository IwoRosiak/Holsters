using Holsters.Settings.Drawing.Utilities;
using Holsters.Utility.ModSettings.Settings_Drawing;
using Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie;
using System.Collections.Generic;
using System;
using UnityEngine;
using Verse;
using System.Linq;
using RimWorldHolsters.Settings.ModSettingsTools.Utilities.Selectors.ListTickSelectors;

namespace Holsters.Settings.ModSettingsTools.Utilities.Selectors.ListTickSelectors
{
    internal class ListTickSelector<T> : Selector
    {
        private Vector2 _scrollVector = new Vector2();

        protected T _selected;

        protected float _elementWidth;

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
            Rect viewRect = GetViewRectSize(drawRect, selection);
            Rect scrollRect = GetScrollRectSize(drawRect, viewRect);


            Widgets.BeginScrollView(scrollRect, ref _scrollVector, viewRect);

            foreach (TickSelectorPair<T> selectionElement in selection)
            {
                int positionInSelection = selection.ToList().IndexOf(selectionElement);

                Vector2 position = CalculatePosition(viewRect, positionInSelection);
                Rect selectorPosition = new Rect(position, new Vector2(_elementWidth - 24, buttonHeight));
                ModSettingsUtilities.DrawButton(selectorPosition, selectionElement.Name.ToString(), () => { });

                Vector2 posDiff = (selectorPosition.position + new Vector2(_elementWidth - 24, 0)) + new Vector2(0, 6);

                bool valueBefore = selectionElement.IsSelected;

                Rect checkboxRect = new Rect(posDiff, new Vector2(_elementWidth, buttonHeight));
                ModSettingsUtilities.DrawTick(checkboxRect, ref selectionElement.IsSelected);

                if (valueBefore != selectionElement.IsSelected)
                {
                    if (selectionElement.IsSelected == false)
                    {
                        OnDeselected?.Invoke(selectionElement.Selected);
                    }
                    else
                    {
                        OnSelected?.Invoke(selectionElement.Selected);
                    }
                } 
            }
            Widgets.EndScrollView();
        }

        internal T GetSelected()
        {
            return _selected;
        }

        protected virtual Rect GetViewRectSize(Rect drawRect, ICollection<TickSelectorPair<T>> selection)
        {
            return new Rect(drawRect.x, drawRect.y, smallButtonWidth + tinyButtonWidth * 2, buttonHeight * selection.Count);
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