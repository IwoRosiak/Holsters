using Holsters.Settings;
using UnityEditor;
using UnityEngine;
using Verse;

namespace Holsters.Settings.Drawing.Tabs.Presets
{
    internal static class EquipmentChoiceTracker
    {
        private static ThingDef _currentEquipment;

        public static ThingDef CurrentEquipment 
        { 
            get 
            {
                if (_currentEquipment == null)
                    return null;

                return _currentEquipment;
            }
            
            private set => _currentEquipment = value; 
        }

        public static void UpdateChoice(ThingDef equipment)
        {
            _currentEquipment = equipment;
        }
    }
    
}