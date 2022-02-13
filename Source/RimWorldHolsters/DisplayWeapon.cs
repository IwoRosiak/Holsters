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
            return IR_HolstersSettings.GetWeaponPos(weapon.def.defName, pawnRotation, isSidearm, pawn);
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

        public static float GetWeaponAngle(Vector3 rootLoc, Rot4 pawnRotation, Pawn pawn, ThingWithComps weapon, bool isSidearm = false)
        {
            return IR_HolstersSettings.GetWeaponAngle(weapon.def.defName, pawnRotation, isSidearm);
        }

        public static bool GetWeaponLayer(Rot4 pawnRotation, Pawn pawn, ThingWithComps weapon, bool isSidearm = false)
        {
            return IR_HolstersSettings.GetWeaponLayer(weapon.def.defName, pawnRotation, isSidearm);
        }

        public static void DrawEquipmentHolstered(Thing eq, Vector3 drawLoc, float aimAngle, Rot4 pawnRotation,bool isFront, bool isSide)
        {
            if (!IR_HolstersSettings.GetWeaponGroupOf(eq.def.defName).isDisplay)
            {
                return;
            }
            float num = aimAngle;
            Mesh mesh = MeshPool.plane10;

            if (IR_HolstersSettings.GetWeaponFlip(eq.def.defName, pawnRotation, isSide))
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
            //Log.Message("Drawing weapon at " + drawLoc.ToString());

            if (isFront)
            {
                drawLoc.y += forwardPos;
            } else
            {
                drawLoc.y += backPos;
            }

            Graphics.DrawMesh(mesh, Matrix4x4.TRS(drawLoc, Quaternion.AngleAxis(num, Vector3.up), (Vector3.one / eq.def.uiIconScale) * IR_HolstersSettings.GetWeaponGroupOf(eq.def.defName).GetSize(pawnRotation)), material, 0);
        }



        private const float forwardPos = 0.001f;

        private const float backPos = -0.0028957527f;
    }
}