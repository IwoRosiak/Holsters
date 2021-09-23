using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters
{
    [StaticConstructorOnStartup]
    public static class IR_DisplayWeapon
    {
        static IR_DisplayWeapon()
        {
            //weaponsDicts.Add(WeaponType.longRanged, )
        }

        private static WeaponType EstablishWeaponType(ThingWithComps weapon)
        {
            if (weapon.def.IsRangedWeapon)
            {
                if (weapon.def.uiIconScale != 1)
                {
                    return WeaponType.shortRanged;
                }
                return WeaponType.longRanged;
            }
            if (weapon.def.IsMeleeWeapon)
            {
                if (weapon.def.uiIconScale != 1)
                {
                    return WeaponType.shortMelee;
                }
                return WeaponType.longMelee;
            }
            return WeaponType.grenades;
        }

        public static Vector3 GetWeaponPosition(Vector3 rootLoc, Rot4 pawnRotation, Pawn pawn)
        {
            switch (EstablishWeaponType(pawn.equipment.Primary))
            {
                case WeaponType.grenades:
                    break;

                case WeaponType.longRanged:
                    return rootLoc + longRangedPos[pawnRotation];

                case WeaponType.shortRanged:
                    return rootLoc + shortRangedPos[pawnRotation];

                case WeaponType.longMelee:
                    return rootLoc + longMeleePos[pawnRotation];

                case WeaponType.shortMelee:
                    return rootLoc + shortMeleePos[pawnRotation];
            }
            return Vector3.zero;
        }

        private const float forwardPos = 0f;
        private const float backPos = -0.0028957527f;

        private static Dictionary<Rot4, Vector3> longRangedPos = new Dictionary<Rot4, Vector3>()
        {
            {Rot4.South, new Vector3(-0.1f, backPos, 0.1f) },
            {Rot4.North, new Vector3(0f, forwardPos, 0f) },
            {Rot4.East, new Vector3(-0.2f, backPos, 0f) },
            {Rot4.West, new Vector3(0.24f, forwardPos, 0f) }
        };

        private static Dictionary<Rot4, Vector3> shortRangedPos = new Dictionary<Rot4, Vector3>()
        {
            {Rot4.South, new Vector3(0.1f, forwardPos, -0.25f) },
            {Rot4.North, new Vector3(-0.1f,backPos, -0.25f) },
            {Rot4.East, new Vector3(-0.1f, backPos, -0.25f) },
            {Rot4.West, new Vector3(0.1f, forwardPos, -0.25f) }
        };

        private static Dictionary<Rot4, Vector3> longMeleePos = new Dictionary<Rot4, Vector3>()
        {
            {Rot4.South, new Vector3(-0.1f, backPos, 0f) },
            {Rot4.North, new Vector3(0f, forwardPos, 0f) },
            {Rot4.East, new Vector3(-0.2f, forwardPos, 0f) },
            {Rot4.West, new Vector3(0.24f, backPos, 0f) }
        };

        private static Dictionary<Rot4, Vector3> shortMeleePos = new Dictionary<Rot4, Vector3>()
        {
            {Rot4.South,  new Vector3(-0.1f, 1f, -0.1f) },
            {Rot4.North, new Vector3(-0.1f, backPos, -0.1f) },
            {Rot4.East, new Vector3(0.2f, backPos, -0.1f) },
            {Rot4.West,  new Vector3(-0.2f, forwardPos, -0.1f) }
        };

        public static float GetWeaponAngle(Vector3 rootLoc, Rot4 pawnRotation, Pawn pawn)
        {
            switch (EstablishWeaponType(pawn.equipment.Primary))
            {
                case WeaponType.grenades:
                    break;

                case WeaponType.longRanged:
                    return longRangedAngle[pawnRotation];
                    break;

                case WeaponType.shortRanged:
                    return shortRangedAngle[pawnRotation];
                    break;

                case WeaponType.longMelee:
                    return longMeleeAngle[pawnRotation];
                    break;

                case WeaponType.shortMelee:
                    return shortMeleeAngle[pawnRotation];
                    break;
            }
            return 180f;
        }

        //private static Dictionary<WeaponType, Dictionary<Rot4, float>> weaponsDicts = new Dictionary<WeaponType, Dictionary<Rot4, float>>();

        private const float equipOffset = 65f;

        private static Dictionary<Rot4, float> longRangedAngle = new Dictionary<Rot4, float>()
        {
            {Rot4.South, 255f},
            {Rot4.North, 310f},
            {Rot4.East, 295f},
            {Rot4.West, 255f}
        };

        private static Dictionary<Rot4, float> shortRangedAngle = new Dictionary<Rot4, float>()
        {
            {Rot4.South, 45f },
            {Rot4.North, -320f },
            {Rot4.East, -320f },
            {Rot4.West, 15f }
        };

        private static Dictionary<Rot4, float> longMeleeAngle = new Dictionary<Rot4, float>()
        {
            {Rot4.South, 65f},
            {Rot4.North, 120f},
            {Rot4.East, 115f},
            {Rot4.West, 70f}
        };

        private static Dictionary<Rot4, float> shortMeleeAngle = new Dictionary<Rot4, float>()
        {
            {Rot4.South, 70f},
            {Rot4.North, 110f},
            {Rot4.East, 135f},
            {Rot4.West, 50f}
        };

        public static void DrawEquipmentHolstered(Thing eq, Vector3 drawLoc, float aimAngle, Rot4 pawnRotation)
        {
            float num = aimAngle;
            Mesh mesh;
            if (num < 0)
            {
                mesh = MeshPool.plane10Flip;
                num = -num;
            }
            else
            {
                mesh = MeshPool.plane10;
            }

            num %= 360f;
            CompEquippable compEquippable = eq.TryGetComp<CompEquippable>();
            if (compEquippable != null)
            {
                Vector3 b;
                float num2;
                EquipmentUtility.Recoil(eq.def, EquipmentUtility.GetRecoilVerb(compEquippable.AllVerbs), out b, out num2, aimAngle);
                drawLoc += b;
                num += num2;
            }
            Graphic_StackCount graphic_StackCount = eq.Graphic as Graphic_StackCount;
            Material material;
            if (graphic_StackCount != null)
            {
                material = graphic_StackCount.SubGraphicForStackCount(1, eq.def).MatSingleFor(eq);
            }
            else
            {
                material = eq.Graphic.MatSingleFor(eq);
            }
            Graphics.DrawMesh(mesh, drawLoc, Quaternion.AngleAxis(num, Vector3.up), material, 0);
        }

        private static bool ShouldFlip(Rot4 pawnRotation)
        {
            if (pawnRotation == Rot4.West || pawnRotation == Rot4.North)
            {
                return true;
            }
            return false;
        }
    }

    public enum WeaponType
    {
        grenades,
        longRanged,
        shortRanged,
        longMelee,
        shortMelee
    }
}