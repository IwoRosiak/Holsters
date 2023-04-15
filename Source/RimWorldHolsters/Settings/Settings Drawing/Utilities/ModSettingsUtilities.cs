using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Holsters.Utility.ModSettings.Settings_Drawing
{
    internal static class ModSettingsUtilities
    {
        internal static float pixelRatio = 96;

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
    }
}
