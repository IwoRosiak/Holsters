using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters
{
    public class IR_HolstersSettings : ModSettings
    {
        public IR_HolstersSettings()
        {
        }

        public static Dictionary<WeaponType, WeaponPos> WeaponDataSettings;

        public static Dictionary<string, WeaponType> WeaponSpecialType;

        public static WeaponPos longRangedSettings;
        public static WeaponPos shortRangedSettings;
        public static WeaponPos longMeleeSettings;
        public static WeaponPos shortMeleeSettings;
        public static WeaponPos bowSettings;
        public static WeaponPos grenadesSettings;
        public static WeaponPos custom1Settings;
        public static WeaponPos custom2Settings;
        public static WeaponPos custom3Settings;
        public static WeaponPos doNotDisplaySettings;

        public override void ExposeData()
        {
            Scribe_Collections.Look(ref WeaponDataSettings, "WeaponDataSettings", LookMode.Value, LookMode.Deep);
            Scribe_Collections.Look(ref WeaponSpecialType, "WeaponSpecialType", LookMode.Value, LookMode.Value);

            base.ExposeData();
        }

        public static void InitSpecificSetting(WeaponType type, WeaponPos settings)
        {
            settings.pos = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, Vector3.zero},
                {Rot4.North, Vector3.zero},
                {Rot4.East, Vector3.zero},
                {Rot4.West, Vector3.zero}
            };

            settings.flip = new Dictionary<Rot4, bool>()
            {
                {Rot4.South, false},
                {Rot4.North, false},
                {Rot4.East, false},
                {Rot4.West, false}
            };

            settings.angle = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 0},
                {Rot4.North, 0},
                {Rot4.East, 0},
                {Rot4.West, 0}
            };

            IR_HolstersSettings.WeaponDataSettings.Remove(type);

            IR_HolstersSettings.WeaponDataSettings.Add(type, settings);
        }

        public static void InitSpecificSideSetting(WeaponType type, WeaponPos settings)
        {
            settings.posSide = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, Vector3.zero},
                {Rot4.North, Vector3.zero},
                {Rot4.East, Vector3.zero},
                {Rot4.West, Vector3.zero}
            };

            settings.flipSide = new Dictionary<Rot4, bool>()
            {
                {Rot4.South, false},
                {Rot4.North, false},
                {Rot4.East, false},
                {Rot4.West, false}
            };

            settings.angleSide = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 0},
                {Rot4.North, 0},
                {Rot4.East, 0},
                {Rot4.West, 0}
            };

            IR_HolstersSettings.WeaponDataSettings.Remove(type);

            IR_HolstersSettings.WeaponDataSettings.Add(type, settings);
        }

        public static void InitWeaponDataSettings()
        {
            IR_HolstersSettings.WeaponDataSettings = new Dictionary<WeaponType, WeaponPos>() {
                {WeaponType.longRanged, longRangedSettings},
                {WeaponType.shortRanged, shortRangedSettings},
                {WeaponType.longMelee, longMeleeSettings},
                {WeaponType.shortMelee, shortMeleeSettings},
                {WeaponType.bow, bowSettings},
                {WeaponType.grenades, grenadesSettings},
                {WeaponType.custom1,  custom1Settings},
                {WeaponType.custom2,  custom2Settings},
                {WeaponType.custom3,  custom3Settings},
                {WeaponType.doNotDisplay,  doNotDisplaySettings}
            };
        }
    }
}