#if RimWorld_1_4 || RimWorld_1_3
using HarmonyLib;
using System.Reflection;
using UnityEngine;
using Verse;
using RimWorldHolsters.Core.WeaponDrawing;
using System;

namespace RimWorldHolsters.Core
{
    [HarmonyPatch(typeof(PawnRenderer), "DrawEquipment")]
    public static class DrawEquipmentPatch
    {
        private static readonly FieldInfo tempPawn = AccessTools.Field(typeof(PawnRenderer), "pawn");

        private static readonly MethodInfo CarryWeaponOpenly = AccessTools.Method(typeof(PawnRenderer), "CarryWeaponOpenly");

        [HarmonyPostfix]
        public static void PawnDrawPostfix(PawnRenderer __instance, Vector3 rootLoc, Rot4 pawnRotation, PawnRenderFlags flags)
        {
            try
            {
                var pawn = (Pawn)tempPawn.GetValue(__instance);

                CompatibilityMode.DrawWeaponFor(__instance, rootLoc, pawnRotation, CarryWeaponOpenly, pawn);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, 0);
            }
        }
    }
}
#endif