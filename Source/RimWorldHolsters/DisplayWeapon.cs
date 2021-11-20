using RimWorld;
using UnityEngine;
using Verse;

namespace RimWorldHolsters
{
    [StaticConstructorOnStartup]
    public static class IR_DisplayWeapon
    {
        public static Vector3 GetWeaponPosition(Vector3 rootLoc, Rot4 pawnRotation, Pawn pawn)
        {
            Vector3 offset = IR_WeaponTypePositions.GetWeaponPos(IR_WeaponType.EstablishWeaponType(pawn.equipment.Primary), pawnRotation);

            if (IR_WeaponType.EstablishWeaponSize(pawn.equipment.Primary) == true)
            {
                offset += IR_PositionAdjuster.GetBodyOffsetLargeWeapons(pawn, pawnRotation);
            }
            else
            {
                offset += IR_PositionAdjuster.GetBodyOffsetSmallWeapons(pawn, pawnRotation);
            }
            return rootLoc + offset;
        }

        public static float GetWeaponAngle(Vector3 rootLoc, Rot4 pawnRotation, Pawn pawn)
        {
            return IR_WeaponTypePositions.GetWeaponAngle(IR_WeaponType.EstablishWeaponType(pawn.equipment.Primary), pawnRotation);
        }

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