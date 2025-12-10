#if RimWorld_1_4 || RimWorld_1_3
using System.Reflection;
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Core
{
    internal static class CompatibilityMode
    {
        internal static void DrawWeaponFor(PawnRenderer renderer, Vector3 rootLoc, Rot4 pawnRotation, MethodInfo CarryWeaponOpenly, Pawn pawn)
        {

            if (WeaponDrawingConditionChecker.ShouldDraw(pawn) == false)
                return;

            bool carriesMainWeapon = (bool)CarryWeaponOpenly?.Invoke(renderer, null);

            var drawingHandler = new WeaponDrawingHandler(pawn, rootLoc, pawnRotation);

            drawingHandler.DrawEquipment(carriesMainWeapon);
        }
    }
}
#endif