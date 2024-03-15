using RimWorldHolsters.Core.WeaponDrawing;
using Verse;

namespace RimWorldHolsters
{
    public class HolstersComp : ThingComp
    {
        public HolstersCompProperties Props => (HolstersCompProperties)props;

        public Pawn pawn
        {
            get => (Pawn)parent;
        }

        public override void PostDraw()
        {
            base.PostDraw();

            WeaponDrawingManager.DrawWeaponsFor(pawn);
        }
    }
}
