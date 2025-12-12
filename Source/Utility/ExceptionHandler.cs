using System;
using Verse;

namespace RimWorldHolsters.Core.WeaponDrawing
{
    internal static class ExceptionHandler
    {
        private const string RIMWORLD_VERSION =
#if RimWorld_1_3
            "1.3";
#endif
#if RimWorld_1_4
            "1.4";
#endif
#if RimWorld_1_5
            "1.5";
#endif
#if RimWorld_1_6
            "1.6";
#endif

        private const string REPORT_MESSAGE_PREFIX = 
            "Holsters encountered unhandled error. Please, report the following on the mods page:\n" +
            "-- RimWorld version: " + RIMWORLD_VERSION + "\n" +
            "-- Exception: \n";

        internal static void HandleException(Exception exception, int id)
        {
            Log.ErrorOnce(REPORT_MESSAGE_PREFIX + exception, id);
        }
    }
}
