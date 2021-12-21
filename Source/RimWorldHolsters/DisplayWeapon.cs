using RimWorld;
using UnityEngine;
using Verse;

namespace RimWorldHolsters
{
    [StaticConstructorOnStartup]
    public static class IR_DisplayWeapon
    {
        public static Vector3 GetWeaponPosition(Vector3 rootLoc, Rot4 pawnRotation, Pawn pawn, ThingWithComps weapon, bool isSidearm = false)
        {
            Vector3 offset = IR_WeaponData.GetWeaponPos(IR_WeaponType.EstablishWeaponType(weapon), pawnRotation, isSidearm) + GetBodyOffset(IR_WeaponType.EstablishWeaponType(weapon.def), isSidearm, pawnRotation, IR_PositionAdjuster.GetBodyType(pawn));

            return rootLoc + offset;
        }

        public static Vector3 GetBodyOffset(WeaponType type, bool isSidearm, Rot4 pawnRotation, BodyType body)
        {
            Vector3 offset = Vector3.zero;

            if (IR_WeaponType.EstablishWeaponSize(type) == true)
            {
                offset += IR_PositionAdjuster.GetBodyOffsetLargeWeapons(pawnRotation, null, body);
            }
            else
            {
                if (isSidearm)
                {
                    offset -= IR_PositionAdjuster.GetBodyOffsetSmallWeapons(pawnRotation, null, body);
                }
                else
                {
                    offset += IR_PositionAdjuster.GetBodyOffsetSmallWeapons(pawnRotation, null, body);
                }
            }

            return offset;
        }

        public static float GetWeaponAngle(Vector3 rootLoc, Rot4 pawnRotation, Pawn pawn, ThingWithComps weapon, bool isSidearm = false)
        {
            return IR_WeaponData.GetWeaponAngle(IR_WeaponType.EstablishWeaponType(weapon), pawnRotation, isSidearm);
        }

        public static void DrawEquipmentHolstered(Thing eq, Vector3 drawLoc, float aimAngle, Rot4 pawnRotation, bool isSide)
        {
            if (eq.def.EstablishWeaponType() == WeaponType.doNotDisplay)
            {
                return;
            }
            float num = aimAngle;
            Mesh mesh = MeshPool.plane10;

            float flip = 1;

            if (IR_WeaponData.GetWeaponFlip(eq.def.EstablishWeaponType(), pawnRotation, isSide))
            {
                mesh = MeshPool.plane10Flip;
                num += 180;
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
        bow,
        doNotDisplay,
        custom1,
        custom2,
        custom3
    }
}