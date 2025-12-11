using HarmonyLib;
using System.Reflection;
using UnityEngine;
using Verse;
using RimWorldHolsters.Core.WeaponDrawing;
using System;

namespace RimWorldHolsters.Core.Patches
{
    [HarmonyPatch(typeof(Pawn_EquipmentTracker), "Notify_EquipmentAdded")]
    public static class DrawEquipmentPatch
    {
        [HarmonyPostfix]
        public static void Notify_EquipmentAddedPostfix(Pawn_EquipmentTracker __instance, ThingWithComps eq)
        {
            try
            {
                Log.Message("Checking equipment...");
                __instance.pawn.Drawer.renderer.SetAllGraphicsDirty();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, 0);
            }
        }
    }


    [HarmonyPatch(typeof(ThingOwner), "NotifyAdded")]
    public static class ThingOwnerPatch
    {
        [HarmonyPostfix]
        public static void NotifyAddedPostfix(ThingOwner __instance, Thing item)
        {
            try
            {
                Log.Message("Checking inventory...");
                if (item.def.IsWeapon)
                {
                    if (__instance.Owner is Pawn_InventoryTracker tracker)
                    {
                        tracker.pawn.Drawer.renderer.SetAllGraphicsDirty();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, 0);
            }
        }
    }
}