using Holsters.Settings.ModSettingsTools.Utilities.Selectors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Settings.ModSettingsTools.Utilities.Selectors.Builders
{
    public sealed class HorizontalSpreadBuilder<T> : ISelectorBuilder<T>
    {
        protected const int buttonHeight = 40;
        protected const int buttonWidth = 180;
        protected const int mediumButtonWidth = 120;
        protected const int smallButtonWidth = 90;
        protected const int tinyButtonWidth = 45;
        protected const int weaponScale = 3;
        protected const int sliderWidth = 16;

        private Vector2 _scrollVector = new Vector2();

        private float _elementWidth;

        private int _elementsPerRow;

        public HorizontalSpreadBuilder( int elementsPerRow)
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
                Rect selectorPosition = new Rect(position, new Vector2(sizeE, buttonHeight));

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