using UnityEngine;
using Verse;
using Verse.AI.Group;

namespace Holsters.Drawing
{
    internal static class DrawDecider
    {
        internal static void DrawWeaponsFor(Pawn pawn)
        {
            if (DrawConditionChecker.ShouldDraw(pawn) == false)
                return;

            Rot4 pawnRotation = pawn.Rotation;
            Vector3 rootLoc = pawn.DrawPos;

            DrawRequestProcessor drawingHandler = new DrawRequestProcessor(pawn, rootLoc, pawnRotation);

            bool carriesWeaponOpenly = CarriesWeapon(pawn);

            drawingHandler.DrawEquipment(carriesWeaponOpenly);
        }


        private static bool CarriesWeapon(Pawn pawn)
        {
            if (pawn.carryTracker != null && pawn.carryTracker.CarriedThing != null)
            {
                return false;
            }
            if (pawn.Drafted)
            {
                return true;
            }
            if (pawn.CurJob != null && pawn.CurJob.def.alwaysShowWeapon)
            {
                return true;
            }
            if (pawn.mindState.duty != null && pawn.mindState.duty.def.alwaysShowWeapon)
            {
                return true;
            }
            Lord lord = pawn.GetLord();
            return lord != null && lord.LordJob != null && lord.LordJob.AlwaysShowWeapon;

        }
    }
}
