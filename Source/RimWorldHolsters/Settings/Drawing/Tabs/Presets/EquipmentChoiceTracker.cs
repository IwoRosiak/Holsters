using Holsters.Settings;
using UnityEditor;
using UnityEngine;
using Verse;

namespace Holsters.Settings.Drawing.Tabs.Presets
{
    internal static class EquipmentChoiceTracker
    {
        public static ThingDef CurrentEquipment { get; private set; }

        public static void UpdateChoice(ThingDef equipment)
        {
            CurrentEquipment = equipment;
        }
    }
    
}