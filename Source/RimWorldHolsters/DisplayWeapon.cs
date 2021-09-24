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
                if (weapon.def.defName.StartsWith("Bow_"))
                {
                    return WeaponType.bow;
                }
                if (weapon.def.Verbs[0].CausesExplosion)
                {
                    return WeaponType.grenades;
                }
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
            Vector3 pos = Vector3.zero;
            switch (EstablishWeaponType(pawn.equipment.Primary))
            {
                case WeaponType.grenades:
                    pos = rootLoc + shortRangedPos[pawnRotation];
                    break;

                case WeaponType.longRanged:
                    pos = rootLoc + longRangedPos[pawnRotation] + GetBodyOffset(pawn, pawnRotation);
                    break;

                case WeaponType.shortRanged:
                    pos = rootLoc + shortRangedPos[pawnRotation];
                    break;

                case WeaponType.longMelee:
                    pos = rootLoc + longMeleePos[pawnRotation] + GetBodyOffset(pawn, pawnRotation);
                    break;

                case WeaponType.shortMelee:
                    pos = rootLoc + shortMeleePos[pawnRotation];
                    break;

                case WeaponType.bow:
                    pos = rootLoc + bowPos[pawnRotation] + GetBodyOffset(pawn, pawnRotation);
                    break;
            }
            return pos;
        }

        private static Vector3 GetBodyOffset(Pawn pawn, Rot4 pawnRotation)
        {
            Vector3 offset = Vector3.zero;
            if (pawn.story.bodyType.defName == "Fat" || pawn.story.bodyType.defName == "Hulk")
            {
                if (pawnRotation == Rot4.East)
                {
                    offset = new Vector3(-0.2f, 0f, 0f);
                }
                if (pawnRotation == Rot4.West)
                {
                    offset = new Vector3(0.2f, 0f, 0f);
                }
            }

            return offset;
        }

        private const float forwardPos = 0f;
        private const float backPos = -0.0028957527f;

        private static Dictionary<Rot4, Vector3> longRangedPos = new Dictionary<Rot4, Vector3>()
        {
            {Rot4.South, new Vector3(-0.1f, backPos, 0.1f) },
            {Rot4.North, new Vector3(0f, forwardPos, 0f) },
            {Rot4.East, new Vector3(-0.2f, forwardPos, 0f) },
            {Rot4.West, new Vector3(0.2f, backPos, 0f) }
        };

        private static Dictionary<Rot4, Vector3> shortRangedPos = new Dictionary<Rot4, Vector3>()
        {
            {Rot4.South, new Vector3(0.15f, forwardPos, -0.3f) },
            {Rot4.North, new Vector3(-0.15f,backPos, -0.3f) },
            {Rot4.East, new Vector3(0.15f, backPos, -0.3f) },
            {Rot4.West, new Vector3(-0.15f, forwardPos, -0.3f)}
        };

        private static Dictionary<Rot4, Vector3> longMeleePos = new Dictionary<Rot4, Vector3>()
        {
            {Rot4.South, new Vector3(-0.1f, backPos, 0.1f) },
            {Rot4.North, new Vector3(0f, forwardPos, 0.1f) },
            {Rot4.East, new Vector3(-0.2f, forwardPos, 0f) },
            {Rot4.West, new Vector3(0.2f, backPos, 0f) }
        };

        private static Dictionary<Rot4, Vector3> shortMeleePos = new Dictionary<Rot4, Vector3>()
        {
            {Rot4.South,  new Vector3(0.1f, 1f, -0.3f) },
            {Rot4.North, new Vector3(-0.1f, backPos, -0.3f) },
            {Rot4.East, new Vector3(0.15f, backPos, -0.3f) },
            {Rot4.West,  new Vector3(-0.15f, forwardPos, -0.3f) }
        };

        private static Dictionary<Rot4, Vector3> bowPos = new Dictionary<Rot4, Vector3>()
        {
            {Rot4.South, new Vector3(-0.1f, backPos, 0.1f) },
            {Rot4.North, new Vector3(0f, forwardPos, 0f) },
            {Rot4.East, new Vector3(-0.2f, backPos, 0f) },
            {Rot4.West, new Vector3(0.2f, forwardPos, 0f) }
        };

        public static float GetWeaponAngle(Vector3 rootLoc, Rot4 pawnRotation, Pawn pawn)
        {
            float angle = 180f;
            switch (EstablishWeaponType(pawn.equipment.Primary))
            {
                case WeaponType.grenades:
                    angle = shortRangedAngle[pawnRotation];
                    break;

                case WeaponType.longRanged:
                    angle = longRangedAngle[pawnRotation];
                    break;

                case WeaponType.shortRanged:
                    angle = shortRangedAngle[pawnRotation];
                    break;

                case WeaponType.longMelee:
                    angle = longMeleeAngle[pawnRotation];
                    break;

                case WeaponType.shortMelee:
                    angle = shortMeleeAngle[pawnRotation];
                    break;

                case WeaponType.bow:
                    angle = bowAngle[pawnRotation];
                    break;
            }
            return angle;
        }

        private static Dictionary<Rot4, float> longRangedAngle = new Dictionary<Rot4, float>()
        {
            {Rot4.South, 255f},
            {Rot4.North, 310f},
            {Rot4.East, 295f},
            {Rot4.West, 255f}
        };

        private static Dictionary<Rot4, float> shortRangedAngle = new Dictionary<Rot4, float>()
        {
            {Rot4.South, 40f },
            {Rot4.North, -320f },
            {Rot4.East, -320f },
            {Rot4.West, 35f }
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

        private static Dictionary<Rot4, float> bowAngle = new Dictionary<Rot4, float>()
        {
            {Rot4.South, 135},
            {Rot4.North, 35f},
            {Rot4.East, -20f},
            {Rot4.West, 340f}
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
            Graphics.DrawMesh(mesh, Matrix4x4.TRS(drawLoc, Quaternion.AngleAxis(num, Vector3.up), Vector3.one / eq.def.uiIconScale), material, 0);
        }
    }

    public enum WeaponType
    {
        grenades,
        longRanged,
        shortRanged,
        longMelee,
        shortMelee,
        bow
    }
}