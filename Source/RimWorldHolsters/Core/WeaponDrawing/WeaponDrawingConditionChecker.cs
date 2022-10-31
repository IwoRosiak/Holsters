using RimWorld;
using Verse;

namespace RimWorldHolsters.Core
{
    internal static class WeaponDrawingConditionChecker
    {
        internal static bool ShouldDraw(Pawn pawn)
        {
            if (pawn.Dead)
            {
                return false;
            }

            if (!IR_HolstersSettings.displayIndoors && pawn.GetRoom()?.ProperRoom == true)
            {
                return false ;
            }


            if (pawn.GetPosture() != PawnPosture.Standing )
            {
                return false;
            }

            if (pawn.equipment?.Primary == null)
            {
                return false;
            }

            /*
            Stance_Busy stance_Busy = pawn.stances?.curStance as Stance_Busy;

            if (stance_Busy != null && !stance_Busy.neverAimWeapon && stance_Busy.focusTarg.IsValid && (flags & PawnRenderFlags.NeverAimWeapon) == PawnRenderFlags.None)
            {
                return;
            }*/

            if (IsNaked(pawn))
            {
                return false;
            }


            return true;
        }

        private static bool IsNaked(Pawn pawn)
        {
            bool isNotDrawingAnyClothes = pawn.Drawer.renderer.graphics.apparelGraphics.NullOrEmpty();

            return isNotDrawingAnyClothes;
        }
    }
}
