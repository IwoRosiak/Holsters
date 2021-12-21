﻿using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
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

            if (pawn.Dead)
            {
                return;
            }
            /*
            Stance_Busy stance_Busy = pawn.stances?.curStance as Stance_Busy;

            if (stance_Busy != null && !stance_Busy.neverAimWeapon && stance_Busy.focusTarg.IsValid && (flags & PawnRenderFlags.NeverAimWeapon) == PawnRenderFlags.None)
            {
                return;
            }*/
            List<WeaponType> filledSlots = new List<WeaponType>();

            if (pawn.GetPosture() == PawnPosture.Standing && pawn.equipment?.Primary != null)
            {
                //DRAW PRIMARY
                if (!(bool)CarryWeaponOpenly?.Invoke(__instance, null))
                {
                    vector += IR_DisplayWeapon.GetWeaponPosition(rootLoc, pawnRotation, pawn, pawn.equipment.Primary);
                    float angle = IR_DisplayWeapon.GetWeaponAngle(rootLoc, pawnRotation, pawn, pawn.equipment.Primary);

                    IR_DisplayWeapon.DrawEquipmentHolstered(pawn.equipment.Primary, vector, angle, pawnRotation, false);
                }

                filledSlots.Add(IR_WeaponType.EstablishWeaponType(pawn.equipment.Primary));

                if (IR_HolstersSettings.displaySide)
                {
                    foreach (var weapon in pawn.inventory.innerContainer)
                    {
                        Vector3 vector2 = new Vector3(0f, 0f, 0f);
                        if (weapon.def.IsWeapon)
                        {
                            bool isSide = true;

                            if (IR_HolstersSettings.smartSideDisplay && !filledSlots.Contains(IR_WeaponType.EstablishWeaponType(weapon.def)))
                            {
                                isSide = false;
                            }

                            vector2 += IR_DisplayWeapon.GetWeaponPosition(rootLoc, pawnRotation, pawn, (ThingWithComps)weapon, isSide);
                            float angle2 = IR_DisplayWeapon.GetWeaponAngle(rootLoc, pawnRotation, pawn, (ThingWithComps)weapon, isSide);

                            IR_DisplayWeapon.DrawEquipmentHolstered(weapon, vector2, angle2, pawnRotation, isSide);

                            filledSlots.Add(IR_WeaponType.EstablishWeaponType(weapon.def));
                        }
                    }
                }
            }
        }
    }
}