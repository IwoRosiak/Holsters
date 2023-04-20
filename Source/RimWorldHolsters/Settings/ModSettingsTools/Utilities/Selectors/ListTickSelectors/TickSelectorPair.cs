using System.Collections;
using UnityEngine;

namespace RimWorldHolsters.Settings.ModSettingsTools.Utilities.Selectors.ListTickSelectors
{
    public sealed class TickSelectorPair<T>
    {
        public bool IsSelected;

        public TickSelectorPair(T selected, string name)
        {
            Selected = selected;
            Name = name;
        }

        public T Selected { get; }
        public string Name { get; }
    }
}