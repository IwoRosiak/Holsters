using HarmonyLib;
using RimWorld;
using System.Reflection;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.HarmonyPatches
{
    [HarmonyPatch(typeof(PawnRenderer), "DrawEquipment")]
    public static class IR_PawnDrawPatch
    {
        private static readonly FieldInfo tempPawn = AccessTools.Field(typeof(PawnRenderer), "pawn");

        private static MethodInfo CarryWeaponOpenly = AccessTools.Method(typeof(PawnRenderer), "CarryWeaponOpenly");

        [HarmonyPostfix]
        public static void PawnDrawPostfix(PawnRenderer __instance, Vector3 rootLoc, Rot4 pawnRotation, PawnRenderFlags flags)
        {
            Vector3 vector = new Vector3(0f, 0f, 0f);

            Pawn pawn = (Pawn)tempPawn.GetValue(__instance);

            if (pawn.GetPosture() == PawnPosture.Standing && !(bool)CarryWeaponOpenly.Invoke(__instance, null) && pawn.equipment?.Primary != null)
            {
                vector += IR_DisplayWeapon.GetWeaponPosition(rootLoc, pawnRotation, pawn);
                float angle = IR_DisplayWeapon.GetWeaponAngle(rootLoc, pawnRotation, pawn);

                IR_DisplayWeapon.DrawEquipmentHolstered(pawn.equipment.Primary, vector, angle, pawnRotation);
            }
        }
    }
}