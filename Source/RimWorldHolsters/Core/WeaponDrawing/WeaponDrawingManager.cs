using UnityEngine;
using Verse;
using Verse.AI.Group;

namespace RimWorldHolsters.Core.WeaponDrawing
{
    internal static class WeaponDrawingManager
    {
        internal static void DrawWeaponsFor(Pawn pawn)
        {
            if (WeaponDrawingConditionChecker.ShouldDraw(pawn) == false)
                return;

            Rot4 pawnRotation = pawn.Rotation;
            Vector3 rootLoc = pawn.DrawPos;

            var drawingHandler = new WeaponDrawingHandler(pawn, rootLoc, pawnRotation);

            bool carriesWeaponOpenly = IsCarryingWeaponOpenly(pawn);

            drawingHandler.DrawEquipment(carriesWeaponOpenly);
        }

        private static bool IsCarryingWeaponOpenly(Pawn pawn)
        {
            if (pawn.carryTracker != null && pawn.carryTracker.CarriedThing != null)
                return false;
            
            if (pawn.Drafted)
                return true;
            
            if (pawn.CurJob != null && pawn.CurJob.def.alwaysShowWeapon)
                return true;
            
            if (pawn.mindState.duty != null && pawn.mindState.duty.def.alwaysShowWeapon)
                return true;
            
            Lord lord = pawn.GetLord();
            return lord != null && lord.LordJob != null && lord.LordJob.AlwaysShowWeapon;
        }
    }
}
