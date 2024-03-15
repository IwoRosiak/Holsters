using RimWorld;
using Verse;

namespace RimWorldHolsters.Core
{
    internal static class WeaponDrawingConditionChecker
    {
        internal static bool ShouldDraw(Pawn pawn)
        {
            if (pawn.Dead)
                return false;

            if (!pawn.ageTracker.Adult)
                return false;

            if (!IR_HolstersSettings.displayIndoors && pawn.GetRoom()?.ProperRoom == true)
                return false;

            if (pawn.GetPosture() != PawnPosture.Standing)
                return false;

            if (pawn.equipment?.Primary == null)
                return false;

            //if (IsNaked(pawn))
            //{
            //return false;
            //}

            return true;
        }
        /*
        private static bool IsNaked(Pawn pawn)
        {
            bool isNotDrawingAnyClothes = pawn.Drawer.renderer.BodyGraphic. apparelGraphics.NullOrEmpty();

            return isNotDrawingAnyClothes;
        }*/
    }
}
