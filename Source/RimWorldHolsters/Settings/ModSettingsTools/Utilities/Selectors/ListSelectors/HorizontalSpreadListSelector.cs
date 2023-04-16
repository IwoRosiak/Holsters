using Holsters.Settings;
using Holsters.Settings.Drawing.Utilities;
using Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie
{
    internal class HorizontalSpreadListSelector<T> : ScrollListSelector<T> 
    {
        private readonly int _elementsPerRow;

        internal HorizontalSpreadListSelector(T defaultSelection, int elementsPerRow, float elementSize) : base(defaultSelection, elementSize)
        {
            _elementsPerRow = (int)elementSize;
            _selected = defaultSelection;
            _elementsPerRow = elementsPerRow;
        }

        internal HorizontalSpreadListSelector(float elementSize, int elementsPerRow) : base(elementSize)
        {
            _elementsPerRow = elementsPerRow;

        }

        protected override Rect GetViewRectSize(Rect drawRect, ICollection<SelectorPair<T>> selection)
        {
            _elementWidth = drawRect.width / _elementsPerRow;

            int timesButtonCanFit = Mathf.FloorToInt(drawRect.width / _elementWidth);
            int selectionCountModulo = Mathf.CeilToInt(selection.Count/ timesButtonCanFit);

            Rect viewRect = new Rect(drawRect.x, drawRect.y, timesButtonCanFit * _elementWidth, buttonHeight * selectionCountModulo);

            return viewRect;
        }

        protected override Rect GetScrollRectSize(Rect drawRect, Rect viewRect)
        {
            Rect scrollRect = viewRect;
            scrollRect.width += sliderWidth;
            scrollRect.height = drawRect.height - buttonHeight;

            return scrollRect;
        }

        protected override Vector2 CalculatePosition(Rect drawRect, int positionInSelection)
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
