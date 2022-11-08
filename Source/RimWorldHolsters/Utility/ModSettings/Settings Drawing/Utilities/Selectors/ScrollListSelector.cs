using RimWorldHolsters.Utility.ModSettings.PresetsLoading;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie
{
    internal class ScrollListSelector<T> : Selector where T : IPresetable
    {
        private Vector2 _scrollVector = new Vector2();

        private T _selected;

        protected float _elementWidth;

        internal ScrollListSelector(T defaultSelection, float elementSize)
        {
            _selected = defaultSelection;
            _elementWidth = elementSize;
        }

        internal ScrollListSelector(float elementSize)
        {
            _elementWidth = elementSize;
        }

        internal void DrawSelection(Rect drawRect, ICollection<T> selection, string labelName) 
        {
            Widgets.LabelFit(new Rect(drawRect.x, drawRect.y, drawRect.width, buttonHeight), labelName);
            Widgets.DrawLineHorizontal(drawRect.x, drawRect.y + (buttonHeight * 0.8f), smallButtonWidth + tinyButtonWidth);

            DrawSelection(drawRect, selection);
        }

        internal void DrawSelection(Rect drawRect, ICollection<T> selection)
        {
            Rect viewRect = GetViewRectSize(drawRect, selection);
            Rect scrollRect = GetScrollRectSize(drawRect, viewRect);

            Widgets.BeginScrollView(scrollRect, ref _scrollVector, viewRect);

            foreach (T selectionElement in selection)
            {
                int positionInSelection = selection.ToList().IndexOf(selectionElement);
                Rect selectorPosition = new Rect(CalculatePosition(viewRect, positionInSelection), new Vector2(_elementWidth, buttonHeight));

                ModSettingsUtilities.DrawButton(selectorPosition, selectionElement.Name.ToString(), () =>
                {
                    _selected = selectionElement;
                });
            }
            Widgets.EndScrollView();

        }

        internal T GetSelected()
        {
            return _selected;
        }

        protected virtual Rect GetViewRectSize(Rect drawRect, ICollection<T> selection)
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
