using Holsters.Settings;
using Holsters.Settings.Drawing.Utilities;
using Holsters.Settings.ModSettingsTools.Utilities.Selectors;
using Holsters.Settings.ModSettingsTools.Utilities.Selectors.Builders;
using Holsters.Settings.ModSettingsTools.Utilities.Selectors.ListTickSelectors;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie
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
