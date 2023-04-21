using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Holsters.Settings.ModSettingsTools.Utilities.Selectors.Builders
{
    public sealed class HorizontalBuilder<T> : Builder, ISelectorBuilder<T>
    {
        private Vector2 _scrollVector = new Vector2();

        private float _elementWidth;

        private readonly int _elementsPerRow;

        public HorizontalBuilder(int elementsPerRow)
        {
            _elementsPerRow = elementsPerRow;
        }

        public void BuildSelector(Rect size, List<T> selection, float sizeE, Action<Rect, T> onBuilt)
        {
            _elementWidth = sizeE;

            Rect viewRect = GetViewRectSize(size, selection);
            Rect scrollRect = GetScrollRectSize(size, viewRect);

            Widgets.BeginScrollView(scrollRect, ref _scrollVector, viewRect);

            foreach (T selectionElement in selection)
            {
                int positionInSelection = selection.ToList().IndexOf(selectionElement);


                Vector2 position = CalculatePosition(viewRect, positionInSelection);
                Rect selectorPosition = new Rect(position, new Vector2(_elementWidth, buttonHeight));

                onBuilt?.Invoke(selectorPosition, selectionElement);
            }

            Widgets.EndScrollView();
        }

        private Rect GetViewRectSize(Rect drawRect, List<T> selection)
        {
            _elementWidth = drawRect.width / _elementsPerRow;

            int timesButtonCanFit = Mathf.FloorToInt(drawRect.width / _elementWidth);
            int selectionCountModulo = Mathf.CeilToInt(selection.Count / timesButtonCanFit);

            Rect viewRect = new Rect(drawRect.x, drawRect.y, timesButtonCanFit * _elementWidth, buttonHeight * selectionCountModulo);

            return viewRect;
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
            int timesButtonCanFit = Mathf.FloorToInt(drawRect.width / _elementWidth);

            int column = positionInSelection % timesButtonCanFit;
            int row = Mathf.FloorToInt(positionInSelection / timesButtonCanFit);

            Vector2 position = drawRect.position;
            position.x += column * _elementWidth;
            position.y += row * buttonHeight;

            return position;
        }
    }
}