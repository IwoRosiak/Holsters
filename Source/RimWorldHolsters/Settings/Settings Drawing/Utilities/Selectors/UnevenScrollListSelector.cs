using Holsters.Settings.PresetsLoading;
using Holsters.Utility.ModSettings.PresetsLoading;
using System;
using UnityEngine;
using Verse;

namespace Holsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie
{
    internal class UnevenScrollListSelector<T> : HorizontalSpreadListSelector<T> where T : IPresetable
    {
        internal UnevenScrollListSelector(float elementSize, int elementsPerRow) : base(elementSize, elementsPerRow)
        {
        }

        protected override Vector2 CalculatePosition(Rect drawRect, int positionInSelection)
        {
            throw new NotImplementedException();
        }
    }
}
