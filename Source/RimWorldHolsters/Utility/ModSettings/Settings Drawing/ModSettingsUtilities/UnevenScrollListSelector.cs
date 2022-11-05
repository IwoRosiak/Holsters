using System;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Utility.ModSettings.Settings_Drawing.ModSettingsUtilitie
{
    internal class UnevenScrollListSelector<T> : HorizontalSpreadListSelector<T> where T : Def
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
