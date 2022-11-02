using RimWorldHolsters.Core.WeaponDrawing;
using Verse;

namespace RimWorldHolsters
{
    public class HolstersComp : ThingComp
    {
        public HolstersCompProperties Props => (HolstersCompProperties)props;

        public override void PostDraw()
        {
            base.PostDraw();

            WeaponDrawingManager.DrawWeaponsFor((Pawn)parent);
        }
    }
}
