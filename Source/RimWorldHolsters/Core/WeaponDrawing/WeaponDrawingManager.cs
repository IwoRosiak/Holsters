using System.Reflection;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Core.WeaponDrawing
{
    internal static class WeaponDrawingManager
    {
        internal static void DrawWeaponFor(PawnRenderer renderer, Vector3 rootLoc, Rot4 pawnRotation, Pawn pawn)
        {
            if (WeaponDrawingConditionChecker.ShouldDraw(pawn) == false)
                return;
            

            var drawingHandler = new WeaponDrawingHandler(pawn, rootLoc, pawnRotation);

            bool carriesMainWeapon = pawn.Drafted;
            drawingHandler.DrawEquipment(carriesMainWeapon);
        }
    }
}
