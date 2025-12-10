#if RimWorld_1_5
using RimWorldHolsters.Core.WeaponDrawing;
using System;
using Verse;

namespace RimWorldHolsters
{
    public class HolstersComp : ThingComp
    {
        public HolstersCompProperties Props => (HolstersCompProperties)props;

        public override void PostDraw()
        {
            base.PostDraw();

            try
            {
                //WeaponDrawingManager.DrawWeaponsFor((Pawn)parent);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, parent.thingIDNumber);
            }
        }
    }
}
#endif