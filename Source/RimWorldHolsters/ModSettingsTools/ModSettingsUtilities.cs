using System;
using UnityEngine;
using Verse;

namespace ModSettingsTools
{
    internal static class ModSettingsUtilities
    {
        internal static readonly float PixelRatio = 96;

        public static void DrawLine(ref Rect rect, string lineName)
        {
            Rect gapLine = rect;
            gapLine.height = 30;

            Widgets.Label(gapLine, lineName);
            Widgets.DrawLineHorizontal(gapLine.x, gapLine.y + 24f, gapLine.width);

            rect.y += 30;
        }

        public static void DrawButton(Rect rect, string buttonText, Action action)
        {
            if (Widgets.ButtonText(rect, buttonText))
            {
                action.Invoke();
            }
        }

        public static void DrawTick(Rect rect, ref bool onTick)
        {
            Widgets.Checkbox(new Vector2(rect.x, rect.y), ref onTick);
        }
    }
}
