using System;
using UnityEngine;

namespace ModSettingsTools.Selection.Selectors
{
    internal sealed class CheckboxSelector<T> : Selector<T>
    {
        public Action<T> OnSelected;
        public Action<T> OnDeselected;

        public CheckboxSelector(float elementSize, ISelectorBuilder<SelectorPair<T>> builder) : base(elementSize, builder)
        {
        }

        protected override void AssembleElement(Rect rect, SelectorPair<T> selectedElement)
        {
            Rect selectorPosition = new Rect(rect.position, new Vector2(rect.size.x - 24, rect.size.y));
            ModSettingsUtilities.DrawButton(selectorPosition, selectedElement.Name.ToString(), () => { });

            Vector2 posDiff = selectorPosition.position + new Vector2(_elementWidth - 24, 0) + new Vector2(0, 6);

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
    }
}