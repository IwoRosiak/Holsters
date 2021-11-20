using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters
{
    public struct WeaponPos
    {
        public Dictionary<Rot4, Vector3> pos;
        public Dictionary<Rot4, float> angle;

        public Dictionary<Rot4, Vector3> posSettings;
        public Dictionary<Rot4, float> angleSettings;
    }

    [StaticConstructorOnStartup]
    public static class IR_WeaponTypePositions
    {
        static IR_WeaponTypePositions()
        {
            LoadWeaponData();
        }

        public static Vector3 GetWeaponPos(WeaponType type, Rot4 rot)
        {
            return weaponData[type].pos[rot];
        }

        public static float GetWeaponAngle(WeaponType type, Rot4 rot)
        {
            return weaponData[type].angle[rot];
        }

        public static Dictionary<WeaponType, WeaponPos> weaponData;

        private static void LoadWeaponData()
        {
            //RANGED
            longRanged.pos = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(-0.1f, backPos, 0.1f) },
                {Rot4.North, new Vector3(0f, forwardPos, 0f) },
                {Rot4.East, new Vector3(-0.2f, forwardPos, 0f) },
                {Rot4.West, new Vector3(0.2f, backPos, 0f) }
            };

            longRanged.angle = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 255f},
                {Rot4.North, 310f},
                {Rot4.East, 295f},
                {Rot4.West, 255f}
            };

            shortRanged.pos = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(0.15f, forwardPos, -0.3f) },
                {Rot4.North, new Vector3(-0.15f,backPos, -0.3f) },
                {Rot4.East, new Vector3(0.10f, backPos, -0.3f) },
                {Rot4.West, new Vector3(-0.10f, forwardPos, -0.3f)}
            };

            shortRanged.angle = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 40f },
                {Rot4.North, -320f },
                {Rot4.East, -320f },
                {Rot4.West, 35f }
            };

            //MELEE

            longMelee.pos = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(-0.1f, backPos, 0.1f) },
                {Rot4.North, new Vector3(0f, forwardPos, 0.1f) },
                {Rot4.East, new Vector3(-0.2f, forwardPos, 0f) },
                {Rot4.West, new Vector3(0.2f, backPos, 0f) }
            };

            longMelee.angle = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 65f},
                {Rot4.North, 120f},
                {Rot4.East, 115f},
                {Rot4.West, 70f}
            };

            shortMelee.pos = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South,  new Vector3(0.1f, forwardPos*4, -0.3f) },
                {Rot4.North, new Vector3(-0.1f, backPos, -0.3f) },
                {Rot4.East, new Vector3(0.15f, backPos, -0.5f) },
                {Rot4.West,  new Vector3(-0.15f, forwardPos*4, -0.5f) }
            };

            shortMelee.angle = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 70f},
                {Rot4.North, 110f},
                {Rot4.East, 135f},
                {Rot4.West, 50f}
            };

            //OTHER
            bow.pos = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(-0.1f, backPos, 0.1f) },
                {Rot4.North, new Vector3(0f, forwardPos, 0f) },
                {Rot4.East, new Vector3(-0.2f, forwardPos, 0f) },
                {Rot4.West, new Vector3(0.2f,backPos , 0f) }
            };

            bow.angle = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 135},
                {Rot4.North, 35f},
                {Rot4.East, -20f},
                {Rot4.West, 340f}
            };

            weaponData = new Dictionary<WeaponType, WeaponPos>() {
                {WeaponType.longRanged, longRanged},
                {WeaponType.shortRanged, shortRanged},
                {WeaponType.longMelee, longMelee},
                {WeaponType.shortMelee, shortMelee},
                {WeaponType.bow, bow}
            };
        }

        private const float forwardPos = 0f;

        private const float backPos = -0.0028957527f;

        public static WeaponPos longRanged = new WeaponPos();
        public static WeaponPos shortRanged = new WeaponPos();
        public static WeaponPos longMelee = new WeaponPos();
        public static WeaponPos shortMelee = new WeaponPos();
        public static WeaponPos bow = new WeaponPos();
    }
}