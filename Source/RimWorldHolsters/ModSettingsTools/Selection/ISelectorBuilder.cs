using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModSettingsTools.Selection
{
    public interface ISelectorBuilder<T>
    {
        void BuildSelector(Rect size, List<T> selection, float sizeE, Action<Rect, T> onBuilt);
    }
}