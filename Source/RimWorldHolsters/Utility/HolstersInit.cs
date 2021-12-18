using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters
{
    public struct WeaponPos : IExposable
    {
        public Dictionary<Rot4, Vector3> pos;
        public Dictionary<Rot4, float> angle;
        public Dictionary<Rot4, bool> flip;

        public Dictionary<Rot4, Vector3> posSide;
        public Dictionary<Rot4, float> angleSide;
        public Dictionary<Rot4, bool> flipSide;

        public Vector3 GetPos(Rot4 rot, bool isSidearm)
        {
            if (isSidearm)
            {
                if (!posSide.NullOrEmpty() && posSide?.ContainsKey(rot) == true)
                {
                    return posSide[rot];
                }
            }
            else if (!pos.NullOrEmpty() && pos?.ContainsKey(rot) == true)
            {
                return pos[rot];
            }

            return Vector3.zero;
        }

        public float GetAngle(Rot4 rot, bool isSidearm)
        {
            if (isSidearm)
            {
                if (!angleSide.NullOrEmpty() && angleSide?.ContainsKey(rot) == true)
                {
                    return angleSide[rot];
                }
            }
            else if (!angle.NullOrEmpty() && angle?.ContainsKey(rot) == true)
            {
                return angle[rot];
            }

            return 0;
        }

        public bool GetFlip(Rot4 rot, bool isSidearm)
        {
            if (isSidearm)
            {
                if (!flipSide.NullOrEmpty() && flipSide?.ContainsKey(rot) == true)
                {
                    return flipSide[rot];
                }
            }
            else
            {
                if (!flip.NullOrEmpty() && flip?.ContainsKey(rot) == true)
                {
                    return flip[rot];
                }
            }

            return false;
        }

        public void SetPos(Rot4 rot, Vector3 val, bool isSidearm)
        {
            if (isSidearm)
            {
                posSide[rot] = val;
            }
            else
            {
                pos[rot] = val;
            }
        }

        public void SetAngle(Rot4 rot, float angle, bool isSidearm)
        {
            if (isSidearm)
            {
                angleSide[rot] = angle;
            }
            else
            {
                this.angle[rot] = angle;
            }
        }

        public void SetFlip(Rot4 rot, bool flip, bool isSidearm)
        {
            if (isSidearm)
            {
                flipSide[rot] = flip;
            }
            else
            {
                this.flip[rot] = flip;
            }
        }

        public void ExposeData()
        {
            Scribe_Collections.Look(ref pos, "pos", LookMode.Value, LookMode.Value);
            Scribe_Collections.Look(ref angle, "angle", LookMode.Value, LookMode.Value);
            Scribe_Collections.Look(ref flip, "flip", LookMode.Value, LookMode.Value);

            Scribe_Collections.Look(ref posSide, "posSide", LookMode.Value, LookMode.Value);
            Scribe_Collections.Look(ref angleSide, "angleSide", LookMode.Value, LookMode.Value);
            Scribe_Collections.Look(ref flipSide, "flipSide", LookMode.Value, LookMode.Value);
        }
    }

    [StaticConstructorOnStartup]
    public static class IR_HolstersInit
    {
        static IR_HolstersInit()
        {
            LoadWeaponData();
            LoadSideWeaponData();
        }

        private static void LoadWeaponData()
        {
            //RANGED
            IR_WeaponData.longRanged.pos = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(-0.1f, backPos, 0.1f) },
                {Rot4.North, new Vector3(0f, forwardPos, 0f) },
                {Rot4.East, new Vector3(-0.2f, forwardPos, 0f) },
                {Rot4.West, new Vector3(0.2f, backPos, 0f) }
            };

            IR_WeaponData.longRanged.angle = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 255f},
                {Rot4.North, 310f},
                {Rot4.East, 295f},
                {Rot4.West, 255f}
            };

            IR_WeaponData.shortRanged.pos = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(0.15f, forwardPos, -0.25f) },
                {Rot4.North, new Vector3(-0.15f,backPos, -0.25f) },
                {Rot4.East, new Vector3(0.10f, backPos, -0.25f) },
                {Rot4.West, new Vector3(-0.05f, forwardPos, -0.25f)}
            };

            IR_WeaponData.shortRanged.angle = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 40f },
                {Rot4.North, 320f },
                {Rot4.East, 320f },
                {Rot4.West, 35f }
            };

            //MELEE

            IR_WeaponData.longMelee.pos = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(-0.05f, backPos, 0.05f) },
                {Rot4.North, new Vector3(0.05f, forwardPos, -0.05f) },
                {Rot4.East, new Vector3(-0.25f, forwardPos, 0f) },
                {Rot4.West, new Vector3(0.2f, backPos, 0f) }
            };

            IR_WeaponData.longMelee.angle = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 55f},
                {Rot4.North, 120f},
                {Rot4.East, 110f},
                {Rot4.West, 75f}
            };

            IR_WeaponData.shortMelee.pos = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South,  new Vector3(0.15f, forwardPos*2, -0.2f) },
                {Rot4.North, new Vector3(-0.1f, backPos, -0.3f) },
                {Rot4.East, new Vector3(0.05f, backPos, -0.2f) },
                {Rot4.West,  new Vector3(-0.05f, forwardPos*2, -0.2f) }
            };

            IR_WeaponData.shortMelee.angle = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 60f},
                {Rot4.North, 110f},
                {Rot4.East, 135f},
                {Rot4.West, 50f}
            };

            //OTHER
            IR_WeaponData.bow.pos = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(-0.1f, backPos, 0.1f) },
                {Rot4.North, new Vector3(0f, forwardPos, 0f) },
                {Rot4.East, new Vector3(-0.2f, forwardPos, 0f) },
                {Rot4.West, new Vector3(0.2f,backPos , 0f) }
            };

            IR_WeaponData.bow.angle = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 135},
                {Rot4.North, 35f},
                {Rot4.East, 20f},
                {Rot4.West, 340f}
            };

            IR_WeaponData.grenades.pos = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(0.15f, forwardPos, -0.25f) },
                {Rot4.North, new Vector3(-0.15f,backPos, -0.25f) },
                {Rot4.East, new Vector3(0.10f, backPos, -0.25f) },
                {Rot4.West, new Vector3(-0.05f, forwardPos, -0.25f)}
            };

            IR_WeaponData.grenades.angle = new Dictionary<Rot4, float>()
            {
               {Rot4.South, 40f },
                {Rot4.North, 320f },
                {Rot4.East, 320f },
                {Rot4.West, 35f }
            };

            //RANGED
            IR_WeaponData.longRanged.posSide = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(-0.1f, backPos, 0.1f) },
                {Rot4.North, new Vector3(0f, forwardPos, 0f) },
                {Rot4.East, new Vector3(-0.2f, forwardPos, 0f) },
                {Rot4.West, new Vector3(0.2f, backPos, 0f) }
            };

            IR_WeaponData.longRanged.angleSide = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 255f},
                {Rot4.North, 310f},
                {Rot4.East, 295f},
                {Rot4.West, 255f}
            };

            IR_WeaponData.shortRanged.posSide = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(0.15f, forwardPos, -0.25f) },
                {Rot4.North, new Vector3(-0.15f,backPos, -0.25f) },
                {Rot4.East, new Vector3(0.10f, backPos, -0.25f) },
                {Rot4.West, new Vector3(-0.05f, forwardPos, -0.25f)}
            };

            IR_WeaponData.shortRanged.angleSide = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 40f },
                {Rot4.North, 320f },
                {Rot4.East, 320f },
                {Rot4.West, 35f }
            };

            //MELEE

            IR_WeaponData.longMelee.posSide = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(-0.05f, backPos, 0.05f) },
                {Rot4.North, new Vector3(0.05f, forwardPos, -0.05f) },
                {Rot4.East, new Vector3(-0.25f, forwardPos, 0f) },
                {Rot4.West, new Vector3(0.2f, backPos, 0f) }
            };

            IR_WeaponData.longMelee.angleSide = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 55f},
                {Rot4.North, 120f},
                {Rot4.East, 110f},
                {Rot4.West, 75f}
            };

            IR_WeaponData.shortMelee.posSide = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South,  new Vector3(0.15f, forwardPos*2, -0.2f) },
                {Rot4.North, new Vector3(-0.1f, backPos, -0.3f) },
                {Rot4.East, new Vector3(0.05f, backPos, -0.2f) },
                {Rot4.West,  new Vector3(-0.05f, forwardPos*2, -0.2f) }
            };

            IR_WeaponData.shortMelee.angleSide = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 60f},
                {Rot4.North, 110f},
                {Rot4.East, 135f},
                {Rot4.West, 50f}
            };

            //OTHER
            IR_WeaponData.bow.posSide = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(-0.1f, backPos, 0.1f) },
                {Rot4.North, new Vector3(0f, forwardPos, 0f) },
                {Rot4.East, new Vector3(-0.2f, forwardPos, 0f) },
                {Rot4.West, new Vector3(0.2f,backPos , 0f) }
            };

            IR_WeaponData.bow.angleSide = new Dictionary<Rot4, float>()
            {
                {Rot4.South, 135},
                {Rot4.North, 35f},
                {Rot4.East, 20f},
                {Rot4.West, 340f}
            };

            IR_WeaponData.grenades.posSide = new Dictionary<Rot4, Vector3>()
            {
                {Rot4.South, new Vector3(0.15f, forwardPos, -0.25f) },
                {Rot4.North, new Vector3(-0.15f,backPos, -0.25f) },
                {Rot4.East, new Vector3(0.10f, backPos, -0.25f) },
                {Rot4.West, new Vector3(-0.05f, forwardPos, -0.25f)}
            };

            IR_WeaponData.grenades.angleSide = new Dictionary<Rot4, float>()
            {
               {Rot4.South, 40f },
                {Rot4.North, 320f },
                {Rot4.East, 320f },
                {Rot4.West, 35f }
            };

            IR_WeaponData.weaponData = new Dictionary<WeaponType, WeaponPos>() {
                {WeaponType.longRanged, IR_WeaponData.longRanged},
                {WeaponType.shortRanged, IR_WeaponData.shortRanged},
                {WeaponType.longMelee, IR_WeaponData.longMelee},
                {WeaponType.shortMelee, IR_WeaponData.shortMelee},
                {WeaponType.bow, IR_WeaponData.bow},
                {WeaponType.grenades, IR_WeaponData.grenades},
               // {WeaponType.custom1,  IR_WeaponData.custom1},
                //{WeaponType.custom2,  IR_WeaponData.custom2},
               // {WeaponType.custom3,  IR_WeaponData.custom3}
            };
        }

        private static void LoadSideWeaponData()
        {
        }

        private const float forwardPos = 1f;

        private const float backPos = -0.0028957527f;
    }
}