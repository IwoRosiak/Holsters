using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Core
{
    [StaticConstructorOnStartup]
    public static class IR_DisplayWeapon
    {
        // private static List<WeaponGroupCordInfo> defaultGroups = new List<WeaponGroupCordInfo>();
        static IR_DisplayWeapon()
        {
            // defaultGroups = IR_HolstersInit.LoadDefaultWeaponGroups();
        }

        public static Vector3 GetWeaponPosition(Vector3 rootLoc, Rot4 pawnRotation, Pawn pawn, ThingWithComps weapon, WeaponGroupCordInfo group, bool isSidearm = false)
        {
            // if (IR_HolstersSettings.groups.NullOrEmpty())
            //{
            //    return defaultGroups[IR_HolstersSettings.GetWeaponGroupOf(weapon.def.defName)].GetPos(pawnRotation, isSidearm);
            //}

            return IR_HolstersSettings.GetWeaponPos(weapon.def.defName, pawnRotation, isSidearm, pawn, group);
        }

        public static BodyType GetBodyType(Pawn pawn)
        {
            if (pawn.story?.bodyType?.defName == "Fat")
            {
                return BodyType.fat;
            }

            if (pawn.story?.bodyType?.defName == "Hulk")
            {
                return BodyType.hulk;
            }

            if (pawn.story?.bodyType?.defName == "Thin")
            {
                return BodyType.thin;
            }
            return BodyType.male;
        }

        public static void DrawEquipmentHolstered(WeaponGroupCordInfo curGroup, Thing eq, Vector3 drawLoc, float aimAngle, Rot4 pawnRotation, bool isFront, bool isSide)
        {
            if (!curGroup.isDisplay)
            {
                return;
            }
            float num = aimAngle;
            Mesh mesh = MeshPool.plane10;

            if (IR_HolstersSettings.GetWeaponFlip(curGroup, pawnRotation, isSide))
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

            if (isFront)
            {
                drawLoc.y += IR_HolstersSettings.FrontPos;
            }
            else
            {
                drawLoc.y += IR_HolstersSettings.BackPos;
            }

            Graphics.DrawMesh(mesh, Matrix4x4.TRS(drawLoc, Quaternion.AngleAxis(num, Vector3.up), Vector3.one / eq.def.uiIconScale * curGroup.GetSize(pawnRotation)), material, 0);
        }
    }
}