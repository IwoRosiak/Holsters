using System;
using UnityEngine;

namespace ModSettingsTools.Selection.Selectors
{
    internal sealed class ClickSelector<T> : Selector<T>
    {
        public Action<T> OnSelected;

        public ClickSelector(float elementSize, ISelectorBuilder<SelectorPair<T>> builder) : base(elementSize, builder)
        {
        }

        public ClickSelector(float elementSize, ISelectorBuilder<SelectorPair<T>> builder, T defaultSelection) : this(elementSize, builder)
        {
            _selected = defaultSelection;
        }

        protected override void AssembleElement(Rect rect, SelectorPair<T> selectedElement)
        {
            ModSettingsUtilities.DrawButton(rect, selectedElement.Name.ToString(), () =>
            {
                _selected = selectedElement.Selected;
                OnSelected?.Invoke(_selected);
            });
        }
    }
}
