using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Holsters.Settings.ModSettingsTools.Utilities.Selectors.Builders
{
    public sealed class VerticalBuilder<T> : Builder, ISelectorBuilder<T>
    {
        private Vector2 _scrollVector = new Vector2();

        public void BuildSelector(Rect size, List<T> selection, float sizeE, Action<Rect, T> onBuilt)
        {
            Rect viewRect = GetViewRectSize(size, selection);
            Rect scrollRect = GetScrollRectSize(size, viewRect);

            Widgets.BeginScrollView(scrollRect, ref _scrollVector, viewRect);

            foreach (T selectionElement in selection)
            {
                int positionInSelection = selection.ToList().IndexOf(selectionElement);


                Vector2 position = CalculatePosition(viewRect, positionInSelection);
                Rect selectorPosition = new Rect(position, new Vector2(sizeE, buttonHeight));

                onBuilt?.Invoke(selectorPosition, selectionElement);
            }

            Widgets.EndScrollView();
        }

        private Rect GetViewRectSize(Rect drawRect, List<T> selection)
        {
            return new Rect(drawRect.x, drawRect.y, smallButtonWidth + tinyButtonWidth * 2, buttonHeight * selection.Count);
        }

        private Rect GetScrollRectSize(Rect drawRect, Rect viewRect)
        {
            Rect scrollRect = viewRect;
            scrollRect.width += sliderWidth;
            scrollRect.height = drawRect.height - buttonHeight;

            return scrollRect;
        }

        private Vector2 CalculatePosition(Rect drawRect, int positionInSelection)
        {
            Vector2 position = drawRect.position;
            position.y += positionInSelection * buttonHeight;

            return position;
        }
    }
}