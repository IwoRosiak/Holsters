﻿using Holsters.Settings;
using Holsters.Settings.Drawing.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie
{
    internal class ScrollListSelector<T> : Selector
    {
        private Vector2 _scrollVector = new Vector2();

        protected T _selected;

        protected float _elementWidth;

        public Action<T> OnSelected;

        internal ScrollListSelector(T defaultSelection, float elementSize)
        {
            _selected = defaultSelection;
            _elementWidth = elementSize;
        }

        internal ScrollListSelector(float elementSize)
        {
            _elementWidth = elementSize;
        }

        internal void DrawSelection(Rect drawRect, ICollection<SelectorPair<T>> selection, string labelName) 
        {
            Widgets.LabelFit(new Rect(drawRect.x, drawRect.y, drawRect.width, buttonHeight), labelName);
            Widgets.DrawLineHorizontal(drawRect.x, drawRect.y + (buttonHeight * 0.8f), smallButtonWidth + tinyButtonWidth);

            DrawSelection(drawRect, selection);
        }

        internal void DrawSelection(Rect drawRect, ICollection<SelectorPair<T>> selection)
        {
            Rect viewRect = GetViewRectSize(drawRect, selection);
            Rect scrollRect = GetScrollRectSize(drawRect, viewRect);
            
  
            Widgets.BeginScrollView(scrollRect, ref _scrollVector, viewRect);
            
            foreach (SelectorPair<T> selectionElement in selection)
            {
                int positionInSelection = selection.ToList().IndexOf(selectionElement);
                Rect selectorPosition = new Rect(CalculatePosition(viewRect, positionInSelection), new Vector2(_elementWidth, buttonHeight));

                ModSettingsUtilities.DrawButton(selectorPosition, selectionElement.Name.ToString(), () =>
                {
                    _selected = selectionElement.Selected;
                    OnSelected?.Invoke(_selected);
                });
            }
            Widgets.EndScrollView();
            
             


        }

        internal T GetSelected()
        {
            return _selected;
        }

        protected virtual Rect GetViewRectSize(Rect drawRect, ICollection<SelectorPair<T>> selection)
        {
            return new Rect(drawRect.x, drawRect.y, smallButtonWidth + tinyButtonWidth * 2, buttonHeight * selection.Count);
        }

        protected virtual Rect GetScrollRectSize(Rect drawRect,Rect viewRect)
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