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

        public static WeaponPos longRangedSettings;
        public static WeaponPos shortRangedSettings;
        public static WeaponPos longMeleeSettings;
        public static WeaponPos shortMeleeSettings;
        public static WeaponPos bowSettings;

        public override void ExposeData()
        {
            Scribe_Collections.Look(ref WeaponDataSettings, "WeaponDataSettings", LookMode.Value, LookMode.Deep);

            base.ExposeData();
        }

        public static void InitWeaponDataSettings()
        {
            //RANGED
            IR_HolstersSettings.longRangedSettings.pos = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, Vector3.zero },
                {Rot4.North, Vector3.zero },
                {Rot4.East, Vector3.zero },
                {Rot4.West, Vector3.zero }
            };

            IR_HolstersSettings.longRangedSettings.angle = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 0},
                {Rot4.North, 0},
                {Rot4.East, 0},
                {Rot4.West, 0}
            };

            IR_HolstersSettings.shortRangedSettings.pos = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, Vector3.zero },
                {Rot4.North, Vector3.zero },
                {Rot4.East, Vector3.zero },
                {Rot4.West, Vector3.zero}
            };

            IR_HolstersSettings.shortRangedSettings.angle = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 0 },
                {Rot4.North, 0 },
                {Rot4.East, 0 },
                {Rot4.West, 0 }
            };

            //MELEE

            IR_HolstersSettings.longMeleeSettings.pos = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South,Vector3.zero },
                {Rot4.North, Vector3.zero },
                {Rot4.East,Vector3.zero },
                {Rot4.West, Vector3.zero }
            };

            IR_HolstersSettings.longMeleeSettings.angle = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 0},
                {Rot4.North, 0},
                {Rot4.East, 0},
                {Rot4.West, 0}
            };

            IR_HolstersSettings.shortMeleeSettings.pos = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South,  Vector3.zero },
                {Rot4.North, Vector3.zero },
                {Rot4.East, Vector3.zero},
                {Rot4.West,  Vector3.zero}
            };

            IR_HolstersSettings.shortMeleeSettings.angle = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 0},
                {Rot4.North, 0},
                {Rot4.East, 0},
                {Rot4.West, 0}
            };

            //OTHER
            IR_HolstersSettings.bowSettings.pos = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, Vector3.zero},
                {Rot4.North, Vector3.zero},
                {Rot4.East, Vector3.zero},
                {Rot4.West, Vector3.zero}
            };

            IR_HolstersSettings.bowSettings.angle = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 0},
                {Rot4.North, 0},
                {Rot4.East, 0},
                {Rot4.West, 0}
            };

            IR_HolstersSettings.WeaponDataSettings = new Dictionary<WeaponType, WeaponPos>() {
                {WeaponType.longRanged, longRangedSettings},
                {WeaponType.shortRanged, shortRangedSettings},
                {WeaponType.longMelee, longMeleeSettings},
                {WeaponType.shortMelee, shortMeleeSettings},
                {WeaponType.bow, bowSettings}
            };
        }
    }
}