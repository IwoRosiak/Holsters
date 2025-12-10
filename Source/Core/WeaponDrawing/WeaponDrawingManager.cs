#if RimWorld_1_5
using UnityEngine;
using Verse;

namespace RimWorldHolsters.Core.WeaponDrawing
{
    internal static class WeaponDrawingManager
    {
        internal static void DrawWeaponsFor(Pawn pawn)
        {
            if (WeaponDrawingConditionChecker.ShouldDraw(pawn) == false)
                return;

            Rot4 pawnRotation = pawn.Rotation;
            Vector3 rootLoc = pawn.Drawer.renderer.SilhouettePos;
            rootLoc.y = pawn.DrawPos.y; // We want to use the actual draw location here.


            var drawingHandler = new WeaponDrawingHandler(pawn, rootLoc, pawnRotation);

            bool carriesWeaponOpenly = PawnRenderUtility.CarryWeaponOpenly(pawn);

            drawingHandler.DrawEquipment(carriesWeaponOpenly);
        }
    }
}
#endif