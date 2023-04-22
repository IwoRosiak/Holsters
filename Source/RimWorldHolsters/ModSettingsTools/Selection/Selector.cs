using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace ModSettingsTools.Selection
{
    public abstract class Selector<T>
    {
        protected const int buttonHeight = 40;
        protected const int buttonWidth = 180;
        protected const int mediumButtonWidth = 120;
        protected const int smallButtonWidth = 90;
        protected const int tinyButtonWidth = 45;
        protected const int weaponScale = 3;
        protected const int sliderWidth = 16;

        public Action<T> OnSelected;

        protected T _selected;

        protected readonly float _elementWidth;

        private readonly ISelectorBuilder<SelectorPair<T>> _builder;

        protected Selector(float elementSize, ISelectorBuilder<SelectorPair<T>> builder)
        {
            _builder = builder;
            _elementWidth = elementSize;
        }

        public T Selected => _selected;

        public void DrawSelection(Rect drawRect, ICollection<SelectorPair<T>> selection, string labelName)
        {
            Widgets.LabelFit(new Rect(drawRect.x, drawRect.y, drawRect.width, buttonHeight), labelName);
            Widgets.DrawLineHorizontal(drawRect.x, drawRect.y + buttonHeight * 0.8f, smallButtonWidth + tinyButtonWidth);

            DrawSelection(drawRect, selection);
        }

        public void DrawSelection(Rect drawRect, ICollection<SelectorPair<T>> selection)
        {
            Action<Rect, SelectorPair<T>> drawElement = AssembleElement;

            _builder.BuildSelector(drawRect, selection.ToList(), _elementWidth, drawElement);
        }

        protected abstract void AssembleElement(Rect rect, SelectorPair<T> selectedElement);
    }
}
