using System.Reflection;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Core.WeaponDrawing
{
    internal static class WeaponDrawingManager
    {
        internal static void DrawWeaponFor(PawnRenderer renderer, Vector3 rootLoc, Rot4 pawnRotation, MethodInfo CarryWeaponOpenly, FieldInfo tempPawn)
        {
            Pawn pawn = (Pawn)tempPawn.GetValue(renderer);

            if (WeaponDrawingConditionChecker.ShouldDraw(pawn) == false)
            {
                return;
            }

            bool carriesMainWeapon = (bool)CarryWeaponOpenly?.Invoke(renderer, null);

            WeaponDrawingHandler drawingHandler = new WeaponDrawingHandler(pawn, rootLoc, pawnRotation);

            drawingHandler.DrawEquipment(carriesMainWeapon);
        }
    }
}
