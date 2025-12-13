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
                __instance.pawn.Drawer.renderer.SetAllGraphicsDirty();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, 0);
            }
        }
    }

    [HarmonyPatch(typeof(Pawn_EquipmentTracker), "Notify_EquipmentRemoved")]
    public static class Pawn_EquipmentTracker_Notify_EquipmentRemoved_Patch
    {
        [HarmonyPostfix]
        public static void Notify_EquipmentRemovedPostfix(Pawn_EquipmentTracker __instance, ThingWithComps eq)
        {
            try
            {
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


    [HarmonyPatch(typeof(Pawn_InventoryTracker), "Notify_ItemRemoved")]
    public static class ThingOwnerItemRemovedPatch
    {
        [HarmonyPostfix]
        public static void Notify_ItemRemovedPostfix(Pawn_InventoryTracker __instance, Thing item)
        {
            try
            {
                if (item.def.IsWeapon)
                {
                    __instance.pawn.Drawer.renderer.SetAllGraphicsDirty();
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, 0);
            }
        }
    }
}