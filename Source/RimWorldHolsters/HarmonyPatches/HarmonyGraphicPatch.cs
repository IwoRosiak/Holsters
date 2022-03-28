using HarmonyLib;
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
            List<WeaponGroupCordInfo> filledSlots = new List<WeaponGroupCordInfo>();
            WeaponGroupCordInfo curGroup;


            if (pawn.GetPosture() == PawnPosture.Standing && pawn.equipment?.Primary != null)
            {
                curGroup = IR_HolstersSettings.GetWeaponGroupOf(pawn.equipment.Primary.def.defName);

                //DRAW PRIMARY
                if (!(bool)CarryWeaponOpenly?.Invoke(__instance, null))
                {
                    vector += IR_DisplayWeapon.GetWeaponPosition(rootLoc, pawnRotation, pawn, pawn.equipment.Primary, curGroup);
                    vector.y = 0;
                    vector += rootLoc;
                    float angle = IR_HolstersSettings.GetWeaponAngle(curGroup, pawnRotation,false);
                    bool isFrontLayer = IR_HolstersSettings.GetWeaponLayer(curGroup, pawnRotation, false);

                    IR_DisplayWeapon.DrawEquipmentHolstered(curGroup, pawn.equipment.Primary, vector, angle, pawnRotation, isFrontLayer, false);
                }

                filledSlots.Add(curGroup);
                
                if (IR_HolstersSettings.displaySide)
                {
                    foreach (var weapon in pawn.inventory.innerContainer)
                    {
                        curGroup = IR_HolstersSettings.GetWeaponGroupOf(weapon.def.defName);

                        Vector3 vector2 = new Vector3(0f, 0f, 0f);
                        if (weapon.def.IsWeapon)
                        {
                            bool isSide = true;

                            if (IR_HolstersSettings.smartSideDisplay && !filledSlots.Contains(curGroup))
                            {
                                isSide = false;
                            }

                            vector2 += IR_DisplayWeapon.GetWeaponPosition(rootLoc, pawnRotation, pawn, (ThingWithComps)weapon, curGroup, isSide);
                            vector2+= rootLoc;
                            float angle2 = IR_HolstersSettings.GetWeaponAngle(curGroup, pawnRotation, isSide);

                            bool isFrontLayer = IR_HolstersSettings.GetWeaponLayer(curGroup, pawnRotation, isSide);

                            IR_DisplayWeapon.DrawEquipmentHolstered(curGroup, weapon, vector2, angle2, pawnRotation, isFrontLayer, isSide);

                            filledSlots.Add(curGroup);
                            
                        }
                    }
                }
                
            }
        }
    }
}